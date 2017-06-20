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

        public bool AddOrder(OrderAddViewModel orderModel, string userEmail)
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

        public List<OrderInfoViewModel> GetBasket()
        {
            return orderRepository.GetOrders()
                .Where(order => order.IsPaid == false)
                .Select(o => new OrderInfoViewModel()
                {
                    AuthorName = o.Book.BookAuthor.AuthorName,
                    BookId = o.BookId,
                    BookImagePath = o.Book.BookImage.PathToImageFile,
                    BookName = o.Book.BookName,
                    Count = o.BookCount,
                    Price = o.Book.Price,
                    Cost = o.BookCount * o.Book.Price
                }).ToList();
        }

        public List<OrderInfoViewModel> GetHistory()
        {
            return orderRepository.GetOrders()
                .Where(order => order.IsPaid == true)
                .Select(o => new OrderInfoViewModel()
                {
                    AuthorName = o.Book.BookAuthor.AuthorName,
                    BookId = o.BookId,
                    BookImagePath = o.Book.BookImage.PathToImageFile,
                    BookName = o.Book.BookName,
                    Count = o.BookCount,
                    Price = o.Book.Price,
                    Cost = o.BookCount * o.Book.Price
                }).ToList();
        }
    }
}
