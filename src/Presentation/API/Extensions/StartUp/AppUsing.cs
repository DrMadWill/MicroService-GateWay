
using Application;
using DrMadWill.EventBus.Base.Abstractions;
using DrMadWill.Extensions.Cros;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;

namespace API.Extensions.StartUp;

public static class AppUsing
{
    public static WebApplication UseAppUsing(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Verbose;
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
            };
        });
        app.UseAppHealthChecks();
        app.UseHttpsRedirection(); 
        CrosExtension.UsingCrosServices(app);
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseOcelot().GetAwaiter().GetResult();
        var eventBus = app.Services.GetService<IEventBus>();
        eventBus!.UsingSubScribes();
        return app;
    }
}