using AutoMapper;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Backend.Infrastructure.Messaging.Mappings
{
    public class AvailabilityChangedMap : Profile
    {
        public AvailabilityChangedMap()
        {
            CreateMap<Domain.Entities.SkuAvailability, AvailabilityMessages.Manager.AvailabilityChanged>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<Domain.ValueObjects.SkuAvailabilityId, AvailabilityMessages.Manager.AvailabilityChanged>()
                .ReverseMap();
        }
    }
}
