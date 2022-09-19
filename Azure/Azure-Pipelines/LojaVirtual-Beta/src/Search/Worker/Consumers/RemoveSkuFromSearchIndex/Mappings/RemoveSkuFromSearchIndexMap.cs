using AutoMapper;
using SearchMessages = Shared.Messaging.Contracts.Product.Saga.Messages.Search;
using Usecase = Search.Worker.Backend.Application.Usecases.RemoveSku;

namespace Search.Worker.Consumers.RemoveSkuFromSearchIndex.Mappings
{
    public class RemoveSkuFromSearchIndexMap : Profile
    {
        public RemoveSkuFromSearchIndexMap()
        {
            CreateMap<SearchMessages.RemoveSkuFromSearchIndex, Usecase.Models.Inbound>()
                .ReverseMap();
        }
    }
}
