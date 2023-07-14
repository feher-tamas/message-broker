using Majordomo;
using MessageBroker.Configurations;
using Microsoft.Extensions.Options;

namespace MessageBroker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BrokerConfig _config;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<BrokerConfig> config)
        {
            _logger = logger;
            _config = config.CurrentValue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                await RunBroker(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
        }
        private async Task RunBroker(CancellationToken cts)
        {
            using var broker = new MDPBroker($"tcp://{_config.Address}:{_config.Port}");
            broker.LogInfoReady += (s, e) => _logger.LogInformation(e.Info);
            broker.DebugInfoReady += (s, e) => _logger.LogDebug(e.Info);

            await broker.Run(cts);
        }
    }
}