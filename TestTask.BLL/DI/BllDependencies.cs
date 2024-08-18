using Microsoft.Extensions.DependencyInjection;
using TestTask.BLL.Interfaces;
using TestTask.BLL.Services;

namespace TestTask.BLL.DI;

public static class BllDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        services.AddTransient<IMessageService, MessageService>();
    }
}
