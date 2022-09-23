using AutoMapper;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Mappings
{
    public class SubcategoriaMap : Profile
    {
        public SubcategoriaMap()
        {
            CreateMap<Domain.Entities.Subcategory, Models.Subcategoria>()
                .ForMember(
                    dest => dest.IdNumerico,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.IdNumericoCategoria,
                    opt => opt.MapFrom(source => source.Category.Id)
                )
                .ForMember(
                    dest => dest.Categoria,
                    opt => opt.MapFrom(source => source.Category)
                );
        }
    }
}
