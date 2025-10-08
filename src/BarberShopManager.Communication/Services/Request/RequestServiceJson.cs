using BarberShopManager.Communication.Services.Enums;
using System.Globalization;

namespace BarberShopManager.Communication.Services.Request;
public class RequestServiceJson
{
    public ServiceType ServiceType { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string Observation {  get; set; } = string.Empty;
    public int ClientId { get; set; }

    public RequestServiceJson(ServiceType serviceType, double value, DateTime date, string observation, int clientId)
    {
        ServiceType = serviceType;
        Value = value;
        Date = date;
        Observation = observation;
        ClientId = clientId;
    }
}
