namespace BarberShopManager.Application.UseCases.Reports.Excel.GetExcelLastWeek;
public interface IGetExcelLastWeekUseCase
{
    public Task<byte[]> Execute();
}
