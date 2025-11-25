namespace BarberShopManager.Application.UseCases.Reports.PDF.GetPdfByWeek;
public interface IGetPdfByWeekUseCase
{
    public Task<byte[]> Execute();
}
