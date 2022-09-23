using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration.Mappings
{
    public class IntegrateSku : Profile
    {
        public IntegrateSku()
        {
            CreateMap<Domain.Entities.SupplierSku, MessagingContracts.Product.Change.Messages.IntegrateSku>()
                .ForMember(
                    dest => dest.SupplierSku,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();
        }
    }
}
