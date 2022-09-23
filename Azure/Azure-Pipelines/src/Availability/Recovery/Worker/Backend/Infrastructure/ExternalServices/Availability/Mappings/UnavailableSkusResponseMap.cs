using AutoMapper;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using Shared.Messaging.Contracts.Availability.Models.Manager;
using AvailabilityMessageContracts = Shared.Messaging.Contracts.Availability;
using SharedDomain = Shared.Backend.Domain;

namespace Availability.Recovery.Worker.Backend.Infrastructure.ExternalServices.Availability.Mappings
{
    public class UnavailableSkusResponseMap : Profile
    {
        public UnavailableSkusResponseMap()
        {
            CreateMap<UnavailableSkusResponse, Domain.ValueObjects.PagedRecoverySkus>()
                .ReverseMap();

            CreateMap<UnavailableSku, Domain.Entities.SkuRecovery>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<UnavailableSku, SharedDomain.ValueObjects.SupplierSkuId>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ReverseMap();
        }
    }
}
