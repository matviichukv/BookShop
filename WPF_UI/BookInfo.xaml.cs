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
using System.Collections.ObjectModel;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for BookInfo.xaml
    /// </summary>
    public partial class BookInfo : Window
    {
        private BookProvider bookProvider = new BookProvider();
        private BookInfoViewModel bookInfo = null;
        private UserInfoViewModel user = null;
        private ObservableCollection<OrderInfoViewModel> booksInBasket;
        private int bookId;

        public BookInfo()
        {
            InitializeComponent();
        }

        public BookInfo(int _bookId, UserInfoViewModel _user, ObservableCollection<OrderInfoViewModel> _booksInBasket)
        {
            InitializeComponent();
            user = _user;
            bookId = _bookId;
            booksInBasket = _booksInBasket;
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

        private void sendReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user == null)
            {
                MessageBox.Show("Error.Need sign in if you want send review.");
                return;
            }

            SendReview sendReview = new SendReview(user, bookId);
            sendReview.ShowDialog();
            UpdateReviews();
        }

        private void UpdateReviews()
        {
            ReviewProvider reviewProvider = new ReviewProvider();
            var reviews = reviewProvider.GetBookReviews(bookId);
            bookInfo.BookReviews.Clear();
            foreach (var item in reviews)
            {
                bookInfo.BookReviews.Add(item);
            }
        }


        private void continueSearchGoodsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addToBasket_Click(object sender, RoutedEventArgs e)
        {
            BasketUIProvider basketUIProvider = new BasketUIProvider();
            basketUIProvider.AddToBasket(bookId, booksInBasket, user.UserEmail);
        }
    }
}
