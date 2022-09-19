using AutoMapper;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Mappings
{
    public class ProdutoCategorizadoManualmenteMap : Profile
    {
        public ProdutoCategorizadoManualmenteMap()
        {
            CreateMap<Domain.Entities.KnowledgeProduct, Models.ProdutoCategorizadoManualmente>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.Id)
                )
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(source => source.Name)
                )
                .ForMember(
                    dest => dest.Marca,
                    opt => opt.MapFrom(source => source.Brand)
                )
                .ForMember(
                    dest => dest.CategoriaParceiro,
                    opt => opt.MapFrom(source => source.PartnerCategory)
                )
                .ForMember(
                    dest => dest.SubcategoriaParceiro,
                    opt => opt.MapFrom(source => source.PartnerSubcategory)
                )
                .ForMember(
                    dest => dest.Caracteristicas,
                    opt => opt.MapFrom(source => source.Features
                        .Select(x => new Models.Caracteristica
                        {
                            ProductId = source.Id,
                            Nome = x.Key,
                            Valor = x.Value
                        })
                        .ToArray()
                    )
                )
                .ForMember(
                    dest => dest.IdSubcategoria,
                    opt => opt.MapFrom(source => source.KnowledgeSubcategoryId)
                );
        }
    }
}
