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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserInfoViewModel user = null;
        private List<BookShortInfoViewModel> booksShortInfo = new List<BookShortInfoViewModel>();
        private List<BookInBasketViewModel> booksInBasket = new List<BookInBasketViewModel>();

        public MainWindow()
        {
            //hello
            InitializeComponent();
            BookProvider providerBook = new BookProvider();
            booksShortInfo = providerBook.GetBooks();
            shortBooksInfoLb.ItemsSource = booksShortInfo;
            FillCategorisLb();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            ImageProvider provider = new ImageProvider();
            provider.SaveImage(@"C:\Users\v.matviichuk\Downloads\hs-2015-02-a-hires_jpg.jpg");
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
            }
        }

        private void showUserProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            PrivateOffice office = new PrivateOffice(user);
            office.Show();
        }

        private void booksLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(shortBooksInfoLb.SelectedIndex == -1)
            {
                return;
            }

            BookInfo info = new BookInfo(booksShortInfo[shortBooksInfoLb.SelectedIndex].BookId, user);
            info.ShowDialog();
            shortBooksInfoLb.SelectedIndex = -1;
        }

        private void cartBtn_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket(booksInBasket);
            basket.Show();
        }

        private void FillCategorisLb()
        {
            categoriesLb.ItemsSource = new CategoryProvider().GetNameCategories();
        }

        private void addToBasket_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = shortBooksInfoLb.Items.IndexOf(button.DataContext);
            ShortBookInfoVMToBookInBastekVm(booksShortInfo[index].BookId);
        }

        private void ShortBookInfoVMToBookInBastekVm(int bookId)
        {
            BookProvider bookProvider = new BookProvider();
            BookInfoViewModel book = bookProvider.GetBookInfo(bookId);

            BookInBasketViewModel bookInBastek = new BookInBasketViewModel
            {
                BookId = bookId,
                AuthorName = book.AuthorName,
                BookImagePath = book.BookImagePath,
                BookName = book.BookName,
                Count = 1,
                Price = book.BookPrice,
                Cost = book.BookPrice
            };

            booksInBasket.Add(bookInBastek);
        }
    }
}
