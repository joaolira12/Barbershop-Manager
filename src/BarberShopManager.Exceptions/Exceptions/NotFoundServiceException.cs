
using System.Net;

namespace BarberShopManager.Exceptions.Exceptions;
public class NotFoundServiceException : BarberShopManagerException
{
    public NotFoundServiceException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErros()
    {
        return new List<string>() { Message };
    }
}
