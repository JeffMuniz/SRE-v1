using AutoMapper;
using Availability.Api.Endpoints.Models;
using Usecases = Availability.Api.Backend.Application.UseCases;

namespace Availability.Api.Endpoints.Mappings
{
    public class LatestAvailabilityMap : Profile
    {
        public LatestAvailabilityMap()
        {
            CreateMap<Usecases.GetLatestAvailability.Models.Outbound, LatestAvailabilityModel>()
                .ReverseMap();
        }
    }
}
