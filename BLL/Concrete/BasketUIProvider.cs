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
        public void AddToBasket(int bookId, ObservableCollection<OrderInfoViewModel> booksInBasket)
        {
            BookProvider bookProvider = new BookProvider();
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
            }
            else
            {
                booksInBasket.Add(order);
            }
        }
    }
}
