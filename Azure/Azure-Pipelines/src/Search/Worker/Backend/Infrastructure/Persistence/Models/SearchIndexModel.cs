using SolrNet.Attributes;
using System;
using System.Collections.Generic;

namespace Search.Worker.Backend.Infrastructure.Persistence.Models
{
    public class SearchIndexModel
    {
        [SolrUniqueKey]
        public string ProductSkuId { get; set; }

        [SolrField]
        public string Group { get; set; }

        [SolrField]
        public string OriginalProductSkuId { get; set; }

        [SolrField]
        public string Name { get; set; }

        [SolrField]
        public string Description { get; set; }

        [SolrField("EAN")]
        public string Ean { get; set; }

        [SolrField]
        public long CategoryId { get; set; }

        [SolrField]
        public string Category { get; set; }

        [SolrField]
        public long SubCategoryId { get; set; }

        [SolrField]
        public string Subcategory { get; set; }

        [SolrField]
        public long BrandId { get; set; }

        [SolrField]
        public string Brand { get; set; }

        [SolrField]
        public string PartnerId { get; set; }

        [SolrField]
        public string Partner { get; set; }

        [SolrField]
        public int PartnerType { get; set; }

        [SolrField]
        public bool Available { get; set; }

        [SolrField]
        public float PriceFor { get; set; }

        [SolrField]
        public string Image { get; set; }

        [SolrField]
        public string Relevance { get; set; }

        [SolrField]
        public string KeyWord { get; set; }

        [SolrField]
        public int Type { get; set; }

        [SolrField]
        public string ServicePath { get; set; }

        [SolrField]
        public IEnumerable<string> Features { get; set; }

        [SolrField]
        public DateTime CreatedDate { get; set; }
    }
}
