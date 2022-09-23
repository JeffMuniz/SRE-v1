using AutoMapper;
using Availability.Api.Endpoints.Models;
using Usecases = Availability.Api.Backend.Application.UseCases;

namespace Availability.Api.Endpoints.Mappings
{
    public class GetLatestAvailabilityMap : Profile
    {
        public GetLatestAvailabilityMap()
        {
            CreateMap<GetLatestAvailabilityIdModel, Usecases.GetLatestAvailability.Models.Inbound>()
                .ReverseMap();

            CreateMap<GetLatestAvailabilityModel, Usecases.GetLatestAvailability.Models.Inbound>()
                .ReverseMap();
        }
    }
}
