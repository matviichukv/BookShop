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

        public bool AddOrder(Order order)
        {
            try
            {
                ctx.Orders.Add(order);
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool ConfirmOrder(int orderId)
        {
            try
            {
                ctx.Orders.FirstOrDefault(o => o.OrderId == orderId).IsPaid = true;
                ctx.Orders.FirstOrDefault(o => o.OrderId == orderId).DateOrdered = DateTime.Now;
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveOrder(int orderId)
        {
            try
            {
                ctx.Orders.Remove(ctx.Orders.FirstOrDefault(o => o.OrderId == orderId));
                return true;
            }
            catch { return false; }
        }

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
