using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Abstract
{
    public interface IBasketUIProvider
    {
        void AddToBasket(BookShortInfoViewModel bookInfo, ObservableCollection<OrderInfoViewModel> booksInBasket, string userEmail);
        Task<ObservableCollection<OrderInfoViewModel>> GetBasket(int userId);
    }
}
