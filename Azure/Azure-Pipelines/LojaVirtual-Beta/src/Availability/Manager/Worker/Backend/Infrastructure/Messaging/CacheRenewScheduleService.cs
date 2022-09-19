using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Configurations.Models;
using MassTransit;
using Microsoft.Extensions.Options;
using Shared.Messaging.Configuration;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Messaging
{
    public class CacheRenewScheduleService : ICacheRenewScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<CacheConfigurationOptions> _options;
        private readonly ICacheRenewScheduleRepository _cacheRenewScheduleRepository;
        private readonly IMessageScheduler _messageScheduler;

        public CacheRenewScheduleService(
            IMapper mapper,
            IOptionsMonitor<CacheConfigurationOptions> options,
            ICacheRenewScheduleRepository cacheRenewScheduleRepository,
            IMessageScheduler messageScheduler
        )
        {
            _mapper = mapper;
            _options = options;
            _cacheRenewScheduleRepository = cacheRenewScheduleRepository;
            _messageScheduler = messageScheduler;
        }

        public async Task Schedule(
            Domain.ValueObjects.ShardId shardId,
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var cacheRenewScheduleId = _mapper.Map<Domain.ValueObjects.CacheRenewScheduleId>(shardId, skuAvailability);

            if (
                await _cacheRenewScheduleRepository.Get(cacheRenewScheduleId, cancellationToken) is var existingCacheRenewScheduleResult &&
                existingCacheRenewScheduleResult.HasValue &&
                existingCacheRenewScheduleResult.Value is var cacheRenewSchedule
            )
                await CancelScheduleMessageBus(cacheRenewSchedule.ScheduleId);
            else
                cacheRenewSchedule = Domain.Entities.CacheRenewSchedule.Create(cacheRenewScheduleId);

            var newSchedule = await ScheduleMessageBus(cacheRenewScheduleId, skuAvailability, cancellationToken);

            cacheRenewSchedule.ChangeSchedule(newSchedule.TokenId, newSchedule.ScheduledTime);

            if (existingCacheRenewScheduleResult.HasNoValue)
                await _cacheRenewScheduleRepository.Add(cacheRenewSchedule, cancellationToken);
            else
                await _cacheRenewScheduleRepository.Update(cacheRenewSchedule, cancellationToken);
        }

        private Task<MassTransit.Scheduling.ScheduledMessage<CheckAvailabilityCacheMustBeRenewed>> ScheduleMessageBus(
            Domain.ValueObjects.CacheRenewScheduleId cacheRenewScheduleId,
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var checkCacheAvailabilityMustBeRenewedMessage = _mapper.Map<CheckAvailabilityCacheMustBeRenewed>(skuAvailability, cacheRenewScheduleId);

            return _messageScheduler.ScheduleSend(
                EndpointConfigurator.GetCommandEndpoint<CheckAvailabilityCacheMustBeRenewed>(),
                _options.CurrentValue.Availability.Renew.ScheduleTime,
                checkCacheAvailabilityMustBeRenewedMessage,
                cancellationToken
            );
        }

        private Task CancelScheduleMessageBus(Guid scheduleId) =>
            _messageScheduler.CancelScheduledSend(
                EndpointConfigurator.GetCommandEndpoint<CheckAvailabilityCacheMustBeRenewed>(),
                scheduleId
            );
    }
}
