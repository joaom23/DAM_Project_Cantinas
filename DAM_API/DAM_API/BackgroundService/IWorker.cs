using System.Threading;
using System.Threading.Tasks;

namespace DAM_API.BackgroundService
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
        //Task AtualizarFaltas(CancellationToken cancellation);
        //Task VerificarFaltas(CancellationToken cancellation);
    }
}