
using BarberShopManager.Application.UseCases.Reports.PDF.Colors;
using BarberShopManager.Application.UseCases.Reports.PDF.Fonts;
using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Entities.Enums;
using BarberShopManager.Domain.Reports.Resources;
using BarberShopManager.Domain.Repositories.Clients;
using BarberShopManager.Domain.Repositories.Services;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberShopManager.Application.UseCases.Reports.PDF.GetPdfByWeek;
public class GetPdfByWeekUseCase : IGetPdfByWeekUseCase
{
    private readonly IServiceReadRepository _repository;
    private readonly IClientReadRepository _clientRepository;
    private const int ROW_HEIGHT = 25;

    public GetPdfByWeekUseCase(IServiceReadRepository repository, IClientReadRepository clientRepository)
    {
        _repository = repository;
        _clientRepository = clientRepository;

        GlobalFontSettings.FontResolver = new ReportFontResolver();
    }

    public async Task<byte[]> Execute()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var sevenDaysAgo = today.AddDays(-7);

        var services = await _repository.FilterLastWeek(sevenDaysAgo, today);


        var document = CreateDocument(sevenDaysAgo, today);

        var page = CreatePage(document);


        CreateHeader(page);

        var weeklyRevenue = services.Sum(services => services.Value);

        CreateWeeklyRevenueSection(page, sevenDaysAgo, today, weeklyRevenue);


        foreach (var service in services)
        {
            var client = await _clientRepository.GetClientById(service.ClientId);
            var clientName = client.Name;

            CreateServiceTable(page, service, clientName);
        }

        return RenderDocument(document);
    }


    private Document CreateDocument(DateOnly sevenDaysAgo, DateOnly today)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportMessages.SERVICES_FROM} {sevenDaysAgo.ToString("dd/MM/yyyy")} - {today.ToString("dd/MM/yyyy")}";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.ROBOTO_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 40;
        section.PageSetup.BottomMargin = 40;

        return section;
    }

    private void CreateHeader(Section page)
    {
        var tableHeader = page.AddTable();

        tableHeader.AddColumn("62");
        tableHeader.AddColumn("300");

        var row = tableHeader.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "photo-barbermanager.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph(ResourceReportMessages.JOHNS_BARBERSHOP);
        row.Cells[1].Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 25 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;


    }

    private void CreateWeeklyRevenueSection(Section page, DateOnly sevenDaysAgo, DateOnly today, double weeklyRevenue)
    {
        var paragraph = page.AddParagraph();

        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        paragraph.AddFormattedText($"{ResourceReportMessages.WEEKLY_REVENUE} {sevenDaysAgo.ToString("dd/MM/yyyy")} - {today.ToString("dd/MM/yyyy")}",
        new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"R$ {weeklyRevenue.ToString("F2")}", new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 50 });
    }

    private void CreateServiceTable(Section page, Service service, string clientName)
    {
        var serviceTable = page.AddTable();

        serviceTable.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        serviceTable.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        serviceTable.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        serviceTable.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;


        var row = serviceTable.AddRow();
        row.Height = ROW_HEIGHT;


        AddServiceType(row.Cells[0], service.ServiceType);

        AddClientHeader(row.Cells[2]);

        AddAmountHeader(row.Cells[3]);


        row = serviceTable.AddRow();
        row.Height = ROW_HEIGHT;

        row.Cells[0].AddParagraph(service.Date.ToString("D"));
        SetStyleBaseForServiceInformation(row.Cells[0]);
        row.Cells[0].MergeRight = 1;
        row.Cells[0].Format.LeftIndent = 15;


        row.Cells[2].AddParagraph(clientName);
        SetStyleBaseForServiceInformation(row.Cells[2]);


        AddServiceAmount(row.Cells[3], service.Value);



        if(string.IsNullOrWhiteSpace(service.Observation) == false)
        {
            var observationRow = serviceTable.AddRow();
            observationRow.Height = ROW_HEIGHT;

            AddObservation(observationRow.Cells[0], service.Observation);

        }


        AddWhiteSpace(serviceTable);

    }

    private void SetStyleForServiceHeaders(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 15, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.DARK_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddServiceType(Cell cell, ServiceType serviceType)
    {
        cell.AddParagraph(serviceType.AsString());
        SetStyleForServiceHeaders(cell);
        cell.MergeRight = 1;
        cell.Format.LeftIndent = 15;
    }

    private void AddClientHeader(Cell cell)
    {
        cell.AddParagraph(ResourceReportMessages.CLIENT);
        SetStyleForServiceHeaders(cell);
    }

    private void AddAmountHeader(Cell cell)
    {
        cell.AddParagraph(ResourceReportMessages.VALUE);
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 15, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.LIGHT_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForServiceInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GRAY;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddServiceAmount(Cell cell, double value)
    {
        cell.AddParagraph($"R$ {value.ToString("F2")}");
        cell.Format.Font = new Font { Name = FontHelper.BEBASNEUE_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.RightIndent = 15;
    }

    private void AddObservation(Cell cell, string observation)
    {
        cell.AddParagraph(observation);
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 9, Color = ColorsHelper.DARK_GRAY };
        cell.Shading.Color = ColorsHelper.LIGHT_GRAY;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 15;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = ROW_HEIGHT;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();

    }
}
