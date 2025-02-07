using Library_Management_System.BLL;
using Library_Management_System.DAL;
using Library_Management_App.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = host.Services.GetRequiredService<App>(); 
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = "Data Source=LibraryDB.db";

                //  Register DbContext
                services.AddDbContext<LibraryDBdbContext>(options =>
                    options.UseSqlite(connectionString));

                // Register Services
                services.AddTransient<MediaServices>();
                services.AddTransient<BorrowerServices>();
                services.AddTransient<TransactionServices>();

                //  Register UI Components
                services.AddTransient<LibrarianUI>();
                services.AddTransient<BorrowerUI>();

                //  Register App
                services.AddTransient<App>();
            });
}
