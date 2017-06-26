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
                .Where(order => order.IsPaid == true && order.UserId == userId).ToList();

            List<OrderHistoryViewModel> ordersHistory = new List<OrderHistoryViewModel>();

            OrderHistoryViewModel firstOrder = new OrderHistoryViewModel();
            firstOrder.Date = (DateTime)orders[0].DateOrdered;
            firstOrder.Cost = orders[0].OrderPrice;
            firstOrder.Cout = orders[0].BookCount;
            firstOrder.Orders.Add(await GetOrderInfoVM(orders[0]));
            ordersHistory.Add(firstOrder);

            for (int i = 1; i < orders.Count; i++)
            {
                for (int j = 0; j < ordersHistory.Count; j++)
                {
                    if (ordersHistory[j].Date.ToShortDateString() == orders[i].DateOrdered.Value.ToShortDateString())
                    {
                        ordersHistory[j].Cost += orders[i].OrderPrice;
                        ordersHistory[j].Cout += orders[i].BookCount;
                        ordersHistory[j].Orders.Add(await GetOrderInfoVM(orders[i]));
                        break;
                    }
                    
                    if(ordersHistory.Count -1 == j)
                    {
                        OrderHistoryViewModel newDateOrder = new OrderHistoryViewModel();
                        newDateOrder.Date = (DateTime)orders[i].DateOrdered;
                        newDateOrder.Cost = orders[i].OrderPrice;
                        newDateOrder.Cout = orders[i].BookCount;
                        newDateOrder.Orders.Add(await GetOrderInfoVM(orders[i]));
                        ordersHistory.Add(newDateOrder);
                        break;
                    }
                }
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
