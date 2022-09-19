using AutoMapper;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class RemoveSkuFromAvailabilityMap : Profile
    {
        public RemoveSkuFromAvailabilityMap()
        {
            CreateMap<States.SkuState, AvailabilityMessages.Manager.RemoveSku>()
                .IncludeMembers(
                    source => source.SupplierSku
                );

            CreateMap<SharedMessagingModels.SupplierSku, AvailabilityMessages.Manager.RemoveSku>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                );
        }
    }
}
