using System;
using Library_Management_App.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_App.UI
{
    public class LibrarianUI
    {

        public void Run()
        {
            MainMenu();
        }

        public void MainMenu()
        {

            string[] menuOptions = {"1.- Media Inventory Menu",
            "2.- Library Members Menu",
            "3.- Logout",
            "4.- Quit",
            };

            Console.WriteLine("\n\n");
            UIHelperMethods.PrintMenu(menuOptions);
            int option = UIHelperMethods.ValidateOption("\nPlease select an option from the menu ", 1, menuOptions.Length);

            Console.ReadLine();
        }

        public void HandleMainMenuOption(int menuOption)
        {
            bool isLoggedIn = true;

            while (isLoggedIn)
            {
                switch(menuOption)
                {
                    case 1:

                        break;

                        case 2:

                        break;

                        case 3:
                        isLoggedIn = false;

                        userName = "";

                        return;
                }
            }


        }


    }


}
