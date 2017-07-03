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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Models;
using BLL.Concrete;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookProvider bookProvider = new BookProvider();
        private SearchBooksModel searchBooks = new SearchBooksModel();
        private UserInfoViewModel user = null;
        private List<BookShortInfoViewModel> booksShortInfo = new List<BookShortInfoViewModel>();
        private ObservableCollection<OrderInfoViewModel> booksInBasket = new ObservableCollection<OrderInfoViewModel>();
        private int currentPage = 1;
        private int maxPage;

        public MainWindow()
        {
            InitializeComponent();
            FillCategorisLb();
            SetBooks();
            //userPhoto.Source = new BitmapImage(new Uri(@"E:\tp\WPF_UI\Images\nonePhoto.jpg"));
        }

        private async void SetBooks()
        {
            searchBooks = await bookProvider.GetBooks();
            shortBooksInfoLb.ItemsSource = searchBooks.books;
            CurrentPageLabel.Content = $"Page 1/{searchBooks.Pages}";
            maxPage = searchBooks.Pages;
        }

        private async void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            booksShortInfo = bookProvider.SearchBooks(SearchTextBox.Text);
            shortBooksInfoLb.ItemsSource = booksShortInfo;
           
        }

        private async void FillBasketUI()
        {
            BasketUIProvider basketUiProvider = new BasketUIProvider();
            booksInBasket = await basketUiProvider.GetBasket(user.UserId);
            UpdateCountBooksInBasket();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(user);
            login.ShowDialog();
            user = login.user;

            if (user != null)
            {
                cartBtn.Visibility = Visibility.Visible;
                showUserProfileBtn.Visibility = Visibility.Visible;
                loginBtn.Visibility = Visibility.Hidden;
                //SetAvatarImage(user.AvatarPath);
                SetBasketImage();
                FillBasketUI();
                UpdateCountBooksInBasket();
            }
        }

        private void showUserProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            PrivateOffice office = new PrivateOffice(user);
            office.ShowDialog();
        }

        private void booksLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(shortBooksInfoLb.SelectedIndex == -1)
            {
                return;
            }

            BookInfo info = new BookInfo(searchBooks.books[shortBooksInfoLb.SelectedIndex], user, booksInBasket);
            info.ShowDialog();
            shortBooksInfoLb.SelectedIndex = -1;
        }

        private void cartBtn_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket(booksInBasket);
            basket.ShowDialog();
            UpdateCountBooksInBasket();
        }

        private void FillCategorisLb()
        {
            categoriesLb.ItemsSource = new CategoryProvider().GetNameCategories();
        }

        private void addToBasket_Click(object sender, RoutedEventArgs e)
        {
            if(user == null)
            {
                MessageBox.Show("Login before buy book");
                return;
            }

            Button button = sender as Button;
            int index = shortBooksInfoLb.Items.IndexOf(button.DataContext);
            BasketUIProvider basketUIProvider = new BasketUIProvider();
            basketUIProvider.AddToBasket(searchBooks.books[index], booksInBasket, user.UserEmail);
            UpdateCountBooksInBasket();
        }

        private void SetBasketImage()
        {
            string imagesLocation = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Images")).LocalPath;
            string basketPath = imagesLocation + "\\cart.png";
            basketImg.Source = new BitmapImage(new Uri(basketPath));
        }

        private void SetAvatarImage(string avatarPath)
        {
            string imagesLocation = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Images")).LocalPath;

            if (avatarPath != null)
            {

            }
            else
            {
                imagesLocation += "\\nonePhoto.jpg";
                //userPhoto.Source = new BitmapImage(new Uri(imagesLocation));
            }
        }

        private void categoriesLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            booksShortInfo = bookProvider.GetBooksByCategoty((categoriesLb.SelectedItem as CategoryViewModel).NameCategory);
            shortBooksInfoLb.ItemsSource = booksShortInfo;
        }

        private void UpdateCountBooksInBasket()
        {
            countBooksInBasketLbl.Content = booksInBasket.Sum(i => i.Count);
        }

        private async void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage + 1 <= searchBooks.Pages)
            {
                searchBooks.books = await bookProvider.GetNextPage(currentPage);
                shortBooksInfoLb.ItemsSource = searchBooks.books;
                currentPage += 1;
                CurrentPageLabel.Content = $"Page {currentPage}/{searchBooks.Pages}";
            }

        }

        private async void previouslyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage - 1 != 0)
            {
                searchBooks.books = await bookProvider.GetPreviouslyPage(currentPage);
                shortBooksInfoLb.ItemsSource = searchBooks.books;
                currentPage -= 1;
                CurrentPageLabel.Content = $"Page {currentPage}/{searchBooks.Pages}";
            }
        }

        private async void lastPageBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBooks.books = await bookProvider.GetNextPage(maxPage - 1);
            shortBooksInfoLb.ItemsSource = searchBooks.books;
            currentPage = maxPage;
            CurrentPageLabel.Content = $"Page {currentPage}/{searchBooks.Pages}";
        }

        private async void firstButon_Click(object sender, RoutedEventArgs e)
        {
            searchBooks.books = await bookProvider.GetPreviouslyPage(2);
            shortBooksInfoLb.ItemsSource = searchBooks.books;
            currentPage = 1;
            CurrentPageLabel.Content = $"Page {currentPage}/{searchBooks.Pages}";
        }
    }
}
