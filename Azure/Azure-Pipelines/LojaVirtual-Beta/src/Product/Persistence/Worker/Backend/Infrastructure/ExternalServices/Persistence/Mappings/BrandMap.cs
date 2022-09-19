using AutoMapper;
using Product.Persistence.Worker.Backend.Domain.Entities;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class BrandMap : Profile
    {
        public BrandMap()
        {
            CreateMap<Models.Response.Brand, Brand>()
                .ReverseMap();
        }
    }
}
