﻿using Library_Management_System;
using Library_Management_System.DAL;
using Library_Management_System.ViewModels;


namespace Library_Management_System.BLL
{
    public class TransactionServices
    {
        private readonly LibraryDBdbContext _dbContext;

        internal TransactionServices(LibraryDBdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string errorMessage;

        //Create an array to store the errors
        List<Exception> errorList = new List<Exception>();


    }
}
