using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class GetAvailabilityMap : Profile
    {
        public GetAvailabilityMap()
        {
            CreateMap<Domain.ValueObjects.ShardId, AvailabilityMessaging.Search.GetAvailability>()
                .ForMember(
                    dest => dest.ShardId,
                    opt => opt.MapFrom(source => source.Id)
                );

            CreateMap<Domain.Entities.SkuAvailability, AvailabilityMessaging.Search.GetAvailability>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, AvailabilityMessaging.Search.GetAvailability>()
                .ReverseMap();
        }
    }
}
