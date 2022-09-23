using AutoMapper;
using Product.Persistence.Worker.Backend.Domain.Entities;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class SubcategoryMap : Profile
    {
        public SubcategoryMap()
        {
            CreateMap<Models.Response.Subcategory, Subcategory>()
                .ReverseMap();
        }
    }
}
