﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class OrderAddViewModel
    {
        public int BookId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
