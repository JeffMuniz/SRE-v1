using AutoMapper;
using Availability.Api.Endpoints.Models;
using Usecases = Availability.Api.Backend.Application.UseCases;

namespace Availability.Api.Endpoints.Mappings
{
    public class PartnerAvailabilityMap : Profile
    {
        public PartnerAvailabilityMap()
        {
            CreateMap<Usecases.GetPartnerAvailability.Models.Outbound, PartnerAvailabilityModel>()
                .ReverseMap();
        }
    }
}
