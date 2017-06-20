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
       void AddToBasket(int bookId, ObservableCollection<OrderInfoViewModel> booksInBasket);
    }
}
