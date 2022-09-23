using AutoMapper;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Domain.ValueObjects.PagedSkuAvailability, Models.Outbound>();

            CreateMap<Domain.ValueObjects.SkuUnavailableSearchFilter, Models.Outbound>()
                .ForMember(
                    dest => dest.IsLastPage,
                    opt => opt.MapFrom((source, dest) => source.IsLastPage(dest.Total))
                );

            CreateMap<Models.UnavailableSku, Domain.Entities.SkuAvailability>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<Models.UnavailableSku, Domain.ValueObjects.SkuAvailabilityId>()
                .ReverseMap();
        }
    }
}
