using BarberShopManager.Communication.Services.Enums;

namespace BarberShopManager.Communication.Services.Response;
public class ResponseShortServiceJson
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string CientName { get; set; }

    public ResponseShortServiceJson(int id, ServiceType serviceType, double value, DateTime date, string cientName)
    {
        Id = id;
        ServiceType = serviceType;
        Value = value;
        Date = date;
        CientName = cientName;
    }
}
