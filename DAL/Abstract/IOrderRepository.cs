using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        int AddOrder(Order order);
        bool RemoveOrder(int orderId);
        bool ConfirmOrder(int orderId);
    }
}
