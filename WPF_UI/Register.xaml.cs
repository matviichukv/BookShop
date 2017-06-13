using Microsoft.Win32;
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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if(emailTb.Text == "" || firstNameTb.Text == "" || lastNameTb.Text == "" 
                || passwordPb.Password == "" || confirmPasswordPB.Password == "")
            {
                MessageBox.Show("Fill all fields");
            }

            if(passwordPb.Password != confirmPasswordPB.Password)
            {
                MessageBox.Show("Password different");
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if(openFile.ShowDialog() == true)
            {
                ImageSource imageSource = new BitmapImage(new Uri(openFile.FileName));
                userPhotoImg.Source = imageSource;
            }
        }
    }
}
