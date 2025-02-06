using Library_Management_App.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_App.UI
{
    internal class BorrowerUI
    {
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
                        UIHelperMethods.BrowseMediaCatalog();
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

    }
}
