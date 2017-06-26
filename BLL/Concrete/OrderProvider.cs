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

        public List<OrderInfoViewModel> GetBasket(int userId)
        {
            return orderRepository.GetOrders()
                .Where(order => order.IsPaid == false && order.UserId == userId)
                .Select(o => new OrderInfoViewModel()
                {
                    AuthorName = o.Book.BookAuthor.AuthorName,
                    BookId = o.BookId,
                    OrderId = o.OrderId,
                    BookImageId = o.Book.BookImage == null? 0 : o.Book.BookImage.ImageId,
                    BookName = o.Book.BookName,
                    Count = o.BookCount,
                    Price = o.Book.Price,
                    Cost = o.BookCount * o.Book.Price
                }).ToList();
        }

        public void UpdateBookCount(int orderId, int bookCount)
        {
            orderRepository.UpdateBookCount(orderId, bookCount);
        }

        public List<OrderHistoryViewModel> GetOrdersHistory(int userId)
        {
            var orders = orderRepository.GetOrders()
                .Where(order => order.IsPaid == true && order.UserId == userId).ToList();

            List<OrderHistoryViewModel> ordersHistory = new List<OrderHistoryViewModel>();

            OrderHistoryViewModel firstOrder = new OrderHistoryViewModel();
            firstOrder.Date = (DateTime)orders[0].DateOrdered;
            firstOrder.Cost = orders[0].OrderPrice;
            firstOrder.Cout = orders[0].BookCount;
            firstOrder.Orders.Add(GetOrderInfo(orders[0]));
            ordersHistory.Add(firstOrder);

            for (int i = 1; i < orders.Count; i++)
            {
                for (int j = 0; j < ordersHistory.Count; j++)
                {
                    if (ordersHistory[j].Date.ToShortDateString() == orders[i].DateOrdered.Value.ToShortDateString())
                    {
                        ordersHistory[j].Cost += orders[i].OrderPrice;
                        ordersHistory[j].Cout += orders[i].BookCount;
                        ordersHistory[j].Orders.Add(GetOrderInfo(orders[i]));
                        break;
                    }
                    else
                    {
                        OrderHistoryViewModel newDateOrder = new OrderHistoryViewModel();
                        newDateOrder.Date = (DateTime)orders[i].DateOrdered;
                        newDateOrder.Cost = orders[i].OrderPrice;
                        newDateOrder.Cout = orders[i].BookCount;
                        newDateOrder.Orders.Add(GetOrderInfo(orders[i]));
                        ordersHistory.Add(newDateOrder);
                        break;
                    }
                }
            }

            return ordersHistory;
        }

        private OrderInfoViewModel GetOrderInfo(Order order)
        {
            return new OrderInfoViewModel
            {
                BookId = order.BookId,
                BookImageId = 0,
                BookName = order.Book.BookName,
                AuthorName = order.Book.BookAuthor.AuthorName,
                Cost = order.OrderPrice,
                Count = order.BookCount,
                Price = order.Book.Price
            };
        }
    }
}
