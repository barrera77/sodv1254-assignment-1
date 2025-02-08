using Library_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.ViewModels
{
    public class AudioBookView : MediaInventoryView
    {
        public int AudioBookId { get; set; }
        public string Narrator { get; set; }
        public string AudioFormat { get; set; }
        public string Author { get; set; }
    }
}
