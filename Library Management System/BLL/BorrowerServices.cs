using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management_System.DAL;

namespace Library_Management_System.BLL
{
    public class BorrowerServices
    {
        private readonly LibrarydbContext _dbContext;

        internal BorrowerServices(LibrarydbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string errorMessage;

        //Create an array to store the errors
        List<Exception> errorList = new List<Exception>();
    }
}
