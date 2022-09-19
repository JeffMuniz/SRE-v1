using AutoMapper;
using Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Models;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Product.Persistence.Worker.Consumers.AvailabilityChanged.Mappings
{
    public class InboudMap : Profile
    {
        public InboudMap()
        {
            CreateMap<AvailabilityMessaging.Manager.AvailabilityChanged, Inbound>()
                .ReverseMap();
        }
    }
}
