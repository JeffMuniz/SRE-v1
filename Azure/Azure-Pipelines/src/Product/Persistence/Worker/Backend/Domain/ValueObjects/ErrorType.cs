using CSharpFunctionalExtensions;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class ErrorType : EnumValueObject<ErrorType>, IError<string>
    {
        public static readonly ErrorType NotFound = new(nameof(NotFound), "Não encontrado");

        public static readonly ErrorType InvalidInput = new(nameof(InvalidInput), "Dados de entrada inválidos");

        public static readonly ErrorType IgnoreInput = new(nameof(IgnoreInput), "Dados de entrada devem ser ignorados");

        public string Error { get; }

        public ErrorType(string id, string error) : base(id)
        {
            Error = error;
        }

        public override string ToString() =>
            $"{Id}|{Error}";
    }
}
