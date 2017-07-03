using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Concrete;
using DAL.Abstract;
using DAL.Entity;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

namespace BLL.Concrete
{
    public class OrderProvider : IOrderProvider
    {
        IOrderRepository orderRepository = new OrderRepository();
        IUserRepository userRepository = new UserRepository();
        
        public bool RemoveOrder(int orderId)
        {
            return orderRepository.RemoveOrder(orderId);
        }

        public int AddOrder(OrderAddViewModel orderModel, string userEmail)
        {
            Order newOrder = new Order()
            {
                BookId = orderModel.BookId,
                BookCount = orderModel.Count,
                IsPaid = false,
                OrderPrice = orderModel.Price,
                UserId = userRepository.GetUserByEmail(userEmail).UserId,
                DateOrdered = null
            };

            return orderRepository.AddOrder(newOrder);
        }

        public bool ConfirmOrder(int orderId)
        {
            return orderRepository.ConfirmOrder(orderId);
        }

        public async Task<List<OrderInfoViewModel>> GetBasket(int userId)
        {
           var basket = orderRepository.GetOrders().Where(order => order.IsPaid == false && order.UserId == userId);
            List<OrderInfoViewModel> booksInBasket = new List<OrderInfoViewModel>();

            foreach(var item in basket)
            {
                booksInBasket.Add(await GetOrderInfoVM(item));
            }

            return booksInBasket;
        }

        public void UpdateBookCount(int orderId, int bookCount)
        {
            orderRepository.UpdateBookCount(orderId, bookCount);
        }

        public async Task<List<OrderHistoryViewModel>> GetOrdersHistory(int userId)
        {
            var orders = orderRepository.GetOrders()
                .Where(order => order.IsPaid == true && order.UserId == userId)
                .GroupBy(i => i.DateOrdered.Value.ToShortDateString()).ToList();

            List<OrderHistoryViewModel> ordersHistory = new List<OrderHistoryViewModel>();

            foreach (IGrouping<string, Order> keyGroup in orders)
            {
                OrderHistoryViewModel historyModel = new OrderHistoryViewModel();

                foreach (Order order in keyGroup)
                {
                    historyModel.Date = order.DateOrdered.Value;
                    historyModel.Cost += order.OrderPrice;
                    historyModel.Cout += order.BookCount;
                    historyModel.Orders.Add(await GetOrderInfoVM(order));
                }

                ordersHistory.Add(historyModel);
            }

            return ordersHistory;
        }

        private async Task<OrderInfoViewModel> GetOrderInfoVM(Order order)
        {
            ImageProvider imageProvider = new ImageProvider();
            return new OrderInfoViewModel
            {
                BookId = order.BookId,
                OrderId = order.OrderId,
                BookImage = order.Book.BookImage == null? null : BitmapToImageSource(await imageProvider.GetImage((int)order.Book.ImageId)),
                BookName = order.Book.BookName,
                AuthorName = order.Book.BookAuthor.AuthorName,
                Cost = order.OrderPrice,
                Count = order.BookCount,
                Price = order.Book.Price
            };
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
