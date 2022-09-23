using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using AvailabilityShared = Availability.Search.Worker.Backend.Application.UseCases.Shared;

namespace Availability.Search.Worker.Consumers.Shared.Mappings
{
    public class AvailabilityFoundMap : Profile
    {
        public AvailabilityFoundMap()
        {
            CreateMap<AvailabilityShared.Models.AvailabilityFound, AvailabilityMessaging.Search.AvailabilityFound>()
                .ReverseMap();
        }
    }
}
