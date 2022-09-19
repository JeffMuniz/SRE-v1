using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Change.Worker.Backend.Application.Usecases;

namespace Product.Change.Worker.Consumers.Mappings
{
    public class SkuMustBeIntegratedMap : Profile
    {
        public SkuMustBeIntegratedMap()
        {
            CreateMap<MessagingContracts.Product.Change.Messages.SkuMustBeIntegrated, Usecases.SkuMustBeIntegrated.Models.Inbound>()                
                .ReverseMap();

            CreateMap<MessagingContracts.Product.Change.Messages.SkuMustBeIntegratedResponse, Usecases.SkuMustBeIntegrated.Models.Outbound>()
                .ReverseMap();
        }
    }
}
