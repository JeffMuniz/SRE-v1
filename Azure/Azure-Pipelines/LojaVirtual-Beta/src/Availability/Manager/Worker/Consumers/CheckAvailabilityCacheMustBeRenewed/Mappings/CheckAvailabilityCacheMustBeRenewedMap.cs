using AutoMapper;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using UsecaseModels = Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed.Models;

namespace Availability.Manager.Worker.Consumers.CheckAvailabilityCacheMustBeRenewed.Mappings
{
    public class CheckAvailabilityCacheMustBeRenewedMap : Profile
    {
        public CheckAvailabilityCacheMustBeRenewedMap()
        {
            CreateMap<AvailabilityMessagingContracts.Messages.Manager.CheckAvailabilityCacheMustBeRenewed, UsecaseModels.Inbound>()
                     .ReverseMap();
        }
    }
}
