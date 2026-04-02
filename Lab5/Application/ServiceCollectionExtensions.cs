using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string systemPassword)
    {
        services.AddSingleton<IAuthenticationService>(sp =>
            new AuthenticationService(
                sp.GetRequiredService<Repositories.IAccountRepository>(),
                sp.GetRequiredService<Repositories.ISessionRepository>(),
                systemPassword));

        services.AddSingleton<IAccountService, AccountService>();

        return services;
    }
}