using BarberShopManager.Application.Validators;
using BarberShopManager.Exceptions.Resources;
using CommonTestUtilities.ClientRequests;
using FluentAssertions;

namespace Validators.Test.Clients;
public class ClientValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ClientValidator();
        var request = RequestClientJsonBuilder.Build();


        var result = validator.Validate(request);


        result.IsValid.Should().BeTrue();

    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void ErrorNameEmpty(string name)
    {
        var validator = new ClientValidator();
        var request = RequestClientJsonBuilder.Build();
        request.Name = name;


        var result = validator.Validate(request);


        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));

    }

    [Theory]
    [InlineData(0)]
    [InlineData((long)0000000000000)]
    public void ErrorTelephoneNumber(long telephoneNumber)
    {
        var validator = new ClientValidator();
        var request = RequestClientJsonBuilder.Build();
        request.TelephoneNumber = telephoneNumber;


        var result = validator.Validate(request);


        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_TELEPHONE_NUMBER));

    }

}
