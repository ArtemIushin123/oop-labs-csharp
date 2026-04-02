using Itmo.ObjectOrientedProgramming.Lab5.Application;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string systemPassword = builder.Configuration["System:Password"] ?? "admin123";

        builder.Services.AddInfrastructure();
        builder.Services.AddApplication(systemPassword);
        builder.Services.AddControllers();

        WebApplication app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}