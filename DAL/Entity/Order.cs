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
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
