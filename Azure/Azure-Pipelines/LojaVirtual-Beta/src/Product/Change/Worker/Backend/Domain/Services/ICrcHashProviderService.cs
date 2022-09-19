namespace Product.Change.Worker.Backend.Domain.Services
{
    public interface ICrcHashProviderService
    {
        string ComputeHash(object obj);

        string ComputeHash(byte[] data);
    }
}
