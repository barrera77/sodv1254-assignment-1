using System;
using Library_Management_App.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Library_Management_System.ViewModels;
using Library_Management_System.BLL;
using ConsoleTables;

namespace Library_Management_App.UI
{
    public class LibrarianUI
    {
        //Declare an instance of  Media services 
        private readonly MediaServices _mediaServices;
        public List<MediaInventoryView> mediaInventory { get; set; } = new List<MediaInventoryView>();

        private string feedbackMessage;
        private string errorMessage;

        private List<string> errorDetails = new List<string>();

        //inject the dependencies via constructor
        public LibrarianUI(MediaServices mediaServices)
        {
            _mediaServices = mediaServices ?? throw new ArgumentNullException(nameof(mediaServices));
        }

        public void Run()
        {
            LibrarianMainMenu();
        }

        /// <summary>
        /// Print the main menu for the librarian
        /// </summary>
        public void LibrarianMainMenu()
        {

            string[] librarianMenuOptions = 
            {
                "1.- Media Menu",
                "2.- Library Members Menu",
                "3.- Logout",
                "4.- Quit",
            };

            Console.WriteLine("\n\n");
            UIHelperMethods.PrintMenu(librarianMenuOptions);
            int option = UIHelperMethods.ValidateOption("\nPlease select an option from the menu: ", 1, librarianMenuOptions.Length);

            HandleMainMenuOption(option);
        }


        /// <summary>
        /// Handle options from the main menu
        /// </summary>
        /// <param name="menuOption"></param>
        public void HandleMainMenuOption(int menuOption)
        {
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                switch(menuOption)
                {
                    case 1:
                        MediaMenu();
                        break;

                        case 2:
                        LibraryBorrowersMenu();
                        break;

                        case 3:
                        UIHelperMethods.LogOut();
                        break;
                    case 4:
                        UIHelperMethods.QuitSystem();
                        break;
                }
            }
        }

        /// <summary>
        /// Print the media management menu for the librarian
        /// </summary>
        public void MediaMenu()
        {
            Console.Clear();
            UIHelperMethods.PrintScreenHeader(true);

            string[] mediaMenuOptions =
            {
                "1.- Browse Media Inventory",
                "2.- Add Media Item",
                "3.- Remove Media item",
                "4.- Back to previous Menu",
                "5.- Logout",
                "6.- Quit",
            };

            Console.WriteLine("\n\n");
            UIHelperMethods.PrintMenu(mediaMenuOptions);
            int option = UIHelperMethods.ValidateOption("\nPlease select an option from the menu: ", 1, mediaMenuOptions.Length);

            HandleMediaMenuOption(option);

        }

        /// <summary>
        /// Handle option for the media menu
        /// </summary>
        /// <param name="option"></param>
        public void HandleMediaMenuOption(int option)
        {           
                switch (option)
                {
                    case 1:
                        DisplayInventory();
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        UIHelperMethods.LogOut();
                        break;

                    case 6:
                        UIHelperMethods.QuitSystem();
                        break;
                }
            

        }

        /// <summary>
        /// Print the borrowers management menu for the librarian
        /// </summary>
        public void LibraryBorrowersMenu()
        {
            Console.Clear();
            UIHelperMethods.PrintScreenHeader(true);

            string[] borrowersMenuOptions =
            {
                "1.- Browse Members List",
                "2.- Add Member",
                "3.- Remove Member",
                "4.- Back to previous Menu",
                "5.- Logout",
                "6.- Quit",
            };

            Console.WriteLine("\n\n");
            UIHelperMethods.PrintMenu(borrowersMenuOptions);
            int option = UIHelperMethods.ValidateOption("\nPlease select an option from the menu ", 1, borrowersMenuOptions.Length);
            
            HandleBorrowersMenuOption(option);
        }

        /// <summary>
        /// Handle option for the borrowers menu
        /// </summary>
        /// <param name="option"></param>
        public void HandleBorrowersMenuOption(int option)
        {

        }

        public void DisplayInventory()
        {            
            try
            {
                mediaInventory = _mediaServices.GetMediaInventory();

                if(mediaInventory == null || !mediaInventory.Any())
                {
                    Console.WriteLine("No media found in the inventory.");
                    return;
                }
            }
            catch (AggregateException ex)
            {
                if(string.IsNullOrWhiteSpace(errorMessage))
                {
                    errorMessage += Environment.NewLine;
                }

                foreach (Exception error in ex.InnerExceptions)
                {
                    errorDetails.Add(error.Message);
                }

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


    }


}
