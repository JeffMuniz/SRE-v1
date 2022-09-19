using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class GetLatestAvailabilityMap : Profile
    {
        public GetLatestAvailabilityMap()
        {
            CreateMap<Domain.ValueObjects.ShardId, AvailabilityMessaging.Manager.GetLatestAvailability>()
                    .ForMember(
                        dest => dest.ShardId,
                        opt => opt.MapFrom(source => source.Id)
                    );

            CreateMap<Domain.Entities.SkuAvailability, AvailabilityMessaging.Manager.GetLatestAvailability>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, AvailabilityMessaging.Manager.GetLatestAvailability>()
                    .ReverseMap();
        }
    }
}
