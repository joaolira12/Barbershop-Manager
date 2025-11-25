namespace BarberShopManager.Communication.Clients.Request;
public class RequestClientJson
{
    public string Name { get; set; } = string.Empty;
    public long TelephoneNumber { get; set; }

    public RequestClientJson()
    {
    }

    public RequestClientJson(string name, long telephoneNumber)
    {
        Name = name;
        TelephoneNumber = telephoneNumber;
    }

}
