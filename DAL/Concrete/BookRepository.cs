using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;

namespace DAL.Concrete
{
    public class BookRepository : IBookRepository
    {
        BookShopContext ctx = new BookShopContext();

        public Book GetBookById(int bookId)
        {
            return ctx.Books.FirstOrDefault(book => book.BookId == bookId);
        }
    }
}
