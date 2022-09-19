using Search.Worker.Backend.Domain.Services;
using System;
using System.Text;

namespace Search.Worker.Backend.Infrastructure.Hash
{
    public class Base64HashProviderService : IHashProviderService
    {
        public string ComputeHash(object obj) =>
            ComputeHash(ToByteArray(obj));

        public string ComputeHash(byte[] data)
        {
            var hashCodeBase64 = Convert.ToBase64String(data);

            return hashCodeBase64;
        }

        private static byte[] ToByteArray(object obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
