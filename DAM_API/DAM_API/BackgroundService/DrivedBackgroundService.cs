using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAM_API.BackgroundService
{
    public class DrivedBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IWorker _worker;
        public DrivedBackgroundService(IWorker worker) 
        {
            _worker = worker;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //await _worker.DoWork(cancellationToken);
            await _worker.DoWork(cancellationToken);
            //await _worker.VerificarFaltas(cancellationToken);
        }
    }
}
