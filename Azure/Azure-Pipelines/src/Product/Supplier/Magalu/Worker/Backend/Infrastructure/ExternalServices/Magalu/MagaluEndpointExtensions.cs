using System;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu
{
    public static class MagaluEndpointExtensions
    {
        public static Uri FormatCursorAndPageSize(this Uri uri, int cursor, int pageSize) =>
            uri
                .Replace("{cursor}", cursor.ToString())
                .Replace("{pageSize}", pageSize.ToString());

        public static Uri FormatProductId(this Uri uri, string productId) =>
            uri.Replace("{productId}", productId);

        private static Uri Replace(this Uri uri, string name, string value) =>
            new(uri.OriginalString.Replace(name, value));
    }
}
