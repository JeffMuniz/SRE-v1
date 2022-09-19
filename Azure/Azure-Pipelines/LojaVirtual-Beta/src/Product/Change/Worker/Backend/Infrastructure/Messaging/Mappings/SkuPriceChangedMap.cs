using AutoMapper;
using Shared.Messaging.Contracts.Product.Change.Messages;

namespace Product.Change.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class SkuPriceChangedMap : Profile
    {
        public SkuPriceChangedMap()
        {
            CreateMap<Domain.Entities.SkuIntegration, SkuPriceChanged>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierSku.Id.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SupplierSku.Id.SkuId)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(source => source.SupplierSku.Price)
                )
                .ReverseMap();
        }
    }
}
