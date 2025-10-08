using BarberShopManager.Communication.Clients.Request;
using BarberShopManager.Exceptions.Resources;
using FluentValidation;

namespace BarberShopManager.Application.Validators;
public class ClientValidator : AbstractValidator<RequestClientJson>
{
    public ClientValidator()
    {
        RuleFor(client => client.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(client => client.TelephoneNumber).Must(TelephoneValidator).WithMessage(ResourceErrorMessages.INVALID_TELEPHONE_NUMBER);
    }

    private bool TelephoneValidator(long telephone)
    {
        int lenght = telephone.ToString().Length;

        if(lenght < 11 || lenght > 11)
        {
            return false;
        }

        return true;
    }
}
