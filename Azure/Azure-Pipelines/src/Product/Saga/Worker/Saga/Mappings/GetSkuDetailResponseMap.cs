using AutoMapper;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class GetSkuDetailResponseMap : Profile
    {
        public GetSkuDetailResponseMap()
        {
            CreateMap<ChangeMessages.GetSkuDetailResponse, SharedMessagingModels.SupplierSku>()
                .IncludeMembers(source => source.SupplierSku)
                .ReverseMap();
        }
    }
}

