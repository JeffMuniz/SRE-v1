using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Shared.Backend.Application.Usecases.Mappings
{
    public class ImageMap : Profile
    {
        public ImageMap()
        {
            CreateMap<Models.Image, SharedDomain.ValueObjects.Image>()
                .ReverseMap();
        }
    }
}
