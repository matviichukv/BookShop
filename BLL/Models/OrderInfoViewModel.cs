using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class OrderInfoViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string BookImagePath { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
    }
}
