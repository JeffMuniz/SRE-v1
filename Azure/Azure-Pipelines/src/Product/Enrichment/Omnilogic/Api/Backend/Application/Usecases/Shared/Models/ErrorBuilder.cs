using Shared.Backend.Application.Usecases.Models;

namespace Product.Enrichment.Macnaima.Api.Backend.Application.Usecases.Shared.Models
{
    public static class ErrorBuilder
    {
        public static Error CreateRegisterNotFound(string message)
            => Create(Codes.RegisterNotFound, message);

        public static Error CreateInvalidBusinessRule(string message)
            => Create(Codes.InvalidBusinessRule, message);

        public static Error Create(Codes code, string message)
            => new() { Code = code.ToString(), Message = message };

        public enum Codes
        {
            RegisterNotFound = 100,
            InvalidBusinessRule = 200,
            UnexpectedResult = 0
        }
    }
}
