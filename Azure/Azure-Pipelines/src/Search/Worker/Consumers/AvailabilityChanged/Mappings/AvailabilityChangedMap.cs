using AutoMapper;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using Usecase = Search.Worker.Backend.Application.Usecases.UpdateSkuAvailability;

namespace Search.Worker.Consumers.AvailabilityChanged.Mappings
{
    public class AvailabilityChangedMap : Profile
    {
        public AvailabilityChangedMap()
        {
            CreateMap<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged, Usecase.Models.Inbound>()
                .ReverseMap();
        }
    }
}
