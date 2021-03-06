﻿using System;
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
        private RoutedEventHandler eventClick = null;

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
            SetBookInfo(_shortBookInfo);
            eventClick = likeBtn_Click;
        }

        private async void SetBookInfo(BookShortInfoViewModel _shortBookInfo)
        {
            bookInfo = await bookProvider.GetBookInfo(_shortBookInfo.BookId);
            bookNameLB.Content = bookInfo.BookName;
            authorNameLb.Content = bookInfo.AuthorName;
            publisherNameLb.Content = bookInfo.PublisherName;
            languageNameLb.Content = bookInfo.BookLanguage;
            publishDateLb.Content = bookInfo.DatePublished;
            volumeLb.Content = bookInfo.BookVolume;
            priceLb.Content = bookInfo.BookPrice;
            descriptionBookTb.Text = bookInfo.BookDescription;
            bookImage.Source = bookInfo.BookImageSource;
            infoAuthorNameLB.Content = bookInfo.AuthorName;
            descriptionAuthorTb.Text = bookInfo.AuthorDescription;
            authorImage.Source = bookInfo.AuthorImage;
            updateReviewsThread = new Thread(UpdateReviews);
            updateReviewsThread.Start();
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
                CreateListBoxReview create = new CreateListBoxReview(eventClick);
                Dispatcher.BeginInvoke((Action)(() => reviewLB.ItemsSource = null));
                Dispatcher.BeginInvoke((Action)(() => reviewLB.ItemsSource = create.GetListBoxReviewItems(reviews.ToList(),1)));
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

        private void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user == null)
            {
                MessageBox.Show("Login before press like");
                return;
            }

            ReviewProvider reviewProvider = new ReviewProvider();
            Button button = sender as Button;
            var reviewId =  button.DataContext;
            reviewProvider.PressLikeButton(user.UserEmail, (int)reviewId);
        }
    }
}
