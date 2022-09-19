using AutoMapper;
using SearchMessages = Shared.Messaging.Contracts.Product.Saga.Messages.Search;
using Usecase = Search.Worker.Backend.Application.Usecases.UpsertSku;

namespace Search.Worker.Consumers.SendSkuToSearchIndex.Mappings
{
    public class SendSkuToSearchIndexMap : Profile
    {
        public SendSkuToSearchIndexMap()
        {
            CreateMap<SearchMessages.SendSkuToSearchIndex, Usecase.Models.Inbound>()
                .ReverseMap();
        }
    }
}
