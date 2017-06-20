using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Concrete
{
    public class BasketUIProvider : IBasketUIProvider
    {
        public void AddToBasket(int bookId, ObservableCollection<OrderInfoViewModel> booksInBasket, string userEmail)
        {
            IOrderProvider orderProvider = new OrderProvider();
            IBookProvider bookProvider = new BookProvider();
            BookInfoViewModel bookVM = bookProvider.GetBookInfo(bookId);

            OrderInfoViewModel order = new OrderInfoViewModel
            {
                BookId = bookId,
                AuthorName = bookVM.AuthorName,
                BookImagePath = bookVM.BookImagePath,
                BookName = bookVM.BookName,
                Count = 1,
                Price = bookVM.BookPrice,
                Cost = bookVM.BookPrice
            };

            var book = booksInBasket.SingleOrDefault(i => i.BookId == bookId);

            if (book != null)
            {
                int index = booksInBasket.IndexOf(book);
                booksInBasket[index].Count++;
                booksInBasket[index].Cost = booksInBasket[index].Price * booksInBasket[index].Count;
            }
            else
            {
                booksInBasket.Add(order);
                orderProvider.AddOrder(new OrderAddViewModel() { BookId = bookId, Count = bookVM.BookCount, Price = bookVM.BookPrice }, userEmail);
            }
        }
    }
}
