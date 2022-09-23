using AutoMapper;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailability.Mappings
{
    public class OutboundMap : Profile
    {
        public OutboundMap()
        {
            CreateMap<Models.Outbound, Domain.Entities.Availability>()
                .IncludeBase<Shared.Models.AvailabilityFound, Domain.Entities.Availability>()
                .ReverseMap();
        }
    }
}
