using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.core;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;

namespace TradingEngineServer.Core
{
    sealed class TradingEngineServer : BackgroundService, ITradingEngineServer
    {
        private readonly ITextLogger _logger;
        private readonly TradingEngineServerConfiguration _tradingEngineServerConfig;
        public TradingEngineServer(IOptions<TradingEngineServerConfiguration> config, ITextLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tradingEngineServerConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task Run(CancellationToken token) => ExecuteAsync(token);
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information(nameof(TradingEngineServer), "Starting Trading Engine");
            while (!stoppingToken.IsCancellationRequested) 
            { 
                
            }
            _logger.Information(nameof(TradingEngineServer), "Stopping Trading Engine");
            return Task.CompletedTask;
        }
    }
}
