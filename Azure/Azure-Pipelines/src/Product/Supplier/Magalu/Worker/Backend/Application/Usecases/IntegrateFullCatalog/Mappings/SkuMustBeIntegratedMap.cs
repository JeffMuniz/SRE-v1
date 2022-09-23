using AutoMapper;
using SharedDomain = Shared.Backend.Domain;
using SharedSupplierDomain = Product.Supplier.Shared.Worker.Backend.Domain;

namespace Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog.Mappings
{
    public class SkuMustBeIntegratedMap : Profile
    {
        public SkuMustBeIntegratedMap()
        {
            CreateMap<Domain.Entities.Sku, SharedSupplierDomain.ValueObjects.SkuMustBeIntegrated>()
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom((source, dest, member, context) =>
                    {
                        if (context.Items.TryGetValue("SupplierId", out var contextValue) && contextValue is int supplierId)
                            return new SharedDomain.ValueObjects.SupplierSkuId { SupplierId = supplierId, SkuId = source.Id.ToString() };
                        
                        return new SharedDomain.ValueObjects.SupplierSkuId { SkuId = source.Id.ToString() };
                    })
                )
                .ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(source => source.Active)
                )
                .ReverseMap();
        }
    }
}
