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
        private UserInfoViewModel user = null;
        private List<BookShortInfoViewModel> booksShortInfo = new List<BookShortInfoViewModel>();
        private ObservableCollection<OrderInfoViewModel> booksInBasket = new ObservableCollection<OrderInfoViewModel>();

        public MainWindow()
        {
            //hello
            InitializeComponent();
            booksShortInfo = bookProvider.GetBooks();
            shortBooksInfoLb.ItemsSource = booksShortInfo;
            FillCategorisLb();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            booksShortInfo = bookProvider.SearchBooks(SearchTextBox.Text);
            shortBooksInfoLb.ItemsSource = booksShortInfo;
            var provider = new ImageProvider();
            provider.SaveImage(@"C:\Users\v.matviichuk\Downloads\hi-512-14.jpg");
        }

        private void FillBasketUI()
        {
            BasketUIProvider basketUiProvider = new BasketUIProvider();
            basketUiProvider.FillBasket(booksInBasket, user.UserId);
            countBooksInBasketLbl.Content = booksInBasket.Sum(i => i.Count);
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(user);
            login.ShowDialog();
            user = login.user;

            if (user != null)
            {
                cartBtn.Visibility = Visibility.Visible;
                //showUserProfileBtn.Visibility = Visibility.Visible;
                loginBtn.Visibility = Visibility.Hidden;
                //SetAvatarImage(user.AvatarPath);
                SetBasketImage();
                FillBasketUI();
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

            BookInfo info = new BookInfo(booksShortInfo[shortBooksInfoLb.SelectedIndex], user, booksInBasket);
            info.ShowDialog();
            shortBooksInfoLb.SelectedIndex = -1;
        }

        private void cartBtn_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket(booksInBasket);
            basket.ShowDialog();
            countBooksInBasketLbl.Content = booksInBasket.Sum(i => i.Count);
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
            basketUIProvider.AddToBasket(booksShortInfo[index], booksInBasket, user.UserEmail);
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

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
