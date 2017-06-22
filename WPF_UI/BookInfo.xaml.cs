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
using System.Threading;
using System.Windows.Threading;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for BookInfo.xaml
    /// </summary>
    public partial class BookInfo : Window
    {
        private BookProvider bookProvider = new BookProvider();
        private BookInfoViewModel bookInfo = null;
        private BookShortInfoViewModel shortBookInfo;
        private UserInfoViewModel user = null;
        private ObservableCollection<OrderInfoViewModel> booksInBasket;
        private Thread updateReviewsThread;

        public BookInfo()
        {
            InitializeComponent();
        }

        public BookInfo(BookShortInfoViewModel _shortBookInfo, UserInfoViewModel _user, ObservableCollection<OrderInfoViewModel> _booksInBasket)
        {
            InitializeComponent();
            user = _user;
            shortBookInfo = _shortBookInfo;
            booksInBasket = _booksInBasket;
            bookInfo = bookProvider.GetBookInfo(_shortBookInfo.BookId);
            updateReviewsThread = new Thread(UpdateReviews);
            updateReviewsThread.Start();
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

            reviewLB.ItemsSource = bookInfo.BookReviews.Reverse();
        }

        private void sendReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user == null)
            {
                MessageBox.Show("Error.Need sign in if you want send review.");
                return;
            }


            SendReview sendReview = new SendReview(user, shortBookInfo.BookId);
            sendReview.ShowDialog();
        }

        private void UpdateReviews()
        {
            while (true)
            {
                ReviewProvider reviewProvider = new ReviewProvider();
                var reviews = reviewProvider.GetBookReviews(shortBookInfo.BookId).AsEnumerable().Reverse();
                Dispatcher.BeginInvoke((Action)(() => bookInfo.BookReviews.Clear()));

                foreach (var item in reviews)
                {
                    Dispatcher.BeginInvoke((Action)(() => bookInfo.BookReviews.Add(item)));
                }

                Dispatcher.BeginInvoke((Action)(() => reviewLB.ItemsSource = null));
                Dispatcher.BeginInvoke((Action)(() => reviewLB.ItemsSource = bookInfo.BookReviews));
                Thread.Sleep(5000);
            }
        }


        private void continueSearchGoodsBtn_Click(object sender, RoutedEventArgs e)
        {
            updateReviewsThread.Abort();
            this.Close();
        }

        private void addToBasket_Click(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("Login before buy book");
                return;
            }

            BasketUIProvider basketUIProvider = new BasketUIProvider();
            basketUIProvider.AddToBasket(shortBookInfo, booksInBasket, user.UserEmail);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            updateReviewsThread.Abort();
        }
    }
}
