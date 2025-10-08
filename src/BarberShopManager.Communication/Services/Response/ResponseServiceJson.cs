using BarberShopManager.Communication.Services.Enums;

namespace BarberShopManager.Communication.Services.Response;
public class ResponseServiceJson
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string Observation {  get; set; } = string.Empty;
    public string CientName { get; set; } = string.Empty;

    public ResponseServiceJson(int id, ServiceType serviceType, double value, DateTime date, string observation, string cientName)
    {
        Id = id;
        ServiceType = serviceType;
        Value = value;
        Date = date;
        Observation = observation;
        CientName = cientName;
    }
}
