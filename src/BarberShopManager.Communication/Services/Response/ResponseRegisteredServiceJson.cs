using BarberShopManager.Communication.Services.Enums;

namespace BarberShopManager.Communication.Services.Response;
public class ResponseRegisteredServiceJson
{
    public ServiceType ServiceType { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }


    public ResponseRegisteredServiceJson(ServiceType serviceType, double value, DateTime date)
    {
        ServiceType = serviceType;
        Value = value;
        Date = date;
    }
}
