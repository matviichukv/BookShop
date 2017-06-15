using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    interface IBookProvider
    {
        BookInfoViewModel GetBookInfo(int bookId);
    }
}
