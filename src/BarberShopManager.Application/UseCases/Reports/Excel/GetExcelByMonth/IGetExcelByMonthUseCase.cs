namespace BarberShopManager.Application.UseCases.Reports.Excel.GetExcelByMonth;
public interface IGetExcelByMonthUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
