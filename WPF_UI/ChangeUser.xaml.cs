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
using Microsoft.Win32;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for ChangeUser.xaml
    /// </summary>
    public partial class ChangeUser : Window
    {
        private enum Initials { lastName, firstName };
        private UserInfoViewModel user;
        private string imagePath = string.Empty;

        public ChangeUser()
        {
            InitializeComponent();
        }

        public ChangeUser(UserInfoViewModel _user)
        {
            InitializeComponent();
            user = _user;
            FillUIUserFields();
        }

        private void addPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                ImageSource imageSource = new BitmapImage(new Uri(openFile.FileName));
                userPhotoImg.Source = imageSource;
                imagePath = openFile.FileName;
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if(emailTb.Text == "" || firstNameTb.Text == "" || lastNameTb.Text == ""
                || oldPasswordPb.Password == "" || newPasswordPb.Password == "" || confirmNewPasswordPB.Password == "")
            {
                MessageBox.Show("Enter all fields");
                return;
            }
            
            if(newPasswordPb.Password != confirmNewPasswordPB.Password)
            {
                MessageBox.Show("Password and confirm password different");
                return;
            }

            UserChangeViewModel userChange = new UserChangeViewModel
            {
                Email = emailTb.Text,
                OldPassword = oldPasswordPb.Password,
                NewPassword = newPasswordPb.Password,
                NewUserName = lastNameTb.Text + " " + firstNameTb.Text,
                NewImagePath = imagePath == string.Empty ? null : imagePath
            };

            UserProvider userProvider = new UserProvider();
            if (userProvider.EditUserInfo(userChange))
            {
                user.UserEmail = userChange.Email;
                user.UserName = userChange.NewUserName;
                user.UserPassword = userChange.NewPassword;
                //user.AvatarPath = userChange.NewImagePath;
                this.Close();
            }
            else
            {
                MessageBox.Show("Old password wrong");
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FillUIUserFields()
        {
            var splitInitials = user.UserName.Split(' ');
            emailTb.Text = user.UserEmail;
            firstNameTb.Text = splitInitials[(int)Initials.firstName];
            lastNameTb.Text = splitInitials[(int)Initials.lastName];

            //if (user.AvatarPath != null)
            //{
            //    ImageSource imageSource = new BitmapImage(new Uri(user.AvatarPath));
            //    userPhotoImg.Source = imageSource;
            //    imagePath = user.AvatarPath;
            //}
        }
    }
}
