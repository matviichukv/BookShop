using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL.Models
{
    public class OrderInfoViewModel
    {
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public BitmapImage BookImage { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
    }
}
