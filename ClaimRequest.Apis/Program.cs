using ClaimRequest.Apis.Extensions;
using ClaimRequest.Application;
using ClaimRequest.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace ClaimRequest.Apis;

public class Program
{
    public static void Main(string[] args)
    {
        
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddSwaggerGenWithAuth();

        builder.Services
            .AddApplication()
            .AddPresentation()
            .AddInfrastructure(builder.Configuration);
        

        WebApplication app = builder.Build();


      
        app.UseSwaggerWithUi();

        app.ApplyMigrations();
        

        app.MapHealthChecks("health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseCors();
        
        app.UseRequestContextLogging();

        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();
        
        app.UseHttpsRedirection();
        
        app.UseAuthentication();

        
        app.UseAuthorization();

// REMARK: If you want to use Controllers, you'll need this.
        app.MapControllers();
        app.Run();
    }
}