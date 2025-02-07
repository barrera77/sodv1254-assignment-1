using Library_Management_System.BLL;
using Library_Management_System.DAL;
using Library_Management_System.ViewModels;
using Library_Management_App.Helpers;
using System.Text;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_App.UI
{
    internal class App
    {
        private readonly LibrarianUI _librarianUI;
        private readonly BorrowerUI _borrowerUI;
        public readonly MediaServices _mediaServices;

        public App(LibrarianUI librarianUI, BorrowerUI borrowerUI) // ✅ Inject MediaServices here
        {
            _librarianUI = librarianUI;
            _borrowerUI = borrowerUI;
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
