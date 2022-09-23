using CSharpFunctionalExtensions;

namespace Availability.Api.Backend.Domain.ValueObjects
{
    public class ErrorType : EnumValueObject<ErrorType>, IError<string>
    {
        public static readonly ErrorType NotFound = new(nameof(NotFound), "Not found");

        public static readonly ErrorType InvalidInput = new(nameof(InvalidInput), "Invalid input data");

        public static readonly ErrorType ThereIsNoChange = new(nameof(ThereIsNoChange), "No changes");

        public static readonly ErrorType Unexpected = new(nameof(Unexpected), "Erro não esperado");

        public string Error { get; }

        public ErrorType(string id, string error) : base(id)
        {
            Error = error;
        }

        public override string ToString() =>
            $"{Id}|{Error}";
    }
}
