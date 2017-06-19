using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ReviewViewModel
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }
    }
}
