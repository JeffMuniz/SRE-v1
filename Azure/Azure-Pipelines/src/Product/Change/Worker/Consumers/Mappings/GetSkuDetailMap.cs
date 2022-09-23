using AutoMapper;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Change.Worker.Backend.Application.Usecases;

namespace Product.Change.Worker.Consumers.Mappings
{
    public class GetSkuDetailMap : Profile
    {
        public GetSkuDetailMap()
        {
            CreateMap<MessagingContracts.Product.Change.Messages.GetSkuDetail, Usecases.GetSkuDetail.Models.Inbound>()                
                .ReverseMap();

            CreateMap<MessagingContracts.Product.Change.Messages.GetSkuDetailResponse, Usecases.GetSkuDetail.Models.Outbound>()
                .IncludeMembers(source => source.SupplierSku)
                .ReverseMap();

            CreateMap<MessagingContracts.Shared.Models.SupplierSku, Usecases.GetSkuDetail.Models.Outbound>()
                .ReverseMap();
        }
    }
}
