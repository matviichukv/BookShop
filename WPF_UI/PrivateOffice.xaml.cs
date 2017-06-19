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
    /// Interaction logic for PrivateOffice.xaml
    /// </summary>
    public partial class PrivateOffice : Window
    {
        private UserInfoViewModel user;

        public PrivateOffice()
        {
            InitializeComponent();
        }

        public PrivateOffice(UserInfoViewModel _user)
        {
            InitializeComponent();
            user = _user;
            FillUserInfo();
        }

        private void personalInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 0;
        }

        private void historyOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 1;
        }

        private void historyReviews_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 2;
            ReviewProvider reviewProvider = new ReviewProvider();
            reviewsHistoryLb.ItemsSource = reviewProvider.GetUserReviews(user.UserId);
        }

        private void backToMainWindowBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void FillUserInfo()
        {
            userNameLb.Content = user.UserName;
            userEmailLb.Content = user.UserEmail;
        }
    }
}
