
using System.Net;

namespace BarberShopManager.Exceptions.Exceptions;
public class ErrorOnValidationClientException : BarberShopManagerException
{
    private readonly List<string> _errors;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationClientException(List<string> errors)
    {
        _errors = errors;
    }

    public override List<string> GetErros()
    {
        return _errors;
    }
}
