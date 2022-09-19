using AutoMapper;

namespace Availability.Search.Worker.Backend.Application.UseCases.Shared.Mappings
{
    public class AvailabilityFoundMap : Profile
    {
        public AvailabilityFoundMap()
        {
            CreateMap<Models.AvailabilityFound, Domain.Entities.Availability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.AvailabilityFound, Domain.ValueObjects.AvailabilityId>()
                .ReverseMap();
        }
    }
}
