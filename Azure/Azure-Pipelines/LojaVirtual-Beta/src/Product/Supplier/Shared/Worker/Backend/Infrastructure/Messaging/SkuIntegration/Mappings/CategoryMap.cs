using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Domain.Entities.Category, MessagingContracts.Shared.Models.Category>()
                .ReverseMap();
        }
    }
}
