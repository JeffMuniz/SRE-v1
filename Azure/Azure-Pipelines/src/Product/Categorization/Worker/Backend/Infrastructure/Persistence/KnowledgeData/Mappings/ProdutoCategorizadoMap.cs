using AutoMapper;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Mappings
{
    public class ProdutoCategorizadoMap : Profile
    {
        public ProdutoCategorizadoMap()
        {
            CreateMap<Entities.ProdutoCategorizado, Domain.Entities.KnowledgeProduct>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(source => source.Nome)
                )
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(source => source.Marca)
                )
                .ForMember(
                    dest => dest.PartnerCategory,
                    opt => opt.MapFrom(source => source.CategoriaParceiro)
                )
                .ForMember(
                    dest => dest.PartnerSubcategory,
                    opt => opt.MapFrom(source => source.SubcategoriaParceiro)
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom(source => source.Caracteristicas
                        .GroupBy(key => key.Nome, value => value.Valor)
                        .ToDictionary(key => key.Key, value => value.First())
                    )
                )
                .ForMember(
                    dest => dest.KnowledgeSubcategoryId,
                    opt => opt.MapFrom(source => source.IdSubcategoria)
                );
        }
    }
}
