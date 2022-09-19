using AutoMapper;
using ApplicationModels = Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Models;
using CategorizationMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Categorization;

namespace Product.Categorization.Worker.Consumers.UpsertSku.Mappings
{
    public class CategorizeSkuMap : Profile
    {
        public CategorizeSkuMap()
        {
            CreateMap<CategorizationMessaging.CategorizeSku, ApplicationModels.Inbound>()
                .ReverseMap();
        }
    }
}
