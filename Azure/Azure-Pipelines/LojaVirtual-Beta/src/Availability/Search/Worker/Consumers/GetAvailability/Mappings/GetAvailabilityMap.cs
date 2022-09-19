using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using SharedUsecaseModels = Availability.Search.Worker.Backend.Application.UseCases.Shared.Models;
using UsecaseModels = Availability.Search.Worker.Backend.Application.UseCases.GetAvailability.Models;

namespace Availability.Search.Worker.Consumers.GetAvailability.Mappings
{
    public class GetAvailabilityMap : Profile
    {
        public GetAvailabilityMap()
        {
            CreateMap<AvailabilityMessaging.Search.GetAvailability, UsecaseModels.Inbound>()
                .ReverseMap();

            CreateMap<UsecaseModels.Outbound, AvailabilityMessaging.Search.AvailabilityFound>()
                .IncludeBase<SharedUsecaseModels.AvailabilityFound, AvailabilityMessaging.Search.AvailabilityFound>()
                .ReverseMap();

            CreateMap<AvailabilityMessaging.Search.GetAvailability, AvailabilityMessaging.Search.AvailabilityFound>()
                .ForMember(
                    dest => dest.PersistedSkuId,
                    opt => opt.MapFrom(source => source.PersistedSkuId)
                )
                .ForMember(
                    dest => dest.ShardId,
                    opt => opt.MapFrom(source => source.ShardId)
                )
                .IgnoreForAllOtherMembers()
                .ReverseMap();
        }
    }
}
