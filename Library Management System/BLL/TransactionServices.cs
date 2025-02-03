﻿using Library_Management_System.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.BLL
{
    public class TransactionServices
    {
        private readonly LibrarydbContext _dbContext;

        internal TransactionServices(LibrarydbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string errorMessage;

        //Create an array to store the errors
        List<Exception> errorList = new List<Exception>();
    }
}
