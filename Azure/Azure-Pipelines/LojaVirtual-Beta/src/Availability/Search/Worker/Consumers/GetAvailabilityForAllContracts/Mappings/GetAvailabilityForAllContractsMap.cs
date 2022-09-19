using AutoMapper;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using UsecaseModels = Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts.Models;

namespace Availability.Search.Worker.Consumers.GetAvailabilityForAllContracts.Mappings
{
    public class GetAvailabilityForAllContractsMap : Profile
    {
        public GetAvailabilityForAllContractsMap()
        {
            CreateMap<AvailabilityMessaging.Search.GetAvailabilityForAllContracts, UsecaseModels.Inbound>()
                .ReverseMap();

            CreateMap<AvailabilityMessaging.Search.GetAvailabilityForAllContracts, AvailabilityMessaging.Search.AvailabilityFound>()
                .ForMember(
                    dest => dest.PersistedSkuId,
                    opt => opt.MapFrom(source => source.PersistedSkuId)
                )
                .IgnoreForAllOtherMembers()
                .ReverseMap();
        }
    }
}
