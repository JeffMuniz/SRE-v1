using CSharpFunctionalExtensions;

namespace Product.Supplier.Shared.Worker.Backend.Domain.ValueObjects
{
    public class ErrorType : EnumValueObject<ErrorType>, IError<string>
    {
        public static readonly ErrorType Unexpected = new(nameof(Unexpected), "Erro não esperado"); 

        public static readonly ErrorType NotFound = new(nameof(NotFound), "Não encontrado");

        public static readonly ErrorType InvalidInput = new(nameof(InvalidInput), "Dados de entrada inválidos");

        public string Error { get; }

        public ErrorType(string id, string error) : base(id)
        {
            Error = error;
        }

        public override string ToString() =>
            $"{Id}|{Error}";
    }
}
