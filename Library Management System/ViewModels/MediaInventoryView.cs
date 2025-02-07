using Library_Management_System;
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
        public string Genre { get; set; }
        
    }
}
