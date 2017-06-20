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
using System.Collections.ObjectModel;
using BLL.Concrete;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        private ObservableCollection<OrderInfoViewModel> books = null;

        public Basket()
        {
            InitializeComponent();
        }

        public Basket(ObservableCollection<OrderInfoViewModel> _books)
        {
            InitializeComponent();
            books = _books;
            basketLb.DataContext = books;
            SumaAllBooksInBasket();
        }

        private void continueSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void subtractBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = basketLb.Items.IndexOf(((Button)sender).DataContext);

            if (books[index].Count == 1)
            {
                return;
            }

            books[index].Count--;
            SumaAllBooksInBasket();
            basketLb.Items.Refresh();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = basketLb.Items.IndexOf(((Button)sender).DataContext);
            books[index].Count++;
            SumaAllBooksInBasket();
            basketLb.Items.Refresh();
        }

        private void removeBookInBasket_Click(object sender, RoutedEventArgs e)
        {
            int index = basketLb.Items.IndexOf(((Button)sender).DataContext);
            books.RemoveAt(index);
            SumaAllBooksInBasket();
        }

        private void basketLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SumaAllBooksInBasket()
        {
            int res = 0;

            for(int i = 0; i < books.Count; i++)
            {
                res += books[i].Count * books[i].Price;
            }

            costLbl.Content = res.ToString();
        }
    }
}
