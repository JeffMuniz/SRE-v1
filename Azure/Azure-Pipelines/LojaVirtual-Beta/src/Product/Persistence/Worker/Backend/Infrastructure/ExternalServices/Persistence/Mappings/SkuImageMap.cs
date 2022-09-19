using AutoMapper;
using Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class SkuImageMap : Profile
    {
        public SkuImageMap()
        {
            CreateMap<Domain.ValueObjects.SkuImage, SkuImage>()
                .ReverseMap();
        }
    }
}
