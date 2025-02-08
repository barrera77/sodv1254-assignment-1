using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.ViewModels
{
    public class DvdView : MediaInventoryView
    {
        public int DvdId { get; set; }
        public string Director { get; set; }
        public string Subtitles { get; set; }
        public string Actors { get; set; }  
    }
}
