using AutoMapper;
using ApplicationModels = Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Mappings
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
