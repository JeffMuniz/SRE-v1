using AutoMapper;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using UsecaseModels = Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability.Models;

namespace Availability.Manager.Worker.Consumers.GetLatestAvailability.Mappings
{
    public class GetLatestAvailabilityMap : Profile
    {
        public GetLatestAvailabilityMap()
        {
            CreateMap<AvailabilityMessagingContracts.Messages.Manager.GetLatestAvailability, UsecaseModels.Inbound>()
                .ReverseMap();

            CreateMap<UsecaseModels.Outbound, AvailabilityMessagingContracts.Messages.Manager.GetLatestAvailabilityResponse>()
                .ReverseMap();
        }
    }
}
