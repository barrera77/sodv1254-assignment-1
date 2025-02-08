using Library_Management_System;
using Library_Management_System.DAL;
using Library_Management_System.Entities;
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

        public void CreateLibraryTransaction(int mediaId, int borrowerId, string mediaType)
        {
            var media = _dbContext.MediaLibraries.FirstOrDefault(m => m.MediaId == mediaId && m.MediaType == mediaType); 
            Borrower borrower = _dbContext.Borrowers.FirstOrDefault(b => b.BorrowerId == borrowerId);

            if(borrower == null)
            {
                throw new ArgumentException("Borrower number not found");
            }
            else if(media == null)
            {
                throw new ArgumentException("Media not found");
            }

            LibraryTransaction transaction = new LibraryTransaction
            {
                MediaId = mediaId,
                BorrowerId = borrowerId,
                BorrowDate = DateOnly.FromDateTime(DateTime.Today),
                ReturnDate = null,
                //notes property need to be added to the entity
            };

            _dbContext.LibraryTransactions.Add(transaction);

            _dbContext.SaveChanges();
        }


    }
}
