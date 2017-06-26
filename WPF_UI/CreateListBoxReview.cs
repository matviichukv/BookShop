using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BLL.Concrete;
using BLL.Models;
using System.Windows;

namespace WPF_UI
{
    public class CreateListBoxReview
    {
        private enum GetReview { userReview, bookReview}
        RoutedEventHandler eventClick = null;

        public CreateListBoxReview()
        {
        
        }

        public CreateListBoxReview(RoutedEventHandler _eventClick)
        {
            eventClick = _eventClick;
        }

        public List<ListBoxItem> GetListBoxReviewItems(List<ReviewViewModel> reviews, int choose)
        {
            List<ListBoxItem> res = new List<ListBoxItem>();

            for (int i = 0; i < reviews.Count; i++)
            {
                res.Add(GetReviewListBoxItem(reviews[i], choose));
            }

            return res;
        }

        private ListBoxItem GetReviewListBoxItem(ReviewViewModel review, int choose)
        {
            ListBoxItem item = new ListBoxItem();
            DockPanel dockPanel = new DockPanel();
            Label bookNameLbl = new Label();

            if(choose == (int)GetReview.bookReview)
            {
                bookNameLbl.Content = review.UserName;
            }
            else if(choose == (int)GetReview.userReview)
            {
                bookNameLbl.Content = review.BookName;
            }

            Label reviewDateLbl = new Label();
            reviewDateLbl.Content = review.Date;
            TextBox messageTb = new TextBox();
            messageTb.AcceptsReturn = true;
            messageTb.MaxLines = 2;
            messageTb.MinLines = 2;
            if (choose == (int)GetReview.bookReview)
            {
                messageTb.Width = 440;
            }
            else if (choose == (int)GetReview.userReview)
            {
                messageTb.Width = 330;
            }

            messageTb.VerticalAlignment = VerticalAlignment.Bottom;
            messageTb.IsReadOnly = true;
            messageTb.Text = review.Message;

            WrapPanel wrap = GetReviewWrapPanel(review, choose);

            DockPanel.SetDock(bookNameLbl, Dock.Top);
            DockPanel.SetDock(reviewDateLbl, Dock.Top);
            DockPanel.SetDock(messageTb, Dock.Top);
            DockPanel.SetDock(wrap, Dock.Bottom);
            dockPanel.Children.Add(bookNameLbl);
            dockPanel.Children.Add(reviewDateLbl);
            dockPanel.Children.Add(messageTb);
            dockPanel.Children.Add(wrap);
            item.Content = dockPanel;
            return item;
        }

        private WrapPanel GetReviewWrapPanel(ReviewViewModel review, int choose)
        {
            WrapPanel wrap = new WrapPanel();
            Button btn = new Button();

            if (choose == (int)GetReview.bookReview)
            {
                btn.Click += eventClick;
            }

            btn.DataContext = review.ReviewId;
            string imagesLocation = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Images")).LocalPath;
            imagesLocation += "\\thumb_up.png";
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(imagesLocation));
            image.Height = 25;
            image.Width = 25;
            btn.Content = image;
            wrap.Children.Add(btn);
            Label countLikes = new Label();
            countLikes.Content = review.Likes;
            wrap.Children.Add(countLikes);
            return wrap;
        }
    }
}
