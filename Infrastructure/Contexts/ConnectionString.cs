using Microsoft.Extensions.Configuration;
namespace Infrastructure.Contexts;

internal static class ConnectionString
{
    public static string LoadConnectionString()
    {
        var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
        return configuration.GetConnectionString("DefaultConnection") ?? "";
    }

}
