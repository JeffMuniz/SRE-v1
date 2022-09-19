using AutoMapper;
using SharedValueObjects = Shared.Backend.Domain.ValueObjects;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Mappings
{
    public class AvailabilityMap : Profile
    {
        public AvailabilityMap()
        {
            CreateMap<Models.AvailabilityResult, Domain.Entities.Availability>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.AvailabilityResult, SharedValueObjects.Price>()
                .ForMember(
                    dest => dest.From,
                    opt => opt.MapFrom(source => source.PriceFrom)
                )
                .ForMember(
                    dest => dest.For,
                    opt => opt.MapFrom(source => source.PriceFor)
                )
                .ReverseMap();
        }
    }
}
