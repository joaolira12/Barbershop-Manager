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
    //public Client Client { get; set; }

    public Service(int id, ServiceType serviceType, double value, DateTime date, string observation, int clientId/*, Client client*/)
    {
        Id = id;
        ServiceType = serviceType;
        Value = value;
        Date = date;
        Observation = observation;
        ClientId = clientId;
        /*Client = client;*/
    }
}
