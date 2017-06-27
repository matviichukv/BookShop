using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SearchBooksModel
    {
        public List<BookShortInfoViewModel> books { get; set; }
        public int Pages { get; set; }

        public SearchBooksModel()
        {
            books = new List<BookShortInfoViewModel>();
        }
    }
}
