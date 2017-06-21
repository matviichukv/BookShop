using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BookInfoViewModel
    {
        public string BookName { get; set; }
        public DateTime DatePublished { get; set; }
        public int BookVolume { get; set; }
        public string BookLanguage { get; set; }
        public string BookDescription { get; set; }
        public double BookPrice { get; set; }
        public int BookCount { get; set; }

        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
        public string AuthorNationality { get; set; }
        public string AuthorImagePath { get; set; }

        public string PublisherName { get; set; }
        public string PublisherCity { get; set; }

        public int ImageId { get; set; }
        public string CategoryName { get; set; }

        public ObservableCollection<ReviewViewModel> BookReviews { get; set; }
    }
}
