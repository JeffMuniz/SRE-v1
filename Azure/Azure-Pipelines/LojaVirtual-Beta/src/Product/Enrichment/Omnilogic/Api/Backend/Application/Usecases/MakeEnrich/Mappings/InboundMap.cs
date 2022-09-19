using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.MakeEnrich.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Models.Inbound, Domain.Entities.EnrichedOffer>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source.OfferId)
                )
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SellerId)
                )
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(source => source.CategoryId)
                )
                .ForMember(
                    dest => dest.SubcategoryIds,
                    opt => opt.MapFrom(source => ParseSubcategoryIds(source.SubcategoryIds))
                );
        }

        private static IEnumerable<int> ParseSubcategoryIds(IEnumerable<string> subcategoryIds) =>
            subcategoryIds
                .Select(subcategoryId =>
                {
                    var splitedId = $"{subcategoryId}".Split('.');

                    if (splitedId.Length == 0)
                        return default;

                    return splitedId.Length == 1
                        ? int.Parse(splitedId[0])
                        : int.Parse(splitedId[1]);
                })
                .ToArray();
    }
}
