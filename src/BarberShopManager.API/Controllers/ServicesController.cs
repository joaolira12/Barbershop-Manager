using BarberShopManager.Application.UseCases.Services.Delete;
using BarberShopManager.Application.UseCases.Services.Register;
using BarberShopManager.Application.UseCases.Services.Update;
using BarberShopManager.Communication.Exceptions;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Communication.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopManager.API.Controllers;
[Route("[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortServiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] IRegisterServiceUseCase useCase, [FromBody] RequestServiceJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
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
