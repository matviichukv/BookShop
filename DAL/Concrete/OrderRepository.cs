using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entity;

namespace DAL.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        BookShopContext ctx = new BookShopContext();

        public List<Order> GetOrders()
        {
            return ctx.Orders
                .Include(o => o.Book)
                .Include(o => o.Book.BookAuthor)
                .Include(o => o.Book.BookImage)
                .ToList();
        }
    }
}
