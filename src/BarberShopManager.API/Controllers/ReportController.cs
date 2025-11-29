using BarberShopManager.Application.UseCases.Reports.Excel.GetExcelByMonth;
using BarberShopManager.Application.UseCases.Reports.Excel.GetExcelLastWeek;
using BarberShopManager.Application.UseCases.Reports.PDF.GetPdfByWeek;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberShopManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{

    [HttpGet("excel-services-week")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetExcelWeek([FromServices] IGetExcelLastWeekUseCase useCase)
    {
        byte[] file = await useCase.Execute();

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();

    }

    [HttpGet("excel-services-month")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetExcelMonth([FromServices] IGetExcelByMonthUseCase useCase, DateOnly month)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();

    }

    [HttpGet("pdf-services-week")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetPDF([FromServices] IGetPdfByWeekUseCase useCase)
    {
        byte[] file = await useCase.Execute();

        Response.Headers["Content-Disposition"] = "attachment; filename=report.pdf";
        Response.Headers.Remove("Content-Type");

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();

    }

}
