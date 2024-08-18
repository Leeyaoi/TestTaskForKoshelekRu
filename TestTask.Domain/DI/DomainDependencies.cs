using Microsoft.Extensions.DependencyInjection;
using TestTask.Domain.Interfaces;
using TestTask.Domain.Services;

namespace TestTask.Domain.DI;

public static class DomainDependencies
{
    public static void RegisterDomainDependencies(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    }
}
