using AutoMapper;
using Availability.Api.Endpoints.Models;
using Usecases = Availability.Api.Backend.Application.UseCases;

namespace Availability.Api.Endpoints.Mappings
{
    public class GetPartnerAvailabilityMap : Profile
    {
        public GetPartnerAvailabilityMap()
        {
            CreateMap<GetPartnerAvailabilityIdModel, Usecases.GetPartnerAvailability.Models.Inbound>()
                .ReverseMap();

            CreateMap<GetPartnerAvailabilityModel, Usecases.GetPartnerAvailability.Models.Inbound>()
                .ReverseMap();


            CreateMap<GetLatestAvailabilityIdModel, Usecases.GetPartnerAvailability.Models.Inbound>()
                .ReverseMap();

            CreateMap<GetLatestAvailabilityModel, Usecases.GetPartnerAvailability.Models.Inbound>()
                .ReverseMap();
        }
    }
}
