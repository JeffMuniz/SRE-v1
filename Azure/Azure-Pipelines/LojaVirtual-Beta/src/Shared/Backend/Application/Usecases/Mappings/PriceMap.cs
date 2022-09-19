using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Shared.Backend.Application.Usecases.Mappings
{
    public class PriceMap : Profile
    {
        public PriceMap()
        {
            CreateMap<Models.Price, SharedDomain.ValueObjects.Price>()
                .ReverseMap();
        }
    }
}
