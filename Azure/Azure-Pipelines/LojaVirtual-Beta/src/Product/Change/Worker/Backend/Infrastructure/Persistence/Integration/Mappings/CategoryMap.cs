using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Domain.Entities.Category, Entities.Category>()
                .ReverseMap();
        }
    }
}
