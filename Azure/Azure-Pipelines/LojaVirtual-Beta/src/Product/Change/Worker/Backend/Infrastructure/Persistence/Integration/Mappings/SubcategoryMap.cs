using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class SubcategoryMap : Profile
    {
        public SubcategoryMap()
        {
            CreateMap<Domain.Entities.Subcategory, Entities.Subcategory>()
                .ReverseMap();
        }
    }
}
