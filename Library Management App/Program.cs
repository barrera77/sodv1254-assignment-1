using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Library_Management_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ✅ Initialize SQLite Batteries
            Batteries.Init();
            Console.WriteLine("SQLite Initialized Successfully!");

            // ✅ Get Project Root Directory
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string dbPath = Path.Combine(projectDirectory, "library.db"); // Creates 'library.db' in the project root

            // ✅ Correct Connection String
            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Media (
                    MediaId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    IsAvailable BOOLEAN NOT NULL DEFAULT 1,
                    MediaType TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Book (
                    BookId INTEGER PRIMARY KEY,
                    Author TEXT NOT NULL,
                    Genre TEXT NOT NULL,
                    FOREIGN KEY (BookId) REFERENCES Media(MediaId)
                );
                CREATE TABLE IF NOT EXISTS DVD (
                    DVDId INTEGER PRIMARY KEY,
                    Director TEXT NOT NULL,
                    Runtime INTEGER NOT NULL,
                    FOREIGN KEY (DVDId) REFERENCES Media(MediaId)
                );
                CREATE TABLE IF NOT EXISTS Borrower (
                    BorrowerId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Address TEXT NOT NULL,
                    ContactNumber TEXT NOT NULL,
                    Email TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS LibraryTransaction (
                    TransactionId INTEGER PRIMARY KEY AUTOINCREMENT,
                    BorrowerId INTEGER NOT NULL,
                    MediaId INTEGER NOT NULL,
                    BorrowDate DATE NOT NULL,
                    ReturnDate DATE,
                    FOREIGN KEY (BorrowerId) REFERENCES Borrower(BorrowerId),
                    FOREIGN KEY (MediaId) REFERENCES Media(MediaId)
                );
            ";
                command.ExecuteNonQuery();
                Console.WriteLine("Database and tables created successfully at: " + dbPath);
            }
        }
    }
}
