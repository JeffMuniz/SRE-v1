using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class ImageMap : Profile
    {
        public ImageMap()
        {
            CreateMap<SharedDomain.ValueObjects.Image, Entities.Image>()
                .ReverseMap();
        }
    }
}
