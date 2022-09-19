using AutoMapper;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using UsecaseModels = Availability.Manager.Worker.Backend.Application.UseCases.RemoveSku.Models;

namespace Availability.Manager.Worker.Consumers.RemoveSku.Mappings
{
    public class RemoveSkuMap : Profile
    {
        public RemoveSkuMap()
        {
            CreateMap<AvailabilityMessages.Manager.RemoveSku, UsecaseModels.Inbound>()
                .ReverseMap();
        }
    }
}
