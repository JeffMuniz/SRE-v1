using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<Domain.Entities.SupplierSku, MessagingContracts.Shared.Models.SupplierSku>()
                .IncludeMembers(source => source.Id)
                .ReverseMap();

            CreateMap<SharedDomain.ValueObjects.SupplierSkuId, MessagingContracts.Shared.Models.SupplierSku>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ReverseMap();
        }
    }
}
