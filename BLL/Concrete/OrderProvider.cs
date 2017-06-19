using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Concrete;

namespace BLL.Concrete
{
    public class OrderProvider : IOrderProvider
    {
        OrderRepository orderRepository = new OrderRepository();

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
