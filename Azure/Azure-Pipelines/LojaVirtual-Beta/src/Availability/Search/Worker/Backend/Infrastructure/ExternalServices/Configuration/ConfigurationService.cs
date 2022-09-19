using AutoMapper;
using Availability.Search.Worker.Backend.Domain.Services;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Cache;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Client;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<MainContractsConfigurationOptions> _mainContractsOptions;
        private readonly ICacheConfiguration _cache;
        private readonly IPartnerHubConfigurationClient _partnerHubConfigurationClient;

        public ConfigurationService(
            ILogger<ConfigurationService> logger,
            IMapper mapper,
            IOptionsMonitor<MainContractsConfigurationOptions> mainContractsOptions,
            ICacheConfiguration cache,
            IPartnerHubConfigurationClient partnerHubConfigurationClient
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mainContractsOptions = mainContractsOptions;
            _cache = cache;
            _partnerHubConfigurationClient = partnerHubConfigurationClient;
        }

        public async Task<Result<Domain.ValueObjects.PartnerConfiguration, Domain.ValueObjects.ErrorType>> GetPartner(
            int supplierId,
            CancellationToken cancellationToken = default
        )
        {
            var getConfiguration = await GetConfiguration(cancellationToken);
            if (getConfiguration.IsFailure)
                return getConfiguration.Error;

            var getPartnerResult = getConfiguration.Value.GetPartner(supplierId);
            if (getPartnerResult.IsFailure)
                return getPartnerResult.Error;

            return getPartnerResult.Value;
        }

        private async Task<Result<Domain.ValueObjects.Configuration, Domain.ValueObjects.ErrorType>> GetConfiguration(
            CancellationToken cancellationToken = default
        )
        {
            if (_cache.TryGetClientConfiguration(out Domain.ValueObjects.Configuration clientConfiguration))
                return clientConfiguration;

            var getContractsResult = await _partnerHubConfigurationClient.GetAllContracts(cancellationToken);
            if (getContractsResult.IsFailure)
                return Domain.ValueObjects.ErrorType.Unexpected;

            var connectorConfigList = new System.Collections.Concurrent.ConcurrentBag<Models.ConnectorConfig>();

            await getContractsResult.Value
                .ParallelForEachAsync(
                    maxDegreeOfParallelism: 4,
                    async config =>
                    {
                        var getContractParametersResult = await _partnerHubConfigurationClient.GetContractParameters(
                            config.Contract,
                            config.Connector,
                            cancellationToken
                        );

                        if (getContractParametersResult.IsFailure)
                        {
                            _logger.LogWarning("O connector {Connector} do contrato {Contract} não pode ser consultado devido o erro: {Error}",
                                config.Connector,
                                config.Contract,
                                getContractParametersResult.Error
                            );
                            return;
                        }

                        connectorConfigList.Add(new Models.ConnectorConfig
                        {
                            Connector = config.Connector,
                            PartnerConfigs = getContractParametersResult.Value.Partners.Select(
                                partnerName => new Models.PartnerConfig
                                {
                                    PartnerCode = partnerName,
                                    Contract = config.Contract,
                                })
                        });
                    }
                );

            var partnerContracts = connectorConfigList
                .SelectMany(contract => contract.PartnerConfigs
                    .Select(partnerConfig => new {
                        contract.Connector,
                        partnerConfig.PartnerCode,
                        partnerConfig.Contract
                    })
                    .Distinct()
                )
                .GroupBy(
                    key => (key.Connector, key.PartnerCode),
                    selector => selector.Contract
                );

            var getPartnersResult = await _partnerHubConfigurationClient.GetAllPartners(cancellationToken);
            if (getPartnersResult.IsFailure)
                return Domain.ValueObjects.ErrorType.Unexpected;

            var partners = Models.Partner.FromPartnerContracts(partnerContracts, getPartnersResult.Value);

            clientConfiguration = _mapper.Map<Domain.ValueObjects.Configuration>(
                partners,
                _mainContractsOptions.CurrentValue
            );

            _cache.SetClientConfiguration(clientConfiguration);

            return clientConfiguration;
        }
    }
}
