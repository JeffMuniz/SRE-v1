using AutoMapper;
using System.Linq;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessagingModels = Shared.Messaging.Contracts.Shared.Models;

namespace Product.Saga.Worker.Saga.Mappings
{
    public class UpdateSkuEnrichedMap : Profile
    {
        public UpdateSkuEnrichedMap()
        {
            CreateMap<SagaMessages.Enrichment.UpdateSkuEnriched, States.SkuState>()
                .ForMember(
                    dest => dest.SkuIntegrationId,
                    opt => opt.MapFrom(source => source.SkuIntegrationId)
                )
                .ForPath(
                    dest => dest.SupplierSku.SupplierId,
                    opt => opt.MapFrom(source => source.SupplierId)
                )
                .ForPath(
                    dest => dest.SupplierSku.SkuId,
                    opt => opt.MapFrom(source => source.SupplierSkuId)
                )
                .ForMember(
                    dest => dest.EnrichedData,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<SagaMessages.Enrichment.UpdateSkuEnriched, SharedMessagingModels.EnrichedData>()
                .ForMember(
                    dest => dest.Product,
                    opt => opt.MapFrom(source => source)
                )
                .ForMember(
                    dest => dest.Sku,
                    opt => opt.MapFrom(source => source)
                )
                .ReverseMap();

            CreateMap<SagaMessages.Enrichment.UpdateSkuEnriched, SharedMessagingModels.EnrichedProduct>()
                .ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(source => source.ProductName)
               )
               .ForMember(
                   dest => dest.Hash,
                   opt => opt.MapFrom(source => source.ProductHash)
               )
               .ForMember(
                   dest => dest.SubcategoryId,
                   opt => opt.MapFrom((source, dest) => source.SubcategoryIds.FirstOrDefault())
               )
               .ForMember(
                   dest => dest.Attributes,
                   opt => opt.MapFrom((source, dest) => source.Metadata.Where(x => !source.SkuMetadata.Contains(x.Key)))
               )
               .ReverseMap();

            CreateMap<SagaMessages.Enrichment.UpdateSkuEnriched, SharedMessagingModels.EnrichedSku>()
               .ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(source => source.SkuName)
               )
               .ForMember(
                   dest => dest.Hash,
                   opt => opt.MapFrom(source => source.SkuHash)
               )
               .ForMember(
                   dest => dest.Attributes,
                   opt => opt.MapFrom((source, dest) => source.Metadata.Where(x => source.SkuMetadata.Contains(x.Key)))
               )               
               .ReverseMap();
        }
    }
}

