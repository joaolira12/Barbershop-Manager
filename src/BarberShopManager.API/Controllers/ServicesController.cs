using BarberShopManager.Application.UseCases.Services.Delete;
using BarberShopManager.Application.UseCases.Services.GetByClient;
using BarberShopManager.Application.UseCases.Services.GetById;
using BarberShopManager.Application.UseCases.Services.GetByMonth;
using BarberShopManager.Application.UseCases.Services.Register;
using BarberShopManager.Application.UseCases.Services.Update;
using BarberShopManager.Communication.Exceptions;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Communication.Services.Response;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortServiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IRegisterServiceUseCase useCase, [FromBody] RequestServiceJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseServiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromServices] IGetServiceByIdUseCase useCase, [FromRoute] int id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet]
    [Route("client/{clientId}")]
    [ProducesResponseType(typeof(ResponseServicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByClientId([FromServices] IGetServicesByClientIdUseCase useCase, [FromRoute] int clientid)
    {
        var response = await useCase.Execute(clientid);

        if(response.Services.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseServicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByMonth([FromServices] IGetServicesByMonthUseCase useCase, [FromHeader] DateOnly date)
    {
        var response = await useCase.Execute(date);

        if(response.Services.Count == 0)
        {
            return NoContent();
        }


        return Ok(response);
    }


    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteServiceUseCase useCase, [FromRoute] int id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateServiceUseCase useCase, [FromRoute] int id, [FromBody] RequestServiceJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
