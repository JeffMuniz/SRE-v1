using AutoMapper;
using System;

namespace Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Domain.ValueObjects.CategorizationResult, Models.Outbound>()
                .ForMember(
                    dest => dest.SubcategoryId,
                    opt =>
                    {
                        opt.PreCondition(source => source.IsCategorized);
                        opt.MapFrom(source => source.Subcategory.SubcategoryId);
                    }
                )
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SubcategoryProbability, Models.SubcategoryProbability>();
        }
    }
}
