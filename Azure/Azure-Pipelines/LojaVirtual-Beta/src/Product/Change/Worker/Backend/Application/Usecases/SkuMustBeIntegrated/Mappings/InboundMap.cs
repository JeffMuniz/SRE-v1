using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Application.Usecases.SkuMustBeIntegrated.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, SharedDomain.ValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap();
        }
    }
}
