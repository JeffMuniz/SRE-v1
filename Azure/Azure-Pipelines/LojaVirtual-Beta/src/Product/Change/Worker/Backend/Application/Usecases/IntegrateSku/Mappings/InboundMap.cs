using AutoMapper;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Change.Worker.Backend.Application.Usecases.IntegrateSku.Mappings
{
    public class InboundMap : Profile
    {
        public InboundMap()
        {
            CreateMap<Domain.Entities.SupplierSku, Models.Inbound>()
                .IncludeBase<Domain.Entities.SupplierSku, SharedUsecases.Models.SupplierSku>()
                .ReverseMap();
        }
    }
}
