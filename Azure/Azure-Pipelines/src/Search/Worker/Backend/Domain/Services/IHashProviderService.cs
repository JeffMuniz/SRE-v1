namespace Search.Worker.Backend.Domain.Services
{
    public interface IHashProviderService
    {
        string ComputeHash(object obj);

        string ComputeHash(byte[] data);
    }
}
