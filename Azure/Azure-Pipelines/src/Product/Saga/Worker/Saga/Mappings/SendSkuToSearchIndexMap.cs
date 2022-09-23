using AutoMapper;
using System.Linq;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;
using SharedBackendDomain = Shared.Backend.Domain;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class SendSkuToSearchIndexMap : Profile
    {
        public SendSkuToSearchIndexMap()
        {
            CreateMap<States.SkuState, SagaMessages.Search.SendSkuToSearchIndex>()
                .IncludeMembers(
                    source => source.PersistedSku, 
                    source => source.SupplierSku
                )
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom((source, dest) => MapSmallImage(source))
                );

            CreateMap<States.Models.PersistedSku, SagaMessages.Search.SendSkuToSearchIndex>()
                .ForMember(
                    dest => dest.SkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                );

            CreateMap<SharedMessagingModels.SupplierSku, SagaMessages.Search.SendSkuToSearchIndex>()
                .ForMember(
                    dest => dest.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForMember(
                    dest => dest.SupplierSkuId,
                    opt => opt.MapFrom(source => source.SkuId)
                )
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private static SharedMessagingModels.ImageSize MapSmallImage(States.SkuState skuState)
        {
            if (skuState.SupplierSku?.Images
                .OrderBy(x => x.Order)
                .SelectMany(x => x.Sizes)
                .FirstOrDefault(x => x.Size == SharedBackendDomain.ValueObjects.ImageSizeType.Small.Id) is not SharedMessagingModels.ImageSize imageSize)
                return default;

            return new SharedMessagingModels.ImageSize
            {
                Size = SharedBackendDomain.ValueObjects.ImageSizeType.Small.Id,
                Url = imageSize.Url
            };
        }
    }
}

