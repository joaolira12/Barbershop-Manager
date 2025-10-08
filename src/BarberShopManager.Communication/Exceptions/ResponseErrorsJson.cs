namespace BarberShopManager.Communication.Exceptions;
public class ResponseErrorsJson
{
    public List<string> Errors { get; set; } = [];

    public ResponseErrorsJson(List<string> errors)
    {
        Errors = errors;
    }

    public ResponseErrorsJson(string error)
    {
        Errors.Add(error);
    }
}
