using AutoMapper;
using ApplicationModels = Availability.Search.Worker.Backend.Application.UseCases.GetAvailability.Models;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailability.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<ApplicationModels.Inbound, Domain.ValueObjects.AvailabilityId>()
                .ReverseMap();
        }
    }
}
