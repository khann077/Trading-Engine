﻿using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Options;
using TradingEngineServer.Logging.LoggingConfiguration;
namespace TradingEngineServer.Logging
{
    public class TextLogger : AbstractLogger, ITextLogger
    {
        private readonly LoggerConfiguration _configuration;
        public TextLogger(IOptions<LoggerConfiguration> loggingConfiguration) : base() 
        {
            _configuration  = loggingConfiguration.Value ?? throw new ArgumentNullException(nameof(loggingConfiguration));
            if (_configuration.LoggerType != LoggerType.Text)
            {
                throw new InvalidOperationException($"{nameof(TextLogger)} doesn't match type of {_configuration.LoggerType}");
            }
            var now = DateTime.Now;
            string logDirectory = Path.Combine(_configuration.TextLoggerConfiguration.Directory, $"{now:yyyy-MM-dd}");
            string uniqueLogName = $"{_configuration.TextLoggerConfiguration.Filename}-{now:HH_mm_ss}";
            string baseLogName = Path.ChangeExtension(uniqueLogName, _configuration.TextLoggerConfiguration.FileExtension);
            string filepath = Path.Combine(logDirectory, baseLogName);
            Directory.CreateDirectory( logDirectory );
            _ = Task.Run(() => LogAsync(filepath, _logQueue, _tokenSource.Token));

        }

        ~TextLogger ()
        {
            Dispose(false);
        }  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing) {
            lock (_lock)
            { 
            if(_disposed) return;
                _disposed = true;

            }
           
            if(disposing)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }
        }

        protected override void Log(LogLevel logLevel, string module, string message)
        {
            _logQueue.Post(new LogInformation(logLevel, module, message, DateTime.Now, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name));
                    }

        private readonly BufferBlock<LogInformation> _logQueue = new BufferBlock<LogInformation>();
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly object _lock = new object();
        private bool _disposed = false;
        private static async Task LogAsync(string filepath, BufferBlock<LogInformation> logQueue, CancellationToken token)
        {
            using var fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs) { AutoFlush = true};

            try
            {
                while (true)
                {
                    var logItem = await logQueue.ReceiveAsync(token).ConfigureAwait(false);
                    string formattedMessage = FormatLogItem(logItem);
                    await sw.WriteAsync(formattedMessage).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            { 
                
            }
        }

        private static string FormatLogItem(LogInformation logItem)
        {
            return $"[{logItem.now:yyyy-MM-dd HH-mm-ss.fffffff}] [{logItem.ThreadName, -30}:{logItem.ThreadId:000}]" + 
               $"[{logItem.logLevel} - {logItem.message}]" ;    

        }
    }
}
