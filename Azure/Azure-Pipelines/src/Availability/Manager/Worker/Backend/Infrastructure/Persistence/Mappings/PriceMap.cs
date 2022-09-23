using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Mappings
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
