using AutoMapper;
using ApplicationModels = Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<ApplicationModels.Inbound, ApplicationModels.Outbound>()
                .ReverseMap();
        }
    }
}
