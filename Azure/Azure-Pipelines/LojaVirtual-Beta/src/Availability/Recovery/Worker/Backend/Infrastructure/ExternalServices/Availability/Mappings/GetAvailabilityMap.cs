using AutoMapper;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using SharedDomain = Shared.Backend.Domain;

namespace Availability.Recovery.Worker.Backend.Infrastructure.ExternalServices.Availability.Mappings
{
    public class GetAvailabilityMap : Profile
    {
        public GetAvailabilityMap()
        {
            CreateMap<Domain.Entities.SkuRecovery, AvailabilityMessages.Search.GetAvailability>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<SharedDomain.ValueObjects.SupplierSkuId, AvailabilityMessages.Search.GetAvailability>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();
        }
    }
}
