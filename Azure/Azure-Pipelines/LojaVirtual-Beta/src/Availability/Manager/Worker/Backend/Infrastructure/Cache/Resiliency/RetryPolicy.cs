using Availability.Manager.Worker.Configurations.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Resiliency
{
    public class RetryPolicy : IRetryPolicy
    {
        private readonly IOptionsMonitor<CacheConfigurationOptions> _options;

        public RetryPolicy(IOptionsMonitor<CacheConfigurationOptions> options)
        {
            _options = options;
        }

        public Task Execute(
            Func<Task> exec,
            (string title, ILogger logger) log,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        ) => Execute(exec, DefaultRetryLog(log), DefaultErrorLog(log), retryAttempts, cancellationToken);

        public Task<T> Execute<T>(
            Func<Task<T>> exec,
            (string title, ILogger logger) log,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        ) => Execute(exec, DefaultRetryLog(log), DefaultErrorLog(log), retryAttempts, cancellationToken);

        public Task Execute(
            Func<Task> exec,
            Action<Exception, TimeSpan, int, int> retryLog,
            Action<Exception> errorLog = null,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        ) => Execute(
            async () =>
            {
                await exec();
                return true;
            }, retryLog, errorLog, retryAttempts, cancellationToken
        );

        public async Task<T> Execute<T>(
            Func<Task<T>> exec,
            Action<Exception, TimeSpan, int, int> retryLog,
            Action<Exception> errorLog = null,
            int? retryAttempts = null,
            CancellationToken cancellationToken = default
        )
        {
            var retryAttemptsCount = retryAttempts ?? _options.CurrentValue.Resiliency.RetryAttempts;

            var policyResult = await Policy
                .Handle<Exception>(error =>
                    !(error is OperationCanceledException)
                )
                .WaitAndRetryAsync(
                    retryAttemptsCount,
                    retryCount => TimeSpan.FromSeconds(retryCount),
                    (error, retryTime, retryCount, _) => retryLog(error, retryTime, retryCount, retryAttemptsCount)
                )
                .ExecuteAndCaptureAsync(async cancel =>
                {
                    if (cancel.IsCancellationRequested)
                        cancel.ThrowIfCancellationRequested();

                    return await exec();
                }, cancellationToken);

            if (policyResult.Outcome == OutcomeType.Failure)
            {
                errorLog?.Invoke(policyResult.FinalException);
                throw policyResult.FinalException;
            }

            return policyResult.Result;
        }

        private static Action<Exception, TimeSpan, int, int> DefaultRetryLog((string title, ILogger logger) log) =>
            (error, retryTime, retryCount, retryAttempts) =>
                log.logger.LogWarning(error, $"[{log.title}] - Retry - Tentativa {retryCount} de {retryAttempts} - Próxima tentativa em {retryTime}");

        private static Action<Exception> DefaultErrorLog((string title, ILogger logger) log) =>
            (error) =>
                log.logger.LogError(error, $"[{log.title}] - Retry - Erro após realizar todas as tentativas");
    }
}
