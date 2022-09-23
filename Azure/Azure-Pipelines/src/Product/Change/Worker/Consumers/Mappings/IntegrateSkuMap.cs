using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Change.Worker.Backend.Application.Usecases;

namespace Product.Change.Worker.Consumers.Mappings
{
    public class IntegrateSkuMap : Profile
    {
        public IntegrateSkuMap()
        {
            CreateMap<MessagingContracts.Product.Change.Messages.IntegrateSku, Usecases.IntegrateSku.Models.Inbound>()
                .IncludeMembers(source => source.SupplierSku)
                .ReverseMap();

            CreateMap<MessagingContracts.Shared.Models.SupplierSku, Usecases.IntegrateSku.Models.Inbound>()
                .ReverseMap();
        }
    }
}
