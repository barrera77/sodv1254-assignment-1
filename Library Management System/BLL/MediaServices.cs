using Library_Management_System.DAL;
using Library_Management_System.Entities;
using Library_Management_System.ViewModels;
using System.Security.Cryptography.X509Certificates;


namespace Library_Management_System.BLL
{
    public class MediaServices
    {
        private readonly LibraryDBdbContext _dbContext;

        public MediaServices(LibraryDBdbContext context)
        {
            _dbContext = context;
        }

        private string errorMessage;

        //Create an array to store the errors
        List<Exception> errorList = new List<Exception>();


        /// <summary>
        /// Get all items in he media inventory
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public List<MediaInventoryView> GetMediaInventory()
        {
            errorList.Clear();

            try
            {
                List<MediaInventoryView> mediaInventory = _dbContext.MediaLibraries
                    .Select(m => new MediaInventoryView
                    {
                        MediaId = m.MediaId,
                        Title = m.Title,
                        Description = m.Description,
                        MediaType = m.MediaType,
                        IsAvailable = m.IsAvailable,
                        CreationDate = m.CreationDate,
                        Language = m.Language,
                        Duration = m.Duration,

                        Genre = m.Genre.Name,
                       
                    })
                    .ToList();
                Console.WriteLine($"Query Executed: Retrieved {mediaInventory.Count} records.");

                return mediaInventory;

            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                throw new AggregateException("An error ocurred retrieving the media inventory from the system", ex);

            }
        }

        /// <summary>
        /// Get a list of all books
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public List<BookView> GetAllBooks()
        {
            errorList.Clear();

            try
            {
                List<BookView> booksList = _dbContext.MediaLibraries
                    .Where(m => m.MediaType == "Book")
                    .OrderBy(m => m.Title)
                    .Select(m => new BookView
                    {
                        BookId = m.Book != null ? m.Book.BookId : 0,
                        Title = m.Title,
                        Author = m.Book == null ? "Unknown" : m.Book.Author,
                        ISBN = m.Book != null ? m.Book.ISBN : "N/A",
                        IsAvailable = m.IsAvailable,
                        Genre = m.Genre != null ? m.Genre.Name : "Unknown",
                    })
                    .ToList();

                Console.WriteLine($"Query Executed: Retrieved {booksList.Count} records.");

                return booksList;
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                throw new AggregateException("An error ocurred retrieving the books from the system", ex);
            }
        }

        /// <summary>
        /// Get a list of all existing DVDs
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public List<DvdView> GetAllDvds()
        {
            errorList.Clear();

            try
            {
                List<DvdView> dvdsList = _dbContext.MediaLibraries
                    .Where(m => m.MediaType == "DVD")
                    .OrderBy(m => m.Title)
                    .Select(m => new DvdView
                    {
                        DvdId = m.DVD != null ? m.DVD.DVDId : 0,
                        Title = m.Title,
                        IsAvailable = m.IsAvailable,
                        Subtitles = m.DVD != null ? m.DVD.Subtitles : "N/A",
                        Actors =  m.DVD != null ? m.DVD.Actors : "Unknown",
                        Genre = m.Genre != null ? m.Genre.Name : "Unknown",


                    })
                    .ToList();

                return dvdsList;

            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                throw new AggregateException("An error ocurred retrieving the books from the system", ex);
            }            
        }

        /// <summary>
        /// Getr a list of all audiobooks
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public List<AudioBookView> GetAllAudioBooks()
        {
            errorList.Clear();

            try
            {
                List<AudioBookView> audioBooksList = _dbContext.MediaLibraries
                    .Where(ab => ab.MediaType == "AudioBook")
                    .OrderBy(ab => ab.Title)
                    .Select(ab => new AudioBookView
                    {
                        AudioBookId = ab.AudioBook != null ? ab.AudioBook.AudioBookId : 0,
                        Title = ab.Title,
                        IsAvailable = ab.IsAvailable,
                        Narrator = ab.AudioBook != null ? ab.AudioBook.Narrator : "Unknown",   
                        Author = ab.AudioBook != null ? ab.AudioBook.Author : "Unknown",
                        Genre = ab.Genre != null ? ab.Genre.Name : "Unknown",

                    })
                    .ToList();

                return audioBooksList;
            }
            catch (Exception ex)
            {
                errorList.Add(ex);
                throw new AggregateException("An error ocurred retrieving the Audiobooks from the system", ex);
            }
        }
    }
}
