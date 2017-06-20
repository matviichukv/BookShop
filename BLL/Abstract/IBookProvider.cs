using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IBookProvider
    {
        BookInfoViewModel GetBookInfo(int bookId);
        List<BookShortInfoViewModel> GetBooks();
        List<BookShortInfoViewModel> SearchBooks(string filter);
        List<BookShortInfoViewModel> GetBooksByCategoty(string category);
    }
}
