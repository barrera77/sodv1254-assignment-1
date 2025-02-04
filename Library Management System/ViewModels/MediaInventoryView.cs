using Library_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.ViewModels
{
    public class MediaInventoryView
    {
        public int MediaId { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public DateTime CreationDate { get; set; }
        public string Language { get; set; }
        public int? Genre { get; set; }

        // Book-specific properties
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }

        // DVD-specific properties
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Subtitles { get; set; }

        // Audiobook-specific properties
        public string Narrator { get; set; }
        public string AudioFormat { get; set; }
    }
}
