using AutoMapper;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using UsecaseModels = Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus.Models;

namespace Availability.Manager.Worker.Consumers.GetUnavailableSkus.Mappings
{
    public class UnavailableSkusResponseMap : Profile
    {
        public UnavailableSkusResponseMap()
        {
            CreateMap<UsecaseModels.Outbound, AvailabilityMessagingContracts.Messages.Manager.UnavailableSkusResponse>()
                .ReverseMap();

            CreateMap<UsecaseModels.UnavailableSku, AvailabilityMessagingContracts.Models.Manager.UnavailableSku>()
                .ReverseMap();
        }
    }
}
