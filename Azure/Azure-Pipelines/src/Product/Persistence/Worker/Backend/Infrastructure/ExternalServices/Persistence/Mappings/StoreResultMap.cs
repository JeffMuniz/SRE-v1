using AutoMapper;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Mappings
{
    public class StoreResultMap : Profile
    {
        public StoreResultMap()
        {
            CreateMap<Models.Response.StoredResult, Domain.ValueObjects.PersistedData>()
                .ReverseMap();
        }
    }
}
