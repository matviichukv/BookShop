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
    /// Interaction logic for SendReview.xaml
    /// </summary>
    public partial class SendReview : Window
    {
        private UserInfoViewModel user = null;
        private int bookId;

        public SendReview()
        {
            InitializeComponent();
        }

        public SendReview(UserInfoViewModel _user, int _bookId)
        {
            InitializeComponent();
            user = _user;
            bookId = _bookId;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            ReviewProvider reviewProvider = new ReviewProvider();
            reviewProvider.AddReview(messageTb.Text, bookId, user.UserId);
        }
    }
}
