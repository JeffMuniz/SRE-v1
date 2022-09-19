using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Product.Change.Worker.Backend.Domain.ValueObjects
{
    public class SupplierSkuHash : ValueObject
    {
        public string Value { get; init; }

        public static SupplierSkuHash Create(Entities.SupplierSku supplierSku, Services.ICrcHashProviderService crcHashProviderService) =>
            new()
            {
                Value = crcHashProviderService.ComputeHash(GetHashElements(supplierSku))
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() =>
            $"{Value}";

        private static IEnumerable<object> GetHashElements(Entities.SupplierSku supplierSku)
        {
            yield return supplierSku?.Id;
            yield return supplierSku?.ProductId;
            yield return supplierSku?.Name;
            yield return supplierSku?.Description;
            yield return supplierSku?.Ean;
            yield return supplierSku?.Url;
            yield return supplierSku?.Subcategory;
            yield return supplierSku?.Subcategory?.Name;
            yield return supplierSku?.Subcategory?.Category;
            yield return supplierSku?.Subcategory?.Category?.Name;
            yield return supplierSku?.Brand;
            yield return supplierSku?.Brand?.Name;

            foreach (var image in supplierSku?.Images.DefaultIfNull())
            {
                yield return image;
                yield return image?.Sizes;
            }

            foreach (var attribute in supplierSku?.Attributes.DefaultIfNull())
            {
                yield return attribute.Key;
                yield return attribute.Value;
            }
        }        

        public static implicit operator string(SupplierSkuHash supplierSkuHash)
            => supplierSkuHash?.Value;

        public static implicit operator SupplierSkuHash(string hash)
            => new() { Value = hash };
    }
}
