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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        private List<BookInBasketViewModel> books = null;

        public Basket()
        {
            InitializeComponent();
        }

        public Basket(List<BookInBasketViewModel> _books)
        {
            InitializeComponent();
            books = _books;
            basketLb.ItemsSource = books;
        }

        private void continueSearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
