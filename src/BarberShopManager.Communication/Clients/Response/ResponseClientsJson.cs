namespace BarberShopManager.Communication.Clients.Response;
public class ResponseClientsJson
{
    public List<ResponseShortClientJson> Clients { get; set; } = [];

    public ResponseClientsJson(List<ResponseShortClientJson> clients)
    {
        Clients = clients;
    }
}
