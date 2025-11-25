using BarberShopManager.Domain.Entities.Enums;
using BarberShopManager.Domain.Reports.Resources;
using BarberShopManager.Domain.Repositories.Services;
using ClosedXML.Excel;

namespace BarberShopManager.Application.UseCases.Reports.Excel.GetExcelLastWeek;
public class GetExcelLastWeekUseCase : IGetExcelLastWeekUseCase
{
    private readonly IServiceReadRepository _repository;

    public GetExcelLastWeekUseCase(IServiceReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var sevenDaysAgo = today.AddDays(-7);

        var services = await _repository.FilterLastWeek(sevenDaysAgo, today);


        using var workbook = new XLWorkbook();

        workbook.Author = "Barbershop Manager";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add($"{sevenDaysAgo.ToString("dd-MM-yyyy")} - {today.ToString("dd-MM-yyyy")}");

        InsertHeader(worksheet);


        int i = 2;

        foreach (var service in services)
        {
            worksheet.Cell($"A{i}").Value = service.ServiceType.AsString();
            worksheet.Cell($"B{i}").Value = service.Observation;
            worksheet.Cell($"C{i}").Value = service.Date;
            worksheet.Cell($"D{i}").Value = service.Value;

            worksheet.Cell($"D{i}").Style.NumberFormat.Format = $"+R$ #,##0.00";
            i++;
        }

        worksheet.Columns().AdjustToContents();


        var filePath = new MemoryStream();
        workbook.SaveAs(filePath);

        return filePath.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportMessages.SERVICE;
        worksheet.Cell("B1").Value = ResourceReportMessages.OBSERVATION;
        worksheet.Cell("C1").Value = ResourceReportMessages.DATE;
        worksheet.Cell("D1").Value = ResourceReportMessages.VALUE;

        worksheet.Cells("A1:D1").Style.Font.Bold = true;

        worksheet.Cells("A1:D1").Style.Fill.BackgroundColor = XLColor.FromHtml("#483D8B");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

    }

}
