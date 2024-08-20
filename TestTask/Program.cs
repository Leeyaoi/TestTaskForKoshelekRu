using TestTask.BLL.Models;
using TestTask.BLL.DI;
using TestTask.DAL.DI;
using TestTask.Domain.DI;
using TestTask.API.DI;
using dotenv.net;

namespace TestTask.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { @".env" }));

        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(
                        builder.Configuration.GetValue<string>("SENDERCLIENT_ORIGIN")!, 
                        builder.Configuration.GetValue<string>("GETTERCLIENT_ORIGIN")!
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
            });
        });

        builder.RegisterAPIDependencies();

        builder.Services.AddControllers();

        builder.Services.RegisterDomainDependencies();
        builder.Services.RegisterDALDependencies();
        builder.Services.RegisterBLLDependencies();

        builder.Services.AddAutoMapper(typeof(MessageModel).Assembly, typeof(Program).Assembly);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.Run();

    }
}