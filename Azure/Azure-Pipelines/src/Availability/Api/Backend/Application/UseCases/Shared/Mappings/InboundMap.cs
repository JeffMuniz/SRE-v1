using AutoMapper;

namespace Availability.Api.Backend.Application.UseCases.Shared.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Domain.ValueObjects.SkuAvailabilitySearchFilter, Models.Inbound>()
                .IncludeMembers(source => source.SkuAvailabilityId)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, Models.Inbound>()
                .ReverseMap();
        }
    }
}
