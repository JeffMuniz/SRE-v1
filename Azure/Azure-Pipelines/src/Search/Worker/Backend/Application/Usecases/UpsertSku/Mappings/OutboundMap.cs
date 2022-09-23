using AutoMapper;

namespace Search.Worker.Backend.Application.Usecases.UpsertSku.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Domain.Entities.Sku, Models.Outbound>()
                .IncludeBase<Domain.Entities.Sku, Shared.Models.Outbound>()
                .ReverseMap();
        }
    }
}
