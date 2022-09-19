using AutoMapper;
using UsecaseModels = Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Models;
using CategorizationMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Categorization;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;
using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;

namespace Product.Categorization.Worker.Consumers.UpsertSku.Mappings
{
    public class SkuCategorizedMap : Profile
    {
        public SkuCategorizedMap()
        {
            CreateMap<UsecaseModels.Inbound, CategorizationMessaging.SkuCategorized>()
                .IncludeMembers(source => source.SupplierSku);

            CreateMap<SharedUsecaseModels.SupplierSku, CategorizationMessaging.SkuCategorized>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                );

            CreateMap<UsecaseModels.Outbound, CategorizationMessaging.SkuCategorized>()
                .ForMember(
                    dest => dest.CategorizedData,
                    opt => opt.MapFrom(source => source)
                );

            CreateMap<UsecaseModels.Outbound, SharedMessagingModels.CategorizedData>();

            CreateMap<UsecaseModels.SubcategoryProbability, SharedMessagingModels.CategorizedDataProbability>();
        }
    }
}
