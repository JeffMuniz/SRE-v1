using AutoMapper;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using UsecaseModels = Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus.Models;

namespace Availability.Manager.Worker.Consumers.GetUnavailableSkus.Mappings
{
    public class GetUnavailableSkusMap : Profile
    {
        public GetUnavailableSkusMap()
        {
            CreateMap<AvailabilityMessagingContracts.Messages.Manager.GetUnavailableSkus, UsecaseModels.Inbound>()
                .ReverseMap();
        }
    }
}
