using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entity;

namespace DAL.Concrete
{
    public class BookRepository : IBookRepository
    {
        BookShopContext ctx = new BookShopContext();

        public Book GetBookById(int bookId)
        {
            return ctx.Books
                .Include(b => b.BookCategory)
                .Include(b => b.BookImage)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookAuthor)
                .Include(b => b.BookReviews)
                .Include(b => b.BookReviews.Select(r => r.User))
                .FirstOrDefault(book => book.BookId == bookId);
        }

        public List<Book> GetBooks()
        {
            return ctx.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookImage)
                .ToList();
        }
    }
}
