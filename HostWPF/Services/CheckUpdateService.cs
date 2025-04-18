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
        //private readonly ILogger<CheckUpdateService> _logger;
        public CheckUpdateService(ILogger logger)
        //public CheckUpdateService(ILogger<CheckUpdateService> logger)
        {
                _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {
             await Task.Delay(TimeSpan.FromSeconds(15),stoppingToken);

                //_logger.LogInformation("Checking for updates...");
                _logger.Information("Checking for updates...");

            }
        }
    }
}
