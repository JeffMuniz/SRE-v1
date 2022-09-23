using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Api.Backend.Infrastructure.ExternalServices.Mappings
{
    public class AvailabilityMap : Profile
    {
        public AvailabilityMap()
        {
            CreateMap<Domain.ValueObjects.SkuAvailabilitySearchFilter, AvailabilityMessaging.Search.GetAvailability>()
                .IncludeMembers(source => source.SkuAvailabilityId)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, AvailabilityMessaging.Search.GetAvailability>()
                .ReverseMap();

            CreateMap<AvailabilityMessaging.Search.AvailabilityFound, Domain.Entities.SkuAvailability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<AvailabilityMessaging.Search.AvailabilityFound, Domain.ValueObjects.SkuAvailabilityId>()
                .ReverseMap();
        }
    }
}
