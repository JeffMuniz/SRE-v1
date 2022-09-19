using System.Threading;
using System.Threading.Tasks;

namespace Shared.Job.Abstractions
{
    public interface IJob : IJob<object>
    {
    }

    public interface IJob<T>
    {
        string JobName { get; }

        Task Execute(T context, CancellationToken cancellationToken);
    }
}
