using Product.Change.Worker.Backend.Domain.Services;
using System;

namespace Product.Change.Worker.Backend.Infrastructure.Hash
{
    public class CrcHashProviderService : ICrcHashProviderService
    {
        public string ComputeHash(object obj) =>
            ComputeHash(ToByteArray(obj));
        public string ComputeHash(byte[] data)
        {
            var hashCodeString = Force.Crc32.Crc32Algorithm.Compute(data).ToString();
            var hashCodeBytes = System.Text.Encoding.UTF8.GetBytes(hashCodeString);
            var hashCodeBase64 = Convert.ToBase64String(hashCodeBytes);

            return hashCodeBase64;
        }

        private static byte[] ToByteArray(object obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }
    }
}
