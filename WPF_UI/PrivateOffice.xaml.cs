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
        private List<ReviewViewModel> userReviews = new List<ReviewViewModel>();

        public PrivateOffice()
        {
            InitializeComponent();
        }

        public PrivateOffice(UserInfoViewModel _user)
        {
            InitializeComponent();
            user = _user;
            FillUserInfo();
            //SetAvatarImage(user.AvatarPath);
        }

        private void personalInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 0;
        }

        private void historyOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 1;
            OrderProvider orderProvider = new OrderProvider();
        }

        private void historyReviews_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 2;
            ReviewProvider reviewProvider = new ReviewProvider();
            var reviews = reviewProvider.GetUserReviews(user.UserId).AsEnumerable().Reverse();
            CreateListBoxReview create = new CreateListBoxReview();
            reviewsHistoryLb.ItemsSource = create.GetListBoxReviewItems(reviews.ToList(),0);
        }

        private void backToMainWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void FillUserInfo()
        {
            userNameLb.Content = user.UserName;
            userEmailLb.Content = user.UserEmail;
        }

        private void changeProficeBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeUser change = new ChangeUser(user);
            change.ShowDialog();
            FillUserInfo();
        }

        private void SetAvatarImage(string avatarPath)
        {
            string imagesLocation = null;

            if (avatarPath != null)
            {

            }
            else
            {
                imagesLocation = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Images")).LocalPath;
                imagesLocation += "\\nonePhoto.jpg";
                userAvatar.Source = new BitmapImage(new Uri(imagesLocation));
            }
        }

        private void showOrderInDateExpanded_Expanded(object sender, RoutedEventArgs e)
        {
            
        }

        private void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            ReviewProvider reviewProvider = new ReviewProvider();
            Button button = sender as Button;
            var reviewId = button.DataContext;
            reviewProvider.PressLikeButton(user.UserEmail, (int)reviewId);
        }
    }
}
