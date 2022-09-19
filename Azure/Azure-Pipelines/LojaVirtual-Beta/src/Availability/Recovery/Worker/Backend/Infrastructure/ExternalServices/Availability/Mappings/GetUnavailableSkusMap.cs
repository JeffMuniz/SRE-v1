using AutoMapper;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Recovery.Worker.Backend.Infrastructure.ExternalServices.Availability.Mappings
{
    public class GetUnavailableSkusMap : Profile
    {
        public GetUnavailableSkusMap()
        {
            CreateMap<Domain.ValueObjects.SearchFilter, GetUnavailableSkus>()
                .ReverseMap();
        }
    }
}
