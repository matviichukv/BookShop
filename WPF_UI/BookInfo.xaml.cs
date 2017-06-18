using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Models;
using BLL.Concrete;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for BookInfo.xaml
    /// </summary>
    public partial class BookInfo : Window
    {
        private BookProvider bookProvider = new BookProvider();
        private BookInfoViewModel bookInfo = null;

        public BookInfo()
        {
            InitializeComponent();
        }

        public BookInfo(int bookId)
        {
            InitializeComponent();
            bookInfo = bookProvider.GetBookInfo(bookId);
            FillBookInfo(bookInfo);
        }

        private void FillBookInfo(BookInfoViewModel bookInfo)
        {
            bookNameLB.Content = bookInfo.BookName;
            authorNameLb.Content = bookInfo.AuthorName;
            publisherNameLb.Content = bookInfo.PublisherName;
            languageNameLb.Content = bookInfo.BookLanguage;
            publishDateLb.Content = bookInfo.DatePublished;
            volumeLb.Content = bookInfo.BookVolume;
            priceLb.Content = bookInfo.BookPrice;
            descriptionBookTb.Text = bookInfo.BookDescription;

            infoAuthorNameLB.Content = bookInfo.AuthorName;
            descriptionAuthorTb.Text = bookInfo.AuthorDescription;

            reviewLB.ItemsSource = bookInfo.BookReviews;
        }
    }
}
