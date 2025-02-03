using Library_Management_System.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.BLL
{
    public class MediaServices
    {
        private readonly LibrarydbContext _context;

        public MediaServices(LibrarydbContext context) 
        {
            _context = context;
        }

        private string errorMessage;

        //Create an array to store the errors
        List<Exception> errorList = new List<Exception>();
    }
}
