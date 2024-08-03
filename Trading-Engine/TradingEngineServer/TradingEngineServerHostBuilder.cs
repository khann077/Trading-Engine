﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingEngineServer.core;
using TradingEngineServer.Logging;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging.LoggingConfiguration;

namespace TradingEngineServer.Core
{
    public sealed class TradingEngineServerHostBuilder
    {
        public static IHost BuildTradingEngineServer()
            => Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                // Start Config
                services.AddOptions();
                services.Configure<TradingEngineServerConfiguration>(context.Configuration.GetSection(nameof(TradingEngineServerConfiguration)));
                services.Configure<LoggerConfiguration>(context.Configuration.GetSection(nameof(LoggerConfiguration)));
                    
                // Add Singleton Objects
                services.AddSingleton<ITradingEngineServer, TradingEngineServer>();
                services.AddSingleton<ITextLogger, TextLogger>();   

                // Add hosted service
                services.AddHostedService<TradingEngineServer>();
            }).Build();
    }
}
