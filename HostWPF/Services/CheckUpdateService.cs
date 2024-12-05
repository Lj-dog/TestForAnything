using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
using Serilog;

namespace HostWPF.Services
{
    internal class CheckUpdateService : BackgroundService
    {


        private readonly ILogger _logger;
        public CheckUpdateService(ILogger logger)
        {
                _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {
             await Task.Delay(TimeSpan.FromSeconds(5),stoppingToken);

                //_logger.LogInformation("Checking for updates...");
                _logger.Information("Checking for updates...");

            }
        }
    }
}
