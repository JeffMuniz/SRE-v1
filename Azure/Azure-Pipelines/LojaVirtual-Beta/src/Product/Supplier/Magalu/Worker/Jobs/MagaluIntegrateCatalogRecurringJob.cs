using Microsoft.Extensions.Options;
using Shared.Job.Abstractions.Product.Supplier.Magalu;
using System.Threading;
using System.Threading.Tasks;
using Usecases = Product.Supplier.Magalu.Worker.Backend.Application.Usecases;

namespace Product.Supplier.Magalu.Worker.Jobs
{
    internal class MagaluIntegrateCatalogRecurringJob : IMagaluIntegrateCatalogRecurringJob
    {
        public const string JOB_NAME = "IntegrateCatalog.Magalu";

        private readonly IOptionsMonitor<JobScheduleConfiguration> _options;
        private readonly Usecases.IntegrateFullCatalog.IIntegrateFullCatalogUsecase _integrateFullCatalogUsecase;

        public string JobName => JOB_NAME;

        public MagaluIntegrateCatalogRecurringJob(
            IOptionsMonitor<JobScheduleConfiguration> options,
            Usecases.IntegrateFullCatalog.IIntegrateFullCatalogUsecase integrateFullCatalogUsecase
        )
        {
            _options = options;
            _integrateFullCatalogUsecase = integrateFullCatalogUsecase;
        }

        //[AutomaticRetry(Attempts = 0, DelaysInSeconds = new[] { 60} )]
        //[JobDisplayName(JOB_NAME)]
        public async Task Execute(object context, CancellationToken cancellationToken)
        {
            await _integrateFullCatalogUsecase
                .Execute(
                    new Usecases.IntegrateFullCatalog.Models.Inbound
                    {
                        SupplierId = _options.CurrentValue.SupplierId
                    },
                    cancellationToken
                );
        }

        
    }
}
