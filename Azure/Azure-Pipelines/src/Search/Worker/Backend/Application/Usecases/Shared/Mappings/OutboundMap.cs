using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Search.Worker.Backend.Application.Usecases.Shared.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Domain.Entities.Sku, Models.Outbound>()
                .IncludeMembers(source => source.SupplierSkuId)
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId.SkuId)
                )
                .ReverseMap();

            CreateMap<SharedDomain.ValueObjects.SupplierSkuId, Models.Outbound>()
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();
        }
    }
}
