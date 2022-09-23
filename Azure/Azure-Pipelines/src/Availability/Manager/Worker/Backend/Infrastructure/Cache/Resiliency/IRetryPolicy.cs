using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Resiliency
{
    public interface IRetryPolicy
    {
        Task Execute(
            Func<Task> exec,
            (string title, ILogger logger) log,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        );

        Task<T> Execute<T>(
            Func<Task<T>> exec,
            (string title, ILogger logger) log,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        );

        Task Execute(
            Func<Task> exec,
            Action<Exception, TimeSpan, int, int> retryLog,
            Action<Exception> errorLog = null,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        );

        Task<T> Execute<T>(
            Func<Task<T>> exec,
            Action<Exception, TimeSpan, int, int> retryLog,
            Action<Exception> errorLog = null,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        );
    }
}
