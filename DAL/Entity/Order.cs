using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public int OrderPrice { get; set; }
        public int BookCount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
