using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.ViewModels
{
    public class BookView : MediaInventoryView
    {
        public int BookId { get; set; } 
        public string Author { get; set; }
        public string ISBN { get; set; }    
        public string Publisher { get; set; }   
    }
}
