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

        public int AddOrder(Order order)
        {
            try
            {
                ctx.Orders.Add(order);
                ctx.SaveChanges();
                return order.OrderId;
            }
            catch { return -1; }
        }

        public bool ConfirmOrder(int orderId)
        {
            try
            {
                var orders = ctx.Orders.Include(b => b.Book).FirstOrDefault(o => o.OrderId == orderId);

                if (orders.Book.Count >= orders.BookCount)
                {
                    orders.IsPaid = true;
                    orders.DateOrdered = DateTime.Now;
                    orders.Book.Count -= orders.BookCount;
                }
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
                ctx.SaveChanges();
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

        void IOrderRepository.UpdateBookCount(int orderId, int bookCount)
        {
            var order = ctx.Orders.Include(i => i.Book).FirstOrDefault(o => o.OrderId == orderId);
            order.BookCount = bookCount;
            order.OrderPrice = order.Book.Price * order.BookCount;
            ctx.SaveChanges();
        }
    }
}
