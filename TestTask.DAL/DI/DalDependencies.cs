using Microsoft.Extensions.DependencyInjection;
using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;

namespace TestTask.DAL.DI;

public static class DalDependencies
{
    public static void RegisterDALDependencies(this IServiceCollection services)
    {
        services.AddTransient<IMessageRepository, MessageRepository>();
    }
}
