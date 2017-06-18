using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BLL.Models;
using DAL.Concrete;
using DAL.Entity;
using DAL.Abstract;

namespace BLL.Concrete
{
    public class BookProvider : IBookProvider
    {
        IBookRepository bookRepository = new BookRepository();

        public BookInfoViewModel GetBookInfo(int bookId)
        {
            Book book = bookRepository.GetBookById(bookId);
            List<ReviewViewModel> reviews = new List<ReviewViewModel>();

            foreach (var review in book.BookReviews)
            {
                reviews.Add(new ReviewViewModel()
                {
                    Date = review.Date,
                    Message = review.Message,
                    ThumbDown = review.ThumbDown,
                    ThumbUp = review.ThumbUp,
                    UserName = review.User.UserName
                });
            }

            BookInfoViewModel bookInfo = new BookInfoViewModel()
            {
                AuthorDescription = book.BookAuthor.Description,
                AuthorImagePath = book.BookAuthor.AuthorImage.PathToImageFile,
                AuthorName = book.BookAuthor.AuthorName,
                AuthorNationality = book.BookAuthor.AuthorNationality.NationalityName,
                BookCount = book.Count,
                BookDescription = book.Description,
                BookImagePath = book.BookImage.PathToImageFile,
                BookLanguage = book.Language,
                BookName = book.BookName,
                BookPrice = book.Price,
                BookVolume = book.Volume,
                CategoryName = book.BookCategory.CategoryName,
                DatePublished = book.DatePublished,
                PublisherCity = book.BookPublisher.PublisherCity,
                PublisherName = book.BookPublisher.PublisherName,
                BookReviews = reviews
            };
            
            return bookInfo;
        }

        public List<BookShortInfoViewModel> GetBooks()
        {
            List<BookShortInfoViewModel> result = new List<BookShortInfoViewModel>();

            foreach (var book in bookRepository.GetBooks())
            {
                result.Add(new BookShortInfoViewModel()
                {
                    BookAuthorName = book.BookAuthor.AuthorName,
                    BookDescription = book.Description,
                    BookId = book.BookId,
                    BookName = book.BookName,
                    BookImagePath = "",
                    BookPrice = book.Price
                });
            }

            return result;
        }

        public List<BookShortInfoViewModel> SearchBooks(string filter)
        {
            var filteredBooks = bookRepository.GetBooks().AsQueryable();

            var result = filteredBooks
                .Where(r => r.BookName.Contains("Taras") || r.BookAuthor.AuthorName.Contains("Taras"))
                .Select(b => new BookShortInfoViewModel()
                {
                    BookAuthorName = b.BookAuthor.AuthorName,
                    BookDescription = b.Description,
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BookImagePath = b.BookImage.PathToImageFile,
                    BookPrice = b.Price
                }).ToList();

            return result;
        }
    }
}
