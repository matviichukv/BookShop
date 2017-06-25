using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class OrderHistoryViewModel
    {
        public DateTime Date { get; set; }
        public int Cout { get; set; }
        public decimal Cost { get; set; }
        public List<OrderInfoViewModel> Orders = new List<OrderInfoViewModel>();
    }
}
