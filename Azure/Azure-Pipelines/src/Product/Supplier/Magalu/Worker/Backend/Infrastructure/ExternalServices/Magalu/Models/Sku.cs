using Newtonsoft.Json;
using System;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Models
{
    public class Sku
    {
        [JsonProperty("@CODIGO")]
        public string Code { get; set; }

        [JsonProperty("@MODELO")]
        public string Model { get; set; }

        [JsonProperty("@CATEGORIA")]
        public string Category { get; set; }

        [JsonProperty("@DESC_CATEGORIA")]
        public string CategoryDescription { get; set; }

        [JsonProperty("@SUBCATEGORIA")]
        public string Subcategory { get; set; }

        [JsonProperty("@DESC_SUBCATEGORIA")]
        public string SubcategoryDescription { get; set; }

        [JsonProperty("@REFERENCIA")]
        public string Reference { get; set; }

        [JsonProperty("@DESCRICAO")]
        public string Description { get; set; }

        [JsonProperty("@VOLTAGEM")]
        public int Voltage { get; set; }

        [JsonProperty("@MARCA")]
        public string Brand { get; set; }

        [JsonProperty("@IMAGES")]
        public string Images { get; set; }

        [JsonProperty("@VALOR")]
        public decimal? Price { get; set; }

        [JsonProperty("@VALOR_VENDA")]
        public decimal? SellingPrice { get; set; }

        [JsonProperty("@ATIVO")]
        public bool? Active { get; set; }

        [JsonProperty("@CLASSIFICACAO_FISCAL")]
        public long? TaxClassification { get; set; }

        [JsonProperty("@ACAO")]
        public string Action { get; set; }

        [JsonProperty("@DATA_ALTERACAO")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("@TEM_MONTAGEM")]
        public int? HasAssembly { get; set; }

        [JsonProperty("@FRETE")]
        public decimal? Freight { get; set; }

        [JsonProperty("@MESTRE")]
        public string Master { get; set; }

        [JsonProperty("@TIPO")]
        public string Type { get; set; }

        [JsonProperty("@VIDEO")]
        public string Video { get; set; }
    }
}
