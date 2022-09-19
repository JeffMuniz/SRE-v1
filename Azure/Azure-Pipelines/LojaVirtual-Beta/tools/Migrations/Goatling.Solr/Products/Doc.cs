using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goatling.Solr.Products
{
    public class Doc
    {
        [SolrUniqueKey("ProductSkuId")]
        public string ProductSkuId { get; set; }

        [SolrField("Group")]
        public string Group { get; set; }

        [SolrField("OriginalProductSkuId")]
        public string OriginalProductSkuId { get; set; }

        [SolrField("Name")]
        public string Name { get; set; }

        [SolrField("Description")]
        public string Description { get; set; }

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
        public int BestSellerOrdination { get; set; }

        [SolrField("CreatedDate")]
        public List<DateTime> CreatedDate { get; set; }
    }
}
