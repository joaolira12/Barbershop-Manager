using BarberShopManager.Application.Validators;
using BarberShopManager.Communication.Services.Enums;
using BarberShopManager.Exceptions.Resources;
using CommonTestUtilities.ServiceRequests;
using FluentAssertions;

namespace Validators.Test.Services;
public class ServiceValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ServiceValidator();
        var request = RequestServiceJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ErrorInvalidServiceType()
    {
        var validator = new ServiceValidator();
        var request = RequestServiceJsonBuilder.Build();
        request.ServiceType = (ServiceType)12;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.INVALID_SERVICE_TYPE));
    }

    [Fact]
    public void ErrorDateFuture()
    {
        var validator = new ServiceValidator();
        var request = RequestServiceJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.SERVICE_CANNOT_FUTURE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-12)]
    public void ErrorValueGreaterThanZero(double value)
    {
        var validator = new ServiceValidator();
        var request = RequestServiceJsonBuilder.Build();
        request.Value = value;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.VALUE_GREATER_THAN_ZERO));
    }

}
