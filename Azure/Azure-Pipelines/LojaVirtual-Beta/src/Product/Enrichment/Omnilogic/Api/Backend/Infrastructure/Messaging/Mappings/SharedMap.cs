using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SharedDomain = Shared.Backend.Domain;
using SharedMessagingContracts = Shared.Messaging.Contracts.Shared;

namespace Product.Enrichment.macnaima.Api.Backend.Infrastructure.Messaging.Mappings
{
    public class SharedMap : Profile
    {
        public SharedMap()
        {
            CreateMap<SharedMessagingContracts.Models.SupplierSku, Domain.Entities.Offer>()
                .ForMember(
                    dest => dest.Images,
                    opt => opt.MapFrom(source => ImagesMap(source.Images))
                )
                .ForMember(
                    dest => dest.ProductAttributes,
                    opt => opt.MapFrom(source => source.Attributes)
                )
                .ForMember(
                    dest => dest.SkuAttributes,
                    opt => opt.Ignore()
                );
        }

        private static IEnumerable<string> ImagesMap(IEnumerable<SharedMessagingContracts.Models.Image> images)
        {
            return images
                .OrderBy(image => image.Order)
                .Select(image =>
                    image.Sizes.FirstOrDefault(imageSize => imageSize.Size == SharedDomain.ValueObjects.ImageSizeType.Large)
                    ?? image.Sizes.FirstOrDefault(imageSize => imageSize.Size == SharedDomain.ValueObjects.ImageSizeType.Medium)
                    ?? image.Sizes.FirstOrDefault(imageSize => imageSize.Size == SharedDomain.ValueObjects.ImageSizeType.Small)
                )
                .OrderBy(imageSize => imageSize?.Url != null)
                .Select(imageSize => imageSize.Url.AbsoluteUri);
        }
    }
}
