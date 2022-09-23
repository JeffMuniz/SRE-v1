using AutoMapper;
using Messaging = Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;
using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;
using UsecaseModels = Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku.Models;

namespace Product.Persistence.Worker.Consumers.UpsertSku.Mappings
{
    public class SkuUpsertedMap : Profile
    {
        public SkuUpsertedMap()
        {
            CreateMap<UsecaseModels.Inbound, Messaging.SkuUpserted>()
                .IncludeMembers(source => source.SupplierSku);

            CreateMap<SharedUsecaseModels.SupplierSku, Messaging.SkuUpserted>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                );

            CreateMap<UsecaseModels.Outbound, Messaging.SkuUpserted>();
        }
    }
}
