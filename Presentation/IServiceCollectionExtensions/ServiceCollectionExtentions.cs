using Application.DTOs.Options;
using Application.UseCases.Courses.Commands;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Presentation.Filters;

namespace Presentation.IServiceCollectionExtensions;


public static class ServiceCollectionExtentions
{
    public static void AddAppDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container (Inversion of Control).
        //see: DotnetAPIIdentityAuthentication/DependencyInjection.cs
        // we have 3 types of servises
        // 1- Framwork Services: already dclared and registered in the container:ex:ILogger,IConfiguration;
        // 2- Built in servises : already dclared but not registered in the container :ex:AddDbContext,AddIdentity,AddJWTAuthentication,AddCors;
        // 3- Custom Servises :  not dclared and not registered in the container :ex:IEmployeeRepository,IDepartmentRepository;
        services.AddAppConfigurations(configuration);
        services.AddAppControllers();
        services.AddSwaggerWithJWTAuth();
        services.AddAppDbContext(configuration);
        services.AddAppIdentity();
        services.AddAppCors();
        services.AddAppCustomServises();
        services.AddJWTAuthentication(configuration);
    }
    private static void AddAppConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        //The ASP.NET Has many prddifainde Sources to read main configurations from it
        //like appsettings.json ,Environment Variables , user secrets,Command Line Arguments,Windows Registry
        //we can add our own configuration source like :
        //-Configuration.json
        //in program.cs file:
        //builder.Configuration.AddJsonFile("Configuration.json");
        //then we can read from it like this:
        //_configuration["TestKey"];
        //this file will override all the previous configuration sources
        //builder.Configuration.AddJsonFile("Configuration.json");
        //----------------------------------------------------
        //read Attachment section in the appsettings.json file and bind it to AttachmentOptions class
        //var attachmentOptions = builder.Configuration.GetSection("Attachment").Get<AttachmentOptions>();
        //Inject AttachmentOptions class in the container
        //builder.Services.AddSingleton<AttachmentOptions>(attachmentOptions!);
        //this is th recommended way to bind configuration to the class
        //it add the class to the container and we can inject it in the constructor using 
        //1- IOptions<AttachmentOptions>
        //2- IOptionSnapshot<AttachmentOptions>
        //3- IOptionMonitor<AttachmentOptions>
        services.Configure<JwtOptions>(configuration.GetSection(MyAppConstants.JwtConfigSectionName));
    }

    private static void AddAppControllers(this IServiceCollection services)
    {
        //builder.Services.AddControllers();
        // Disable the automatic validation of the model state 
        // which returns 400 Bad Request
        // and enable the custom validation.
        services.AddControllers(options => options.Filters.Add<ValidateModelAttribute>())
          .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    }
    private static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        //for entity framework (DbContext)
        //add DbContext to the container
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString(MyAppConstants.DefaultConnection))
        );
    }
    private static void AddAppCors(this IServiceCollection services)
    {
        //Add this in the Program.cs before app.Run();
        // app.UseCors(MyAppConstants.CorsAllowAllPolicy);

        // this line add the cors policy to the container and this enable cross-origin requests
        // from the frontend (Angular in this case) to the backend (ASP.NET Core API).
        services.AddCors(options =>
        {
            options.AddPolicy(MyAppConstants.CorsAllowAllPolicy, policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });
    }
    private static void AddAppCustomServises(this IServiceCollection services)
    {
        //add custom services to the container
        //Custom Servises : not dclared and not registered in the container
        //AddSingleton() => create only one object for the service and share it accross the application 
        //builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        //AddScoped() => create one object for the service and share it accross the request pipeline 
        //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        //AddTransient() => create new object for the service for each request 
        //for Repositories
        // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCourseCommand).Assembly));
    }
}