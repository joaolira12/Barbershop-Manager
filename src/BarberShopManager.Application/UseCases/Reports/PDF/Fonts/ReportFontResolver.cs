using PdfSharp.Fonts;
using System.Reflection;

namespace BarberShopManager.Application.UseCases.Reports.PDF.Fonts;
public class ReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        if(stream == null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }

        var lenght = stream!.Length;

        var data = new byte[lenght];

        stream.Read(data, 0, (int)lenght);

        return data;

    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"BarberShopManager.Application.UseCases.Reports.PDF.Fonts.{faceName}.ttf");
    }

}
