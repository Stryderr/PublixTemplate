using API;
using S0WISRXX.SharedExternal.Logger;
using S0WISRXX.SharedExternal.UIM;
using Serilog;

public class Program
{

    private static IUtilityLogger _logger;

    public static void Main(string[] args)
    {
        Console.WriteLine("Entering Program.cs");
        Console.WriteLine("Starting Application");
        ConfigureLogging();
        CreateHostBuilder(args).Build().Run();
    }


    public static void ConfigureLogging()
    {
        try
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine($"Current Environment is {environment}");

            if (!string.IsNullOrWhiteSpace(environment))
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .Build();

                Console.WriteLine($"ConfigurationBuilder details are - {configuration?.Providers.Count()}");

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                IUIM uIM = new UIM();
                _logger = new UtilityLogger(uIM);
                _logger.SetConfiguration(configuration);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred in ConfigureLogging() - {ex.Message}");
            Console.WriteLine($"Call Stack occurred in ConfigureLogging() - {ex.StackTrace}");
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
