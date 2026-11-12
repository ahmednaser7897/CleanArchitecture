
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Presentation.IServiceCollectionExtensions;
//https://www.codemag.com/Article/2009081/A-Deep-Dive-into-ASP.NET-Core-Localization
public static class LocalizationExtentions
{
    public static void AddCustomLocalization(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddLocalization(opt => opt.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCultures =
        [
            new("en-US"),
            new("ar-EG")
        ];

            options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

    }

}

