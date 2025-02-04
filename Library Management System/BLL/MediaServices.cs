using Library_Management_System.DAL;
using Library_Management_System.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.BLL
{
    public class MediaServices
    {
        private readonly LibrarydbContext _dbContext;

        public MediaServices(LibrarydbContext context)
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
                List<MediaInventoryView> mediaInventory = _dbContext.LibraryMedia
                    .Select(m => new MediaInventoryView
                    {
                        MediaId = m.MediaId,
                        Title = m.Title,
                        Description = m.Description,
                        MediaType = m.MediaType,
                        IsAvailable = m.IsAvailable,
                        CreationDate = m.CreationDate,

                        Author = m.MediaType == "Book" ? m.Book.Author : (m.MediaType == "AudioBook" ? m.AudioBook.Author : null),
                        ISBN = m.MediaType == "Book" ? m.Book.Isbn : null,
                        Publisher = m.MediaType == "Book" ? m.Book.Publisher : null,

                        Director = m.MediaType == "DVD" ? m.Dvd.Director : null,
                        Actors = m.MediaType == "DVD" ? m.Dvd.Actors : null,
                        Subtitles = m.MediaType == "DVD" ? m.Dvd.Subtitles : null,

                        Narrator = m.MediaType == "AudioBook" ? m.AudioBook.Narrator : null,
                        AudioFormat = m.MediaType == "AudioBook" ? m.AudioBook.AudioFormat : null,
                    })
                    .ToList();

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
