using AutoMapper;
using ApplicationModels = Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<ApplicationModels.Inbound, Domain.ValueObjects.SkuAvailability>()
                .ReverseMap();
        }
    }
}
