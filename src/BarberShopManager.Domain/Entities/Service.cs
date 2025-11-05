using BarberShopManager.Domain.Entities.Enums;

namespace BarberShopManager.Domain.Entities;
public class Service
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string Observation {  get; set; } = string.Empty;
    public int ClientId { get; set; }

    public Service(ServiceType serviceType, double value, DateTime date, string observation, int clientId)
    {
        ServiceType = serviceType;
        Value = value;
        Date = date;
        Observation = observation;
        ClientId = clientId;
    }
}
