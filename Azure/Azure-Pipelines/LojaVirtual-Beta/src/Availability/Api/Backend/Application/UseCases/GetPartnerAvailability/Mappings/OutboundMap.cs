using AutoMapper;

namespace Availability.Api.Backend.Application.UseCases.GetPartnerAvailability.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {

            CreateMap<Models.Outbound, Domain.Entities.SkuAvailability>()
                .IncludeBase<Shared.Models.Outbound, Domain.Entities.SkuAvailability>()
                .ReverseMap();
        }
    }
}
