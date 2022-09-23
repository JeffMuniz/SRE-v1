using AutoMapper;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.ValueObjects.SkuUnavailableSearchFilter>()
                .ForMember(
                    dest => dest.MainContract,
                    opt => opt.Ignore()
                )
                .ForMember(
                    dest => dest.Available,
                    opt => opt.Ignore()
                )
                .ReverseMap();
        }
    }
}
