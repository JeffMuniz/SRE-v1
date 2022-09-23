using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class PriceMap : Profile
    {
        public PriceMap()
        {
            CreateMap<SharedDomain.ValueObjects.Price, Entities.Price>()
                .ReverseMap();
        }
    }
}
