using Library_Management_System.BLL;
using Library_Management_System.DAL;
using Library_Management_System.ViewModels;
using Library_Management_App.Helpers;
using System.Text;

namespace Library_Management_App.UI
{
    internal class App
    {
        
        private readonly MediaServices _mediaServices;

        public App(LibrarydbContext dbContext)
        {
            _mediaServices = new MediaServices(dbContext);
        }

        public void Run()
        {
            //print the intro screen
            UIHelperMethods.IntroScreen();

            //Print the library's main screen
            UIHelperMethods.LibraryScreen();

        }

        #region main functions

        
        






        #endregion

        #region helper methods






        #endregion
    }

}
