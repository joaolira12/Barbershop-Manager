using BarberShopManager.Application.UseCases.Clients.Delete;
using BarberShopManager.Application.UseCases.Clients.GetAll;
using BarberShopManager.Application.UseCases.Clients.GetById;
using BarberShopManager.Application.UseCases.Clients.Register;
using BarberShopManager.Application.UseCases.Clients.Update;
using BarberShopManager.Communication.Clients.Request;
using BarberShopManager.Communication.Clients.Response;
using BarberShopManager.Communication.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredClientJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IRegisterClientUseCase useCase, [FromBody] RequestClientJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseShortClientJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllClientsUseCase useCase)
    {
        var response = await useCase.Execute();

        if(response.Clients.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetClientByIdUseCase useCase, [FromRoute] int id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteClientUseCase useCase, [FromRoute] int id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateClientUseCase useCase, [FromRoute] int id, [FromBody] RequestClientJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }


}
