using SolrNet.Attributes;
using System;
using System.Collections.Generic;

namespace Goatling.Solr.Enriched
{
    public class SearchIndexModel
    {
        public SearchIndexModel()
        {
        }

        [SolrUniqueKey("ProductSkuId")]
        public string ProductSkuId { get; set; }

        [SolrField("OriginalProductSkuId")]
        public string OriginalProductSkuId { get; set; }

        [SolrField("Name")]
        public string Name { get; set; }

        [SolrField("Description")]
        public string Description { get; set; }

        [SolrField("Available")]
        public bool Available { get; set; }

        [SolrField("Group")]
        public string Group { get; set; }

        [SolrField("EAN")]
        public string EAN { get; set; }

        [SolrField("CategoryId")]
        public long CategoryId { get; set; }

        [SolrField("Category")]
        public string Category { get; set; }

        [SolrField("SubCategoryId")]
        public long SubCategoryId { get; set; }

        [SolrField("Subcategory")]
        public string Subcategory { get; set; }

        [SolrField("BrandId")]
        public string BrandId { get; set; }

        [SolrField("Brand")]
        public string Brand { get; set; }

        [SolrField("PartnerId")]
        public long PartnerId { get; set; }

        [SolrField("PartnerType")]
        public int PartnerType { get; set; }

        [SolrField("Partner")]
        public string Partner { get; set; }

        [SolrField("PriceFor")]
        public float PriceFor { get; set; }

        [SolrField("Image")]
        public string Image { get; set; }

        [SolrField("Relevance")]
        public string Relevance { get; set; }

        [SolrField("KeyWord")]
        public string KeyWord { get; set; }

        [SolrField("Type")]
        public int Type { get; set; }

        [SolrField("ServicePath")]
        public string ServicePath { get; set; }

        [SolrField("Features")]
        public List<string> Features { get; set; }

        [SolrField("BestSellerOrdination")]
        public long BestSellerOrdination { get; set; }

        [SolrField("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        private SearchIndexModel(
            string group,
            string productSkuId,
            string originalProductSkuId,
            string name,
            string description,
            string ean,
            long categoryId,
            string category,
            long subCategoryId,
            string subcategory,
            string brandId,
            string brand,
            long partnerId,
            string partner,
            float priceFor,
            string image,
            string relevance,
            string keyWord,
            List<string> features,
            DateTime createdDate,
            int bestOrdination
        )
        {
            Group = group;
            Available = true;
            ProductSkuId = productSkuId;
            OriginalProductSkuId = originalProductSkuId;
            Name = name;
            Description = description;
            EAN = ean;
            CategoryId = categoryId;
            Category = category;
            SubCategoryId = subCategoryId;
            Subcategory = subcategory;
            BrandId = brandId;
            Brand = brand;
            PartnerId = partnerId;
            PartnerType = (int)Shared.PartnerType.Ecommerce;
            Partner = partner;
            PriceFor = priceFor;
            Image = image;
            Relevance = relevance;
            KeyWord = keyWord;
            Type = (int)Shared.ProductType.Product;
            Features = features;
            CreatedDate = createdDate;
            BestSellerOrdination = bestOrdination;
        }

        public static SearchIndexModel Create(
            string group,
            string productSkuId,
            string originalProductSkuId,
            string name,
            string description,
            string ean,
            long categoryId,
            string category,
            long subCategoryId,
            string subcategory,
            string brandId,
            string brand,
            long partnerId,
            string partner,
            float priceFor,
            string image,
            string relevance,
            string keyWord,
            List<string> features,
            DateTime createdDate,
            int bestSellerOrdination
        ) => new SearchIndexModel(
            group,
            productSkuId,
            originalProductSkuId,
            name,
            description,
            ean,
            categoryId,
            category,
            subCategoryId,
            subcategory,
            brandId,
            brand,
            partnerId,
            partner,
            priceFor,
            image,
            relevance,
            keyWord,
            features,
            createdDate,
            bestSellerOrdination
        );
    }
}
