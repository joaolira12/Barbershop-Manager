using BarberShopManager.Communication.Clients.Request;
using Bogus;

namespace CommonTestUtilities.ClientRequests;
public class RequestClientJsonBuilder
{
    public static RequestClientJson Build()
    {

       return new Faker<RequestClientJson>()
            .RuleFor(r => r.Name, faker => faker.Name.FullName())
            .RuleFor(r => r.TelephoneNumber, faker => long.Parse(faker.Phone.PhoneNumber("###########")));

    }
}
