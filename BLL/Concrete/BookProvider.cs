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
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

namespace BLL.Concrete
{
    public class BookProvider : IBookProvider
    {
        IBookRepository bookRepository = new BookRepository();

        public async Task<BookInfoViewModel> GetBookInfo(int bookId)
        {
            Book book = bookRepository.GetBookById(bookId);
            ObservableCollection<ReviewViewModel> reviews = new ObservableCollection<ReviewViewModel>();

            foreach (var review in book.BookReviews)
            {
                reviews.Add(new ReviewViewModel()
                {
                    Date = review.Date,
                    Message = review.Message,
                    Likes = review.ReviewLikes.Count,
                    UserName = review.User.UserName
                });
            }
            ImageProvider provider = new ImageProvider();
            BookInfoViewModel bookInfo = new BookInfoViewModel()
            {
                AuthorDescription = book.BookAuthor.Description,
                AuthorImage = book.BookAuthor.AuthorImage == null ? null : BitmapToImageSource(await provider.GetImage((int)book.BookAuthor.ImageId)),
                AuthorName = book.BookAuthor.AuthorName,
                AuthorNationality = book.BookAuthor.AuthorNationality == null ? null : book.BookAuthor.AuthorNationality.NationalityName,
                BookCount = book.Count,
                BookDescription = book.Description,
                BookImageSource = book.BookImage == null ? null: BitmapToImageSource(await provider.GetImage(book.BookImage.ImageId)),
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

        public async Task<List<BookShortInfoViewModel>> GetBooks()
        {
            ImageProvider provider = new ImageProvider();
            List<BookShortInfoViewModel> shortInfoBooks = new List<BookShortInfoViewModel>();
            var books = bookRepository.GetBooks();

            foreach (var item in books)
            {
                shortInfoBooks.Add(new BookShortInfoViewModel
                {
                    BookAuthorName = item.BookAuthor.AuthorName,
                    BookDescription = item.Description,
                    BookId = item.BookId,
                    BookName = item.BookName,
                    BookImage = item.BookImage == null ? null : BitmapToImageSource(await provider.GetImage(item.BookImage.ImageId)),
                    BookPrice = item.Price
                });
            }

            return shortInfoBooks;
        }

        public List<BookShortInfoViewModel> SearchBooks(string filter)
        {
            return bookRepository.GetBooks()
                .Where(r => r.BookName.Contains(filter) || r.BookAuthor.AuthorName.Contains(filter) || r.Description.Contains(filter))
                .Select(b => new BookShortInfoViewModel()
                {
                    BookAuthorName = b.BookAuthor.AuthorName,
                    BookDescription = b.Description,
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BookImage = null,//b.BookImage == null ? 0 : b.BookImage.ImageId,
                    BookPrice = b.Price
                }).ToList();
        }

        public List<BookShortInfoViewModel> GetBooksByCategoty(string category)
        {
            return bookRepository.GetBooks()
                .Where(r => r.BookCategory.CategoryName == category)
                .Select(b => new BookShortInfoViewModel()
                {
                    BookAuthorName = b.BookAuthor.AuthorName,
                    BookDescription = b.Description,
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BookImage = null,//b.BookImage == null ? 0 : b.BookImage.ImageId,
                    BookPrice = b.Price
                }).ToList();
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
