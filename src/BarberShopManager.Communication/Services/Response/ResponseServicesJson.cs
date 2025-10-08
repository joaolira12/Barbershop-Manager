namespace BarberShopManager.Communication.Services.Response;
public class ResponseServicesJson
{
    public List<ResponseShortServiceJson> Services { get; set; } = [];

    public ResponseServicesJson(List<ResponseShortServiceJson> services)
    {
        Services = services;
    }
}
