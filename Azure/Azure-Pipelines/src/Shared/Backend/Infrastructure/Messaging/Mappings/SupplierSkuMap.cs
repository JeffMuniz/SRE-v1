using AutoMapper;
using SharedDomain = Shared.Backend.Domain;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;

namespace Shared.Backend.Infrastructure.Messaging.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<SharedDomain.ValueObjects.SupplierSkuId, SharedMessagingContracts.Models.SupplierSku>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .IgnoreForAllOtherMembers()
                .ReverseMap();
        }
    }
}
