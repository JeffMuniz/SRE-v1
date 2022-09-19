using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class CheckCacheAvailabilityMustBeRenewedMap : Profile
    {

        public CheckCacheAvailabilityMustBeRenewedMap()
        {
            CreateMap<Domain.ValueObjects.CacheRenewScheduleId, AvailabilityMessaging.Manager.CheckAvailabilityCacheMustBeRenewed>()
                .ForMember(
                    dest => dest.ShardId,
                    opt => opt.MapFrom((source, dest) => source.ShardId?.Id)
                );

            CreateMap<Domain.Entities.SkuAvailability, AvailabilityMessaging.Manager.CheckAvailabilityCacheMustBeRenewed>()
                .ForMember(
                    dest => dest.PersistedSkuId,
                    opt => opt.MapFrom(source => source.PersistedSkuId)
                )
                .ReverseMap();
        }
    }
}
