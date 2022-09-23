namespace Shared.Backend.Application.Usecases.Models
{
    public class Error
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public override string ToString() =>
            $"{Code}|{Message}";
    }
}
