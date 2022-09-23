using AutoMapper;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Consumers.RemoveSku.Mappings
{
    public class SkuRemovedFromAvailabilityMap : Profile
    {
        public SkuRemovedFromAvailabilityMap()
        {
            CreateMap<AvailabilityMessages.Manager.RemoveSku, AvailabilityMessages.Manager.SkuRemovedFromAvailability>()
                    .ReverseMap();
        }
    }
}
