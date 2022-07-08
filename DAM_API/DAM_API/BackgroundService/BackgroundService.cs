using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAM_API.BackgroundService
{
    public abstract class BackgroundService : IHostedService
    {
        private readonly ILogger<BackgroundService> _logger;
        private readonly IWorker _worker;

        public BackgroundService(ILogger<BackgroundService> logger, IWorker worker)
        {
            _logger = logger;
            _worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _worker.DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Printing worker stopping");
            return Task.CompletedTask;
        }
    }
}
