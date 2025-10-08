namespace BarberShopManager.Domain.Entities;
public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long TelephoneNumber { get; set; }

    public Client(string name, long telephoneNumber)
    {
        Name = name;
        TelephoneNumber = telephoneNumber;
    }
}
