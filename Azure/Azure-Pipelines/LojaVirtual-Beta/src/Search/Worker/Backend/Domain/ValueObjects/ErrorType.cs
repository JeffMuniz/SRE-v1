using CSharpFunctionalExtensions;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class ErrorType : EnumValueObject<ErrorType>, IError<string>
    {
        public static readonly ErrorType NotFound = new(nameof(NotFound), "Não encontrado");

        public static readonly ErrorType InvalidInput = new(nameof(InvalidInput), "Dados de entrada inválidos");

        public static readonly ErrorType IgnoreInput = new(nameof(IgnoreInput), "Dados de entrada devem ser ignorados");

        public static readonly ErrorType ThereIsNoChange = new(nameof(ThereIsNoChange), "Não há alterações");

        public static readonly ErrorType FailureOnPersist = new(nameof(FailureOnPersist), "Não foi possível salvar o documento no indice de busca");

        public static readonly ErrorType FailureOnDeleting = new(nameof(FailureOnDeleting), "Não foi possível excluir o documento no indice de busca");

        public string Error { get; }

        public ErrorType(string id, string error) : base(id)
        {
            Error = error;
        }

        public override string ToString() =>
            $"{Id}|{Error}";
    }
}
