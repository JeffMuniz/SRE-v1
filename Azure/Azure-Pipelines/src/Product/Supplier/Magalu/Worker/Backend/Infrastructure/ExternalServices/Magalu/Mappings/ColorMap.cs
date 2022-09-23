using AutoMapper;
using Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Models;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Mappings
{
    public class ColorMap : Profile
    {
        public ColorMap()
        {
            CreateMap<Color, Domain.Entities.Color>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Code)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Description)
                )
                .ReverseMap();
        }
    }
}
