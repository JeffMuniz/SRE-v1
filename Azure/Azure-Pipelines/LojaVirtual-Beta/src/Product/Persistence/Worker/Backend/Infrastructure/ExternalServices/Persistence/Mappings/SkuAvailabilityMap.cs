using AutoMapper;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class SkuAvailabilityMap : Profile
    {
        public SkuAvailabilityMap()
        {
            CreateMap<Domain.ValueObjects.SkuAvailability, Models.Request.SkuAvailability>()
                .ReverseMap();
        }
    }
}
