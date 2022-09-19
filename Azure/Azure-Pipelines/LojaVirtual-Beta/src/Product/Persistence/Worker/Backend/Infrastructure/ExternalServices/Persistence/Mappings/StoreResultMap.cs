using AutoMapper;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class StoreResumacap : Profile
    {
        public StoreResumacap()
        {
            CreateMap<Models.Response.StoredResult, Domain.ValueObjects.PersistedData>()
                .ReverseMap();
        }
    }
}
