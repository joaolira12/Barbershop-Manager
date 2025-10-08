namespace BarberShopManager.Exceptions.Exceptions;
public abstract class BarberShopManagerException : ApplicationException
{
    protected BarberShopManagerException()
    {
    }

    protected BarberShopManagerException(string? message) : base(message)
    {
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErros();
}
