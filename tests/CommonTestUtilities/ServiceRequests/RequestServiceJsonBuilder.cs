using BarberShopManager.Communication.Services.Enums;
using BarberShopManager.Communication.Services.Request;
using Bogus;

namespace CommonTestUtilities.ServiceRequests;
public class RequestServiceJsonBuilder
{
    public static RequestServiceJson Build()
    {
        return new Faker<RequestServiceJson>()
            .RuleFor(s => s.ServiceType, faker => faker.PickRandom<ServiceType>())
            .RuleFor(s => s.Date, faker => faker.Date.Past())
            .RuleFor(s => s.Value, faker => faker.Random.Double(0.1, 999999999));
            
    }
}
