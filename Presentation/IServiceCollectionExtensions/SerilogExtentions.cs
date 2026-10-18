using Serilog;

namespace Presentation.IServiceCollectionExtensions;

public static class SerilogExtentions
{
  public static void AddSerilogConfiguration(this ConfigureHostBuilder hostBuilder)
  {
    //Add this in the appsettings.Development.json file
    //"Serilog": {
    //    "MinimumLevel": {
    //        "Override": {
    //            "Microsoft.EntityFrameworkCore": "Information",
    //            "Microsoft": "Information"
    //        }
    //    },
    //     "WriteTo": [
    //       {
    //                 "Name": "Console",
    //                 "Args": {
    //                     "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    //                 }
    //             },
    //       {
    //                 "Name": "File",
    //                 "Args": {
    //                     "path": "logs/{Date}.txt",
    //                     "rollingInterval": "Day"
    //                 }
    //             }
    //     ]
    //   },
    //Add this package Serilog.AspNetCore
    //Add this lins on the Program.cs to use the serilog configuration
    //builder.Host.AddSerilogConfiguration();
    //app.UseSerilogRequestLogging();
    //now we can change this based on requirements from appsettings.Development.json and appsettings.Production.json
    hostBuilder.UseSerilog(
      (context, configuration)
      => configuration.ReadFrom.Configuration(context.Configuration)
  //   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
  //   .WriteTo.File("logs/{Date}.txt", rollingInterval: RollingInterval.Day)
  //   .WriteTo.Console(
  //       outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
  //   )
  );
  }

}
