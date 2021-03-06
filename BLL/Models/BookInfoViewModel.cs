﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL.Models
{
    public class BookInfoViewModel
    {
        public string BookName { get; set; }
        public DateTime DatePublished { get; set; }
        public int BookVolume { get; set; }
        public string BookLanguage { get; set; }
        public string BookDescription { get; set; }
        public decimal BookPrice { get; set; }
        public int BookCount { get; set; }

        public string AuthorName { get; set; }
        public string AuthorDescription { get; set; }
        public string AuthorNationality { get; set; }
        public BitmapImage AuthorImage { get; set; }

        public string PublisherName { get; set; }
        public string PublisherCity { get; set; }

        public BitmapImage BookImageSource { get; set; }
        public string CategoryName { get; set; }

        public ObservableCollection<ReviewViewModel> BookReviews { get; set; }
    }
}
