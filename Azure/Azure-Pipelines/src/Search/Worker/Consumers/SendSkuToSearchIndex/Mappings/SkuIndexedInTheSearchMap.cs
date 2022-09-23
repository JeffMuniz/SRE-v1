using AutoMapper;
using SearchMessages = Shared.Messaging.Contracts.Product.Saga.Messages.Search;
using Usecase = Search.Worker.Backend.Application.Usecases.UpsertSku;


namespace Search.Worker.Consumers.SendSkuToSearchIndex.Mappings
{
    public class SkuIndexedInTheSearchMap : Profile
    {
        public SkuIndexedInTheSearchMap()
        {
            CreateMap<Usecase.Models.Outbound, SearchMessages.SkuIndexedInTheSearch>()
                .ReverseMap();
        }
    }
}
