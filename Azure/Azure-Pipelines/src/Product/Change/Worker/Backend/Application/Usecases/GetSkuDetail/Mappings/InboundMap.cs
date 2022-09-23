using AutoMapper;

namespace Product.Change.Worker.Backend.Application.Usecases.GetSkuDetail.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.ValueObjects.SkuIntegrationId>()
                .ForMember(
                    dest => dest.Value,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ReverseMap();
        }
    }
}
