using AutoMapper;
using PersistenceMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;
using ApplicationModels = Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku.Models;
using SharedMessaging = Shared.Messaging.Contracts.Shared;
using SharedUsecasePersistenceModels = Product.Persistence.Worker.Backend.Application.Usecases.Shared.Models;

namespace Product.Persistence.Worker.Consumers.UpsertSku.Mappings
{
    public class UpsertSkuMap : Profile
    {
        public UpsertSkuMap()
        {
            CreateMap<PersistenceMessaging.UpsertSku, ApplicationModels.Inbound>()
                .ForMember(
                    dest => dest.CategorizationData,
                    opt => opt.MapFrom(source => source.CategorizedData)
                )
                .ReverseMap();

            CreateMap<SharedMessaging.Models.CategorizedData, SharedUsecasePersistenceModels.CategorizationData>()
                .ReverseMap();

            CreateMap<SharedMessaging.Models.EnrichedData, SharedUsecasePersistenceModels.EnrichedData>()
                .ReverseMap();

            CreateMap<SharedMessaging.Models.EnrichedProduct, SharedUsecasePersistenceModels.EnrichedProduct>()
                .ReverseMap();

            CreateMap<SharedMessaging.Models.EnrichedSku, SharedUsecasePersistenceModels.EnrichedSku>()
                .ReverseMap();
        }
    }
}
