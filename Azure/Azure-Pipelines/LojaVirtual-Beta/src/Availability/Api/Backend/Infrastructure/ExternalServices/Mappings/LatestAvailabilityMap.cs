using AutoMapper;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Api.Backend.Infrastructure.ExternalServices.Mappings
{
    public class LatestAvailabilityMap : Profile
    {
        public LatestAvailabilityMap()
        {
            CreateMap<Domain.ValueObjects.SkuAvailabilitySearchFilter, GetLatestAvailability>()
                .IncludeMembers(source => source.SkuAvailabilityId)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, GetLatestAvailability>()
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilitySearchFilter, Domain.Entities.SkuAvailability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.SkuAvailabilityId)
                )
                .ReverseMap();

            CreateMap<GetLatestAvailabilityResponse, Domain.Entities.SkuAvailability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<GetLatestAvailabilityResponse, Domain.ValueObjects.SkuAvailabilityId>()
                .ReverseMap();
        }
    }
}
