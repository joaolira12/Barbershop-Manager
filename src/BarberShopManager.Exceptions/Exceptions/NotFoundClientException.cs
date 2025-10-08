
using System.Net;

namespace BarberShopManager.Exceptions.Exceptions;
public class NotFoundClientException : BarberShopManagerException
{
    public NotFoundClientException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErros()
    {
        return new List<string>() { Message };
    }
}
