using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Library_Management_System.DAL;
using Library_Management_System.BLL;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main(string[] args)
    {
        // Create the host
        var host = CreateHostBuilder(args).Build();

        // Retrieve the services and start the application
        // var service = ActivatorUtilities.CreateInstance<App>(host.Services);
        // service.Run();

        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        string dbPath = Path.Combine(projectDirectory, "library.db");
        string connectionString = $"Data Source={dbPath}";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            // ✅ Enable foreign key constraints (optional, for integrity)
            var pragmaCommand = connection.CreateCommand();
            pragmaCommand.CommandText = "PRAGMA foreign_keys = ON;";
            pragmaCommand.ExecuteNonQuery();

            // 🔍 Query Media Table
            Console.WriteLine("Media Table:");
            var mediaCommand = connection.CreateCommand();
            mediaCommand.CommandText = "SELECT MediaId, Title, IsAvailable, MediaType FROM Media;";
            using (var reader = mediaCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["MediaId"]}, Title: {reader["Title"]}, Available: {reader["IsAvailable"]}, Type: {reader["MediaType"]}");
                }
            }

            Console.WriteLine("\nBook Table:");
            var bookCommand = connection.CreateCommand();
            bookCommand.CommandText = "SELECT BookId, Author, Genre FROM Book;";
            using (var reader = bookCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Book ID: {reader["BookId"]}, Author: {reader["Author"]}, Genre: {reader["Genre"]}");
                }
            }

            Console.WriteLine("\nDVD Table:");
            var dvdCommand = connection.CreateCommand();
            dvdCommand.CommandText = "SELECT DVDId, Director, Runtime FROM DVD;";
            using (var reader = dvdCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"DVD ID: {reader["DVDId"]}, Director: {reader["Director"]}, Runtime: {reader["Runtime"]} mins");
                }
            }

            Console.WriteLine("\n Borrower Table:");
            var borrowerCommand = connection.CreateCommand();
            borrowerCommand.CommandText = "SELECT BorrowerId, Name, Address, ContactNumber, Email FROM Borrower;";
            using (var reader = borrowerCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["BorrowerId"]}, Name: {reader["Name"]}, Contact: {reader["ContactNumber"]}, Email: {reader["Email"]}");
                }
            }

            Console.WriteLine("\nLibraryTransaction Table:");
            var transactionCommand = connection.CreateCommand();
            transactionCommand.CommandText = @"
                SELECT lt.TransactionId, b.Name AS BorrowerName, m.Title AS MediaTitle, lt.BorrowDate, lt.ReturnDate
                FROM LibraryTransaction lt
                JOIN Borrower b ON lt.BorrowerId = b.BorrowerId
                JOIN Media m ON lt.MediaId = m.MediaId;";
            using (var reader = transactionCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Transaction ID: {reader["TransactionId"]}, Borrower: {reader["BorrowerName"]}, Media: {reader["MediaTitle"]}, Borrowed On: {reader["BorrowDate"]}, Returned On: {(reader["ReturnDate"] == DBNull.Value ? "Not Returned" : reader["ReturnDate"])}");
                }
            }
        }

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
                // Configure DbContext
                var connectionString = context.Configuration.GetConnectionString("LibraryDB");

                services.AddDbContext<LibrarydbContext>(options =>
                    options.UseSqlite(connectionString));

                // Register services
                services.AddTransient<MediaServices>();
                services.AddTransient<BorrowerServices>();
                services.AddTransient<TransactionServices>();

                // Register the application entry point
                services.AddTransient<App>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
}

public class App
{
    private readonly MediaServices _mediaService;

    public App(MediaServices mediaService)
    {
        _mediaService = mediaService;
    }

    public void Run()
    {
                        
    }
}
