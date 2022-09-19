using AutoMapper;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Mappings
{
    public class SupplierSkuMap : Profile
    {
        public SupplierSkuMap()
        {
            CreateMap<Domain.Entities.SupplierSku, Entities.SupplierSku>()
               .IncludeMembers(source => source.Id)
               .ReverseMap();

            CreateMap<SharedDomain.ValueObjects.SupplierSkuId, Entities.SupplierSku>()
                .ReverseMap();            
        }
    }
}
