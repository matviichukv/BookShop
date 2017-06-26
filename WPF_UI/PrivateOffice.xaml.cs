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
        private List<OrderHistoryViewModel> ordersHistory = null;

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
            ordersHistory = orderProvider.GetOrdersHistory(user.UserId);
            orderHistoryLb.ItemsSource = GetListBoxOrdersHistory(ordersHistory);
            SumAllCostOrdersHistory();
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

        private List<ListBoxItem> GetListBoxOrdersHistory(List<OrderHistoryViewModel> ordersHistory)
        {
            List<ListBoxItem> result = new List<ListBoxItem>();
            for (int i = 0; i < ordersHistory.Count; i++)
            {
                ListBoxItem itemLb = new ListBoxItem();
                itemLb.HorizontalAlignment = HorizontalAlignment.Center;
                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = Brushes.Gray;
                WrapPanel wrap = new WrapPanel();
                Label orderdate = new Label();
                orderdate.Content = ordersHistory[i].Date;
                Label count = new Label();
                count.Content = "Count:";
                Label orderCount = new Label();
                orderCount.Content = ordersHistory[i].Cout;
                Label cost = new Label();
                cost.Content = "Cost:";
                Label orderCost = new Label();
                orderCost.Content = ordersHistory[i].Cost;
                wrap.Children.Add(orderdate);
                wrap.Children.Add(count);
                wrap.Children.Add(orderCount);
                wrap.Children.Add(cost);
                wrap.Children.Add(orderCost);
                stackPanel.Children.Add(wrap);
                Expander expander = new Expander();
                expander.Width = 340;
                ListBox infoOrderLb = new ListBox();
                
                for (int j = 0; j < ordersHistory[i].Orders.Count; j++)
                {
                    infoOrderLb.Items.Add(GetOrderHistoryBookInfoListboxItem(ordersHistory[i].Orders[j]));
                }

                expander.Content = infoOrderLb;
                stackPanel.Children.Add(expander);
                itemLb.Content = stackPanel;
                result.Add(itemLb);
            }

            return result;
        }

        private ListBoxItem GetOrderHistoryBookInfoListboxItem(OrderInfoViewModel order)
        {
            ListBoxItem item = new ListBoxItem();
            DockPanel dockPanel = new DockPanel();
            Image img = new Image();
            Label bookName = new Label();
            Label authorName = new Label();
            Label price = new Label();
            Label countOrderLbl = new Label();
            Label costOrderLBl = new Label();
            dockPanel.HorizontalAlignment = HorizontalAlignment.Center;
            dockPanel.Margin = new Thickness(10, 10, 10, 10);
            img.Height = 100;
            img.Width = 50;
            img.Margin = new Thickness(10, 0, 10, 0);
            authorName.Content = order.AuthorName;
            price.Content = "Price: " + order.Price;
            countOrderLbl.Content = "Count: " + order.Count;
            costOrderLBl.Content = "Cost: " + order.Cost;
            DockPanel.SetDock(img, Dock.Left);
            DockPanel.SetDock(bookName, Dock.Top);
            DockPanel.SetDock(authorName, Dock.Top);
            DockPanel.SetDock(price, Dock.Top);
            DockPanel.SetDock(countOrderLbl, Dock.Top);
            DockPanel.SetDock(costOrderLBl, Dock.Top);
            dockPanel.Children.Add(img);
            bookName.Content = order.BookName;
            dockPanel.Children.Add(bookName);
            dockPanel.Children.Add(authorName);
            dockPanel.Children.Add(price);
            dockPanel.Children.Add(countOrderLbl);
            dockPanel.Children.Add(costOrderLBl);
            item.Content = dockPanel;
            return item;
        }

        private void SumAllCostOrdersHistory()
        {
            ordersHistoryCostLbl.Content = ordersHistory.Sum(i => i.Cost);
        }
    }
}
