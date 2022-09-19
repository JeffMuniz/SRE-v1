using AutoMapper;
using Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Models;
using PersistenceMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;

namespace Product.Persistence.Worker.Consumers.RemoveSku.Mappings
{
    public class RemoveSkuMap : Profile
    {
        public RemoveSkuMap()
        {
            CreateMap<PersistenceMessaging.RemoveSku, Inbound>()
                .ReverseMap();
        }
    }
}
