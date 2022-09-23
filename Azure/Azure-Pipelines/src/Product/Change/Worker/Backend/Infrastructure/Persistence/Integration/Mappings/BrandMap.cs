using AutoMapper;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class BrandMap : Profile
    {
        public BrandMap()
        {
            CreateMap<Domain.Entities.Brand, Entities.Brand>()
                .ReverseMap();
        }
    }
}
