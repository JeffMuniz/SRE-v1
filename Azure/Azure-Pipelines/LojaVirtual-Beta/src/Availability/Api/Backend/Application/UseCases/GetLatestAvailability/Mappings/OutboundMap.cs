using AutoMapper;
using Availability.Api.Endpoints.Models;

namespace Availability.Api.Backend.Application.UseCases.GetLatestAvailability.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {

            CreateMap<Models.Outbound, Domain.Entities.SkuAvailability>()
                .IncludeBase<Shared.Models.Outbound, Domain.Entities.SkuAvailability>()
                .ReverseMap();

            CreateMap<GetPartnerAvailability.Models.Outbound, LatestAvailabilityModel>()
                .ReverseMap();
        }
    }
}
