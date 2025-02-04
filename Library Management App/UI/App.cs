using Library_Management_System.BLL;
using Library_Management_System.DAL;
using Library_Management_System.ViewModels;
using System.Text;

namespace Library_Management_App.UI
{
    internal class App
    {
        private string userName;

        private readonly MediaServices _mediaServices;

        public App(LibrarydbContext dbContext)
        {
            _mediaServices = new MediaServices(dbContext);
        }

        public void Run()
        {
            //print the intro screen
            IntroScreen();

            //Print the library's main screen
            LibraryScreen();

        }

        #region main functions

        public void LibraryScreen()
        {
            Console.Clear();
            PrintScreenHeader();
            LoginScreen();

        }

        public void LoginScreen()
        {
            Console.WriteLine("\n\n\n");
            PrintCentered("W E L C O M E  T O  B V C  L I B R A R Y\n", "static");
            PrintCentered("L O G I N\n", "static");

            PrintCentered("1.- Librarian", "static");
            PrintCentered("2.- Borrower\n", "static");
            PrintCentered("Please Chose you user type: ", "prompt");
            Console.ReadLine();
        }



        public void MainMenu()
        {
            string[] mainMenuOptions =
                {"1.-View Full Inventory",
                "2.-", "3.-", "4.-", "5.-", "6.- Quit" };



        }






        #endregion

        #region helper methods

        /// <summary>
        /// Center text in the x axis
        /// </summary>
        /// <param name="text"></param>
        public void PrintCentered(string text, string textType)
        {
            int consoleWidth = Console.WindowWidth;
            int leftPadding = (consoleWidth - text.Length) / 2;

            if(textType == "static")
            {
                Console.WriteLine(text.PadLeft(leftPadding + text.Length));
            }
            else if(textType == "prompt")
            {
                Console.Write(text.PadLeft(leftPadding + text.Length));
            }
            


        }

        /// <summary>
        /// Print text in color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void PrintColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void IntroScreen()
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
        public void PrintScreenHeader()
        {         
            string[] header = new string[]
          {
            "\t┳┓┓┏┏┓  ┓ •┓           ┏┓                  __________________________________________________________________",
            "\t┣┫┃┃┃   ┃ ┓┣┓┏┓┏┓┏┓┓┏  ┗┓┓┏┏╋┏┓┏┳┓        /",
            "\t┻┛┗┛┗┛  ┗┛┗┗┛┛ ┗┻┛ ┗┫  ┗┛┗┫┛┗┗ ┛┗┗       /",
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






        #endregion
    }

}
