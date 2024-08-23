using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace API.Extensions.StartUp;

public static class AppHosts
{
    private static IConfiguration GetSerilogConfig() =>
        new ConfigurationManager()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Config/Serilog/serilog.json", optional: false)
            .AddJsonFile($"Config/Serilog/serilog.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .AddEnvironmentVariables()
            .Build(); 
    
    public static ConfigureHostBuilder UseAppHosts(this ConfigureHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithProperty("Application", "GateWay")
            .Filter.ByExcluding(c => c.Properties.ContainsKey("RequestPath") && c.Properties["RequestPath"].ToString().Contains("/health"))
            .WriteTo.Graylog(new GraylogSinkOptions
            {
                HostnameOrAddress = "c_graylog",
                Port = 12201,
                TransportType = TransportType.Udp,
                Facility = "gateway-service"
            })
            .WriteTo.Console()
            .CreateLogger();
        
        host.UseSerilog();
        
        host.ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile($"Config/Ocelots/ocelot.{context.HostingEnvironment.EnvironmentName}.json", true)
                //.AddJsonFile("Ocelots/ocelot.json",false)
                .AddEnvironmentVariables();

        });

        return host;
    }
}