using BarberShopManager.Application.UseCases.Services.Delete;
using BarberShopManager.Application.UseCases.Services.GetByClient;
using BarberShopManager.Application.UseCases.Services.GetById;
using BarberShopManager.Application.UseCases.Services.GetByMonth;
using BarberShopManager.Application.UseCases.Services.GetByWeek;
using BarberShopManager.Application.UseCases.Services.Register;
using BarberShopManager.Application.UseCases.Services.Update;
using BarberShopManager.Communication.Exceptions;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Communication.Services.Response;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BarberShopManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredServiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IRegisterServiceUseCase useCase, [FromBody] RequestServiceJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseServiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetServiceByIdUseCase useCase, [FromRoute] int id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet]
    [Route("client/{clientId}")]
    [ProducesResponseType(typeof(ResponseServicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByClientId([FromServices] IGetServicesByClientIdUseCase useCase, [FromRoute] int clientId)
    {
        var response = await useCase.Execute(clientId);

        if(response.Services.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseServicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetByMonth([FromServices] IGetServicesByMonthUseCase useCase, [FromHeader] DateOnly month)
    {

        var response = await useCase.Execute(month);

        if(response.Services.Count == 0)
        {
            return NoContent();
        }


        return Ok(response);
    }


    [HttpGet("last-week")]
    [ProducesResponseType(typeof(ResponseServicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetLastWeek([FromServices] IGetServicesLastWeekUseCase useCase)
    {

        var response = await useCase.Execute();

        if (response.Services.Count == 0)
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
