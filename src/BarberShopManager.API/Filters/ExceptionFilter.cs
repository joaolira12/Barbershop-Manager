using BarberShopManager.Communication.Exceptions;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberShopManager.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is BarberShopManagerException)
        {
            HundleProjectException(context);
        } 
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HundleProjectException(ExceptionContext context)
    {
        var barberShopManagerException = (BarberShopManagerException)context.Exception;
        ResponseErrorsJson errorResponse = new ResponseErrorsJson(barberShopManagerException.GetErros());

        context.HttpContext.Response.StatusCode = barberShopManagerException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        ResponseErrorsJson errorResponse = new ResponseErrorsJson(ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
