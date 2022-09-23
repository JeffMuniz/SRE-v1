using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Search.Worker.Backend.Application.Usecases.RemoveSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<SharedDomain.ValueObjects.SupplierSkuId, Models.Inbound>()
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();
        }
    }
}
