using AutoMapper;
using Product.Persistence.Worker.Backend.Domain.Entities;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Models.Response.Category, Category>()
                .ReverseMap();
        }
    }
}
