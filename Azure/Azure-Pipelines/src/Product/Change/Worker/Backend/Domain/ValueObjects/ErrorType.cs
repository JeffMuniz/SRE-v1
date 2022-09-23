using CSharpFunctionalExtensions;

namespace Product.Change.Worker.Backend.Domain.ValueObjects
{
    public class ErrorType : EnumValueObject<ErrorType>, IError<string>
    {
        public static readonly ErrorType NotFound = new(nameof(NotFound), "Não encontrado");

        public static readonly ErrorType InvalidInput = new(nameof(InvalidInput), "Dados de entrada inválidos");

        public static readonly ErrorType ThereIsNoChange = new(nameof(ThereIsNoChange), "Não há alterações");

        public string Error { get; }

        public ErrorType(string id, string error) : base(id)
        {
            Error = error;
        }

        public override string ToString() =>
            $"{Id}|{Error}";
    }
}
