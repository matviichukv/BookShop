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
using BLL.Concrete;
using BLL.Models;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public UserInfoViewModel user { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        public Login(UserInfoViewModel _user)
        {
            InitializeComponent();
            user = _user;
        }

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            UserProvider userProvider = new UserProvider();
            user = userProvider.GetUserInfo(EmailTb.Text, passwordPb.Password);

            if(user == null)
            {
                MessageBox.Show("Error.Try again!");
            }
            else
            {
                this.Close();
            }
        }

        private void createAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.ShowDialog();
        }

    }
}
