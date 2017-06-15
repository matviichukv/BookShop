using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IBookRepository
    {
        Book GetBookById(int bookId);
        List<Book> GetBooks();
    }
}
