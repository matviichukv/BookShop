using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IOrderProvider
    {
        Task<List<OrderInfoViewModel>> GetBasket(int userId);
        Task<List<OrderHistoryViewModel>> GetOrdersHistory(int userId);
        int AddOrder(OrderAddViewModel orderModel, string userEmail);
        bool RemoveOrder(int orderId);
        bool ConfirmOrder(int orderId);
        void UpdateBookCount(int orderId, int bookCount);
    }
}
