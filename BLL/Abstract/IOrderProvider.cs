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
        List<OrderInfoViewModel> GetBasket();
        List<OrderInfoViewModel> GetHistory();
    }
}
