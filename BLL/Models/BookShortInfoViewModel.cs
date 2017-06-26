using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL.Models
{
    public class BookShortInfoViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthorName { get; set; }
        public string BookDescription { get; set; }
        public BitmapImage BookImage { get; set; }
        public decimal BookPrice { get; set; }
    }
}
