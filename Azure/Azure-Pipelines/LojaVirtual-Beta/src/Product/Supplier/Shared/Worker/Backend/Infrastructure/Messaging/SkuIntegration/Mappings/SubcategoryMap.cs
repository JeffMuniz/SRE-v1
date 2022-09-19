using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class SubcategoryMap : Profile
    {
        public SubcategoryMap()
        {
            CreateMap<Domain.Entities.Subcategory, MessagingContracts.Shared.Models.Subcategory>()
                .ReverseMap();
        }
    }
}
