using AutoMapper;
using ApplicationModels = Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<ApplicationModels.Inbound, Domain.ValueObjects.SkuAvailability>()
                .ForMember(
                    dest => dest.Available,
                    opt => opt.MapFrom((source, dest) => false)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.Ignore()
                )
                .ReverseMap();
        }
    }
}
