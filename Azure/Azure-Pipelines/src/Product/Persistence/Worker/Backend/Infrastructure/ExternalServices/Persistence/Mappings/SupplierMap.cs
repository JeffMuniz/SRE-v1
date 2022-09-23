using AutoMapper;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class SupplierMap : Profile
    {
        public SupplierMap()
        {
            CreateMap<Models.Response.Supplier, Domain.ValueObjects.Supplier>()
                .ReverseMap();
        }
    }
}
