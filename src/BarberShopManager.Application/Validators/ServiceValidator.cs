using BarberShopManager.Communication.Services.Enums;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Exceptions.Resources;
using FluentValidation;

namespace BarberShopManager.Application.Validators;
public class ServiceValidator : AbstractValidator<RequestServiceJson>
{
    public ServiceValidator()
    {
        RuleFor(service => service.ServiceType).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_SERVICE_TYPE);
        RuleFor(service => service.Value).GreaterThan(0).WithMessage(ResourceErrorMessages.VALUE_GREATER_THAN_ZERO);
        RuleFor(service => service.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.SERVICE_CANNOT_FUTURE);
    }
}
