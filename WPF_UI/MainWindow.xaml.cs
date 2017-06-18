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
        UserInfoViewModel user = null;

        public MainWindow()
        {
            //hello
            BookProvider providerBook = new BookProvider();
            var list = providerBook.GetBooks();
            InitializeComponent();
            list[0].BookImagePath = @"E:\img_0335.jpg";
            listboxFolder1.ItemsSource = list;
            FillCategorisLb();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            
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
            if(listboxFolder1.SelectedIndex == -1)
            {
                return;
            }

            listboxFolder1.SelectedIndex = -1;
            BookInfo info = new BookInfo();
            info.Show();
        }

        private void cartBtn_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket();
            basket.Show();
        }

        private void FillCategorisLb()
        {
            categoriesLb.ItemsSource = new CategoryProvider().GetNameCategories();
        }
    }
}
