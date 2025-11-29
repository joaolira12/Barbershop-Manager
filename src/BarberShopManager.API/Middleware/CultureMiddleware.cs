using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace BarberShopManager.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        List<CultureInfo> supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultlureInfo = new CultureInfo("en");

        if(string.IsNullOrWhiteSpace(requestCulture) == false && supportedLanguages.Exists(l => l.Name.Equals(requestCulture)))
        {
            cultlureInfo = new CultureInfo(requestCulture);
        }

        CultureInfo.CurrentCulture = cultlureInfo;
        CultureInfo.CurrentUICulture = cultlureInfo;

        await _next(context);
    }
}
