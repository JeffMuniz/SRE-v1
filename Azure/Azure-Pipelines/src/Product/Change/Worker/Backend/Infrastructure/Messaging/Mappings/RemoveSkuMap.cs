using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class RemoveSkuMap : Profile
    {
        public RemoveSkuMap()
        {
            CreateMap<Domain.Entities.SkuIntegration, Shared.Messaging.Contracts.Product.Saga.Messages.Change.RemoveSku>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierSku.Id.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SupplierSku.Id.SkuId)
                )
                .ReverseMap();
        }
    }
}
