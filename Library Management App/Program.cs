using Library_Management_System.DAL;
using Library_Management_System.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Library_Management_App.UI;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = ActivatorUtilities.CreateInstance<App>(host.Services);
        app.Run();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("LibraryDB");
                services.AddDbContext<LibrarydbContext>(options =>
                    options.UseSqlite(connectionString));

                services.AddTransient<MediaServices>();
                services.AddTransient<BorrowerServices>();
                services.AddTransient<TransactionServices>();

                services.AddTransient<App>(); 
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
}
