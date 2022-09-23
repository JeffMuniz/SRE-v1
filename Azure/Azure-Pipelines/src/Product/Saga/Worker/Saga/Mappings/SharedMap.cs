using AutoMapper;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class SharedMap : Profile
    {
        public SharedMap()
        {
            CreateMap<SharedMessagingModels.SupplierSku, SharedMessagingModels.SupplierSku>()
                .ReverseMap();

            CreateMap<SharedMessagingModels.CategorizedData, SharedMessagingModels.CategorizedData>()
                .ReverseMap();
        }
    }
}
