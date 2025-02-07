using Library_Management_System.DAL;
using Library_Management_System.ViewModels;


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

                        Genre = _dbContext.Genres
                        .Where(g => g.GenreId == m.GenreId)
                        .Select(g => g.Name)
                        .FirstOrDefault()   
                       
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
    }
}
