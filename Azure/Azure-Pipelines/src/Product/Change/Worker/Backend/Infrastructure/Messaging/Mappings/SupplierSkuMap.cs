using AutoMapper;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;

namespace Product.Change.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<Domain.Entities.SupplierSku, SharedMessagingContracts.Models.SupplierSku>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<Domain.Entities.Category, SharedMessagingContracts.Models.Category>()
                .ReverseMap();

            CreateMap<Domain.Entities.Subcategory, SharedMessagingContracts.Models.Subcategory>()
                .ReverseMap();

            CreateMap<Domain.Entities.Brand, SharedMessagingContracts.Models.Brand>()
                .ReverseMap();
        }
    }
}
