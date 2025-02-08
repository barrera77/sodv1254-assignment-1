using Library_Management_System.DAL;
using System.Text;
using Library_Management_App.UI;
using Library_Management_System.BLL;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTables;
using Library_Management_System.ViewModels;


namespace Library_Management_App.Helpers
{
    internal class UIHelperMethods
    {
        static string feedbackMessage;
        static string errorMessage;

        static List<string> errorDetails = new List<string>();

        static string userName;


        #region main logic methods

        public static void LibraryScreen()
        {
            Console.Clear();
            PrintScreenHeader(false);
            LoginScreen();

        }


        public static void LoginScreen()
        {
            Console.Clear();
            PrintScreenHeader(false);
            Console.WriteLine("\n");
            PrintCentered("W E L C O M E  T O  B V C  L I B R A R Y  S E R V I C E\n", "static");
            Console.WriteLine("\n");

            PrintCentered("L O G I N  🔒\n", "static");

            PrintCentered("   1.- Librarian", "static");
            PrintCentered(" 2.- Member", "static");
            PrintCentered("3.- Quit \n", "static");
            int userOption  = ValidateOption("\t\t\t\t\t      Please Select an option: ", 1, 3);

            HandleLoginOption(userOption);  
        }

        public static void HandleLoginOption(int option)
        {
            string input;

            switch (option)
            {
                case 1:
                    //Request the name of the user
                    Console.WriteLine("\n");
                    PrintCentered("Please enter your username (required): ", "prompt");
                    input = Console.ReadLine();

                    userName = ValidateUserName(input, "Admin");

                    Console.Clear();
                    PrintScreenHeader(true);

                    //TODO: Add Librarian user logic here
                    var librarianUI = Program.CreateHostBuilder(new string[] { }).Build().Services.GetRequiredService<LibrarianUI>();
                    librarianUI.Run();
                    break;

                case 2:
                    //Request the name of the user
                    Console.WriteLine("\n");
                    PrintCentered("Please enter your username (Required): ", "prompt");
                    input = Console.ReadLine();

                    userName = ValidateUserName(input, "");

                    Console.Clear();
                    PrintScreenHeader(true);

                    //TODO: Add Borrower user logic here
                    var borrowerUI = Program.CreateHostBuilder(new string[] { }).Build().Services.GetRequiredService<BorrowerUI>();
                    borrowerUI.Run();                   
                    break;

                case 3:

                    QuitSystem();                   

                    break;
            }
            

        }

        public static void DisplayInventory(MediaServices mediaServices, List<MediaInventoryView> mediaInventory)
        {
            try
            {
                mediaInventory = mediaServices.GetMediaInventory();

                if (mediaInventory == null || !mediaInventory.Any())
                {
                    Console.WriteLine("No media found in the inventory.");
                    return;
                }
            }
            catch (AggregateException ex)
            {
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage += Environment.NewLine;
                }

                errorMessage += "Unable to process refund";
                foreach (Exception error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }               
            }            
            catch (Exception ex)
            {
                errorMessage = ExceptionHelperClass.GetInnerException(ex).Message;
            }

            if (mediaInventory == null || !mediaInventory.Any())
            {
                Console.WriteLine("No media items found.");
                Console.ReadLine();
                return;
            }


            var table = new ConsoleTable("Title", "Media Type", "Available", "Language", "Duration", "Genre");

            foreach (var item in mediaInventory)
            {
                table.AddRow(
                    item.Title,
                    item.MediaType,
                    item.IsAvailable == true ? "Yes" : "No",
                    item.Language,
                    item.Duration.HasValue ? item.Duration.Value.ToString() : "N/A",
                    item.Genre
                    );
            }
            table.Write();
            Console.ReadLine();
        }

        /// <summary>
        /// Return a list of media based on the type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getMediaMethod"></param>
        /// <param name="mediaType"></param>
        /// <param name="tableHeaders"></param>
        public static void BrowseMedia<T>(Func<List<T>> getMediaMethod, string mediaType)
        {
            List<T> mediaList = new List<T>();

            try
            {
                mediaList = getMediaMethod();
            }
            catch (AggregateException ex)
            {
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage += Environment.NewLine;
                }

                errorMessage += "Unable to process refund";
                foreach (Exception error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ExceptionHelperClass.GetInnerException(ex).Message;
            }

            if (mediaList == null || !mediaList.Any())
            {
                Console.WriteLine($"No {mediaType}s found.");
                Console.ReadLine();
                return;
            }

            ConsoleTable table;

            switch (mediaType.ToLower())
            {
                case "book":
                    table = new ConsoleTable("Id", "Title", "Author", "ISBN", "Available", "Genre");

                    foreach (var item in mediaList.Cast<BookView>())
                    {
                        table.AddRow(
                            item.BookId,
                            item.Title,
                            item.Author,
                            item.ISBN,
                            item.IsAvailable == true ? "Yes" : "no",
                            item.Genre
                            );
                    }
                    break;

                case "dvd":
                    table = new ConsoleTable("Id", "Title", "Actors", "Subtitles", "Available", "Genre");

                    foreach (var item in mediaList.Cast<DvdView>())
                    {
                        table.AddRow(
                            item.DvdId,
                            item.Title,
                            item.Actors,
                            item.Subtitles,
                            item.IsAvailable == true ? "Yes" : "no",
                            item.Genre
                            );
                    }
                    break;

                case "audiobook":
                    table = new ConsoleTable("Id", "Title", "Author", "Narrator", "Available", "Genre");

                    foreach (var item in mediaList.Cast<AudioBookView>())
                    {
                        table.AddRow(
                            item.AudioBookId,
                            item.Title,
                            item.Author,
                            item.Narrator,
                            item.IsAvailable == true ? "Yes" : "no",
                            item.Genre
                            );
                    }
                    break;

                default:
                    Console.Write("Unsupported Media Type. Press any key to continue...");
                    Console.ReadKey();

                    return;
            }
            table.Write();
            Console.ReadLine();

        }



        #endregion

        #region helper methods

        /// <summary>
        /// Center text in the x axis
        /// </summary>
        /// <param name="text"></param>
        public static void PrintCentered(string text, string textType)
        {
            int consoleWidth = Console.WindowWidth;
            int leftPadding = (consoleWidth - text.Length) / 2;

            if (textType == "static")
            {
                Console.WriteLine(text.PadLeft(leftPadding + text.Length));
            }
            else if (textType == "prompt")
            {
                Console.Write(text.PadLeft(leftPadding + text.Length));
            }



        }

        /// <summary>
        /// Print text in color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void PrintColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void IntroScreen()
        {



            string[] mainScreen = new string[]
            {
                "         ______      _______   _      _ _                                                             ",
                "        |  _ \\ \\    / / ____| | |    (_) |                                                            ",
                "        | |_) \\ \\  / / |      | |     _| |__  _ __ __ _ _ __ _   _                                    ",
                "       |  _ < \\ \\/ /| |      | |    | | '_ \\| '__/ _` | '__| | | |                                  ",
                "       | |_) | \\  / | |____  | |____| | |_) | | | (_| | |  | |_| |                                  ",
                "  __  _|____/   \\/   \\_____| |______|_|_.__/|_|  \\__,_|_|  _\\__, |_____           _                 ",
                " |  \\/  |                                                 | |__/ / ____|         | |               ",
                " | \\  / | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |___/ (___  _   _ ___| |_ ___ _ __ ___ ",
                " | |\\/| |/ _` | '_ \\ / _` |/ _` |/ _ \\ '_ ` _ \\ / _ \\ '_ \\| __|  \\___ \\| | | / __| __/ _ \\ '_ ` _ \\",
                " | |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_   ____) | |_| \\__ \\ ||  __/ | | | | |",
                " |_|  |_|\\__,_|_| |_|\\__,_|\\__, |\\___|_| |_| |_|\\___|_| |_|\\__| |_____/ \\__, |___/\\__\\___|_| |_| |_|",
                "                            __/ |                                        __/ |                      ",
                "                           |___/                                        |___/                       "
            };


            string[] BookArt = new string[]
            {
                "   ______ ______",
                "    _/      Y      \\_",
                "   // ~~ ~~ | ~~ ~  \\\\",
                "   // ~ ~ ~~ | ~~~ ~~ \\\\",
                "   //________.|.________\\\\",
                "    `----------`-'----------'",

            };



            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (string line in mainScreen)
            {
                PrintCentered(line, "static");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n");
            foreach (var line in BookArt)
            {
                PrintCentered(line, "static");
            }
            Console.ResetColor();

            //Console.WriteLine("\n\n");
            //Console.Write("Username (optional): ");
            //userName = Console.ReadLine();

            Console.WriteLine("\n\n\n\n\n");
            Console.OutputEncoding = Encoding.UTF8;
            PrintCentered("\u00A9 2025 <\u0414/> Manuel Alva", "static");

            Console.Write("\nPress any key to start...");
            Console.ReadKey();
        }

        /// <summary>
        /// Create a main header for all pages
        /// </summary>
        public static void PrintScreenHeader(bool isLoggedIn)
        {
            if(!isLoggedIn)
            {
                userName = "";
            }
            

            string[] header = new string[]
          {
            "\t┳┓┓┏┏┓  ┓ •┓           ┏┓                  __________________________________________________________________",
            "\t┣┫┃┃┃   ┃ ┓┣┓┏┓┏┓┏┓┓┏  ┗┓┓┏┏╋┏┓┏┳┓        /",
            $"\t┻┛┗┛┗┛  ┗┛┗┗┛┛ ┗┻┛ ┗┫  ┗┛┗┫┛┗┗ ┛┗┗       /                {userName}",
            "____________________________┛_____┛_____________/",
        };

            //Print screen header
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            foreach (var line in header)
            {
                Console.WriteLine(line);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// validates the user input
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>Validated menu option</returns>
        public static int ValidateOption(string prompt, int min, int max)
        {
            bool isValidOption = false;
            int option = 0;

            Console.Write(prompt);

            while (!isValidOption)
            {
                string input = Console.ReadLine();

                isValidOption = int.TryParse(input, out option) && option >= min && option <= max || option == (max + 1);

                if (!isValidOption)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
                }
            }

            return option;
        }

        /// <summary>
        /// Allow user to quit the game at any time
        /// </summary>
        public static void QuitSystem()
        {
            Console.Write("\nThank for using BVC Library System, come back soon!");
            Thread.Sleep(2000); // Pause for 2 seconds
            Environment.Exit(0);
        }

        //Validate the username
        static string ValidateUserName(string input, string role)
        {           
            while(!ValidStringInput(input))
            {
                Console.Write("Username cannot be null, please provide a username: ");
                input = Console.ReadLine();               
            }

            return $"👤 Username: {input.ToUpper()} {role}";
        }

        
        /// <summary>
        /// Validate string input
        /// </summary>
        /// <param name="input"></param>
        /// <returns>if valid input truel, else false</returns>
        static bool ValidStringInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static void PrintMenu(string[] menuOptions)
        {
            foreach (string option in menuOptions)
            {
                Console.WriteLine(option);
            }
        }

        /// <summary>
        /// Logout the user
        /// </summary>
        public static void LogOut()
        {
            Console.WriteLine("\nLogging out . . .");
            Thread.Sleep(2000);
            PrintColorText("You have been successfully logged out.", ConsoleColor.Green);
            Thread.Sleep(1000);

            LoginScreen();
        }




        #endregion
    }
}
