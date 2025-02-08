using ConsoleTables;
using Library_Management_App.Helpers;
using Library_Management_System.BLL;
using Library_Management_System.Entities;
using Library_Management_System.ViewModels;
using System.Collections.Generic;

namespace Library_Management_App.UI
{
    internal class BorrowerUI
    {
        private readonly MediaServices _mediaServices;
        public List<MediaInventoryView> mediaInventory { get; set; } = new List<MediaInventoryView>();
        public List<BookView> booksList { get; set; } = new List<BookView>();     
        public List<DvdView> dvdList { get; set; } = new List<DvdView>();
        public List<AudioBookView> audioBookList { get; set; } = new List<AudioBookView>();

        private string feedbackMessage;
        private string errorMessage;

        private List<string> errorDetails = new List<string>();


        //inject the dependencies via constructor
        public BorrowerUI(MediaServices mediaServices)
        {
            _mediaServices = mediaServices ?? throw new ArgumentNullException(nameof(mediaServices));
        }

        public void Run()
        {
            BorrowerMainMenu();
        }

        public void BorrowerMainMenu()
        {
            Console.Clear();
            UIHelperMethods.PrintScreenHeader(true);
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                 string[] borrowerMainMenuOptions =
                {
                    "1.- Browse Media Catalog",
                    "2.- Return Media Items",
                    "3.- Logout",
                    "4.- Quit",
                };

            Console.WriteLine("\n\n");
            UIHelperMethods.PrintMenu(borrowerMainMenuOptions);
            int option = UIHelperMethods.ValidateOption("\nPlease select an option from the menu: ", 1, borrowerMainMenuOptions.Length);

                HandleMainMenuOption(option);
            }
        }

        public void HandleMainMenuOption(int option)
        {
            bool isLoggedIn = true;

            while(isLoggedIn)
            {
                switch(option)
                {
                    case 1:
                        MediaMenu();
                        break;

                    case 2:
                        ReturnItems();
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

        public void ReturnItems()
        {
           

        }

        public void MediaMenu()
        {
            Console.Clear();
            UIHelperMethods.PrintScreenHeader(true);

            string[] mediaMenuOptions =
            {
                "1.- Browse All Media Inventory",
                "2.- Browse Books",
                "3.- Browse DVDs",
                "4.- Browse Audiobooks",
                "5.- Back to previous Menu",
                "6.- Logout",
                "7.- Quit",
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
                    UIHelperMethods.DisplayInventory(_mediaServices, mediaInventory);
                    break;

                case 2:
                    BorrowMedia("book");
                    break;

                case 3:
                    BorrowMedia("dvd");

                    break;

                case 4:
                    BorrowMedia("audiobook");

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
       /// Borrow media from library
       /// </summary>
       /// <param name="mediaType"></param>
        public void BorrowMedia(string mediaType)
        {
            switch (mediaType.ToLower())
            {
                case "book":
                    //get the requested media inventory
                    booksList = UIHelperMethods.BrowseMedia(_mediaServices.GetAllBooks, "book");

                    //Process the request
                    HandleBorrowingOptions("book");
                    break;

                case "dvd":
                    //get the requested media inventory
                    dvdList = UIHelperMethods.BrowseMedia(_mediaServices.GetAllDvds, "dvd");

                    //Process the request
                    HandleBorrowingOptions("dvd");
                    break;

                case "audiobook":
                    //get the requested media inventory
                    audioBookList = UIHelperMethods.BrowseMedia(_mediaServices.GetAllAudioBooks, "audiobook");

                    //Process the request
                    HandleBorrowingOptions("audiobook");
                    break;            
            }
        }

        /// <summary>
        /// Handle the borrowing logic
        /// </summary>
        /// <param name="mediaType"></param>
        public void HandleBorrowingOptions(string mediaType)
        {
            bool isValidInput = true;
            int option;

            while (isValidInput)
            {
                Console.Write("Enter the id of the item you wish to borrow or c to cancel and go back to the previous menu: ");
                string input = Console.ReadLine()?.Trim();

                if(!string.IsNullOrWhiteSpace(input) && input.Equals("C", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (int.TryParse(input, out option))
                {
                    switch (mediaType.ToLower())
                    {
                        case "book":
                            var book = booksList.FirstOrDefault(b => b.BookId == option);

                            if(book == null)
                            {
                                Console.WriteLine("Invalid ID...");
                            }
                            else if(!book.IsAvailable)
                            {
                                Console.WriteLine($"Sorry the selected {mediaType} i currently unavailable...");
                            }
                            else
                            {
                                Console.WriteLine($"The {mediaType}  {book.Title} is available");
                                Console.ReadLine();
                                isValidInput = false;
                                break;
                            }
                            break;

                        case "dvd":
                            var dvdItemt = dvdList.FirstOrDefault(d => d.DvdId == option);

                            if (dvdItemt == null)
                            {
                                Console.WriteLine("Invalid ID...");
                            }
                            else if (!dvdItemt.IsAvailable)
                            {
                                Console.WriteLine($"Sorry the selected {mediaType} is currently unavailable...");
                            }
                            else
                            {
                                Console.WriteLine($"The {mediaType}  {dvdItemt.Title} is available");
                                Console.ReadLine();
                                isValidInput = false;
                                break;
                            }
                            break;

                        case "audiobook":
                            var audiobook = audioBookList.FirstOrDefault(ab => ab.AudioBookId == option);

                            if (audiobook == null)
                            {
                                Console.WriteLine("Invalid ID...");
                            }
                            else if (!audiobook.IsAvailable)
                            {
                                Console.WriteLine($"Sorry the selected {mediaType} is currently unavailable...");
                            }
                            else
                            {
                                Console.WriteLine($"The {mediaType}  {audiobook.Title} is available");
                                Console.ReadLine();
                                isValidInput = false;
                                break;
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid Media Type...");
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input please enter a numeric ID");
                }
            }
        }

        public void 

    }
}
