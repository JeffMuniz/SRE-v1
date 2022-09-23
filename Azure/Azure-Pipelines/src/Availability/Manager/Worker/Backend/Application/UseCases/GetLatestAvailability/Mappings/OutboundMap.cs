using AutoMapper;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Models.Outbound, Domain.Entities.SkuAvailability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.Outbound, Domain.ValueObjects.SkuAvailabilityId>()
                .ReverseMap();
        }
    }
}
