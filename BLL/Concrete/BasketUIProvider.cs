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
        IOrderProvider orderProvider = new OrderProvider();

        public void AddToBasket(BookShortInfoViewModel bookInfo, ObservableCollection<OrderInfoViewModel> booksInBasket, string userEmail)
        {
            var book = booksInBasket.SingleOrDefault(i => i.BookId == bookInfo.BookId);

            if (book != null)
            {
                int index = booksInBasket.IndexOf(book);
                booksInBasket[index].Count++;
                booksInBasket[index].Cost = booksInBasket[index].Price * booksInBasket[index].Count;
            }
            else
            {
                OrderInfoViewModel order = new OrderInfoViewModel
                {
                    BookId = bookInfo.BookId,
                    AuthorName = bookInfo.BookAuthorName,
                    BookImagePath = bookInfo.BookImagePath,
                    BookName = bookInfo.BookName,
                    Count = 1,
                    Price = bookInfo.BookPrice,
                    Cost = bookInfo.BookPrice
                };

                booksInBasket.Add(order);
                booksInBasket[booksInBasket.Count - 1].OrderId = orderProvider.AddOrder(new OrderAddViewModel() { BookId = bookInfo.BookId, Count = 1, Price = bookInfo.BookPrice }, userEmail);
            }
        }

        public void FillBasket(ObservableCollection<OrderInfoViewModel> booksInBasket, int userId)
        {
            var books = orderProvider.GetBasket(userId);

            foreach(var item in books)
            {
                booksInBasket.Add(item);
            }
        }
    }
}
