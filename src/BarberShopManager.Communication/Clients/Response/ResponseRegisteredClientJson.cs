namespace BarberShopManager.Communication.Clients.Response;
public class ResponseRegisteredClientJson
{
    public string Name { get; set; } = string.Empty;

    public ResponseRegisteredClientJson()
    {
    }

    public ResponseRegisteredClientJson(string name)
    {
        Name = name;
    }
}
