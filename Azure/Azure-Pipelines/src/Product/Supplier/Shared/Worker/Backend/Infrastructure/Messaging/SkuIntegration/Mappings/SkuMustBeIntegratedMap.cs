using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class SkuMustBeIntegratedMap : Profile
    {
        public SkuMustBeIntegratedMap()
        {
            CreateMap<MessagingContracts.Product.Change.Messages.SkuMustBeIntegrated, Domain.ValueObjects.SkuMustBeIntegrated>()
                .ForPath(
                    source => source.SupplierSkuId.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForPath(
                    source => source.SupplierSkuId.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap();

            CreateMap<MessagingContracts.Product.Change.Messages.SkuMustBeIntegrated, SharedDomain.ValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap();
        }
    }
}
