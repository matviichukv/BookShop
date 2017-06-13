using DAL.Entity;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;

namespace WcfBookShopService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BookShopService : IBookShopService
    {
        BookShopContext ctx = new BookShopContext();

        public void BuyBook(int bookId, int count, int orderId, int userId)
        {
            Order newOrder = new Order() {  BookCount = count,
                                            OrderNumber = orderId,
                                            OrderPrice = count * ctx.Books.FirstOrDefault(book => book.BookId == bookId).Price,
                                            UserId = userId
            };
        }

        public bool CheckUserCredentials(string email, string password)
        {
            try
            {
                return (BCrypt.Net.BCrypt.Verify(ctx.Users.FirstOrDefault(user => user.UserEmail == email).UserPassword, password));
            }
            catch
            {
                return false;
            }
        }

        public IList<string> GetBooks()
        {
            List<string> jsonBooks = new List<string>();

            foreach (var book in ctx.Books)
            {
                jsonBooks.Add(JsonConvert.SerializeObject(book));
            }

            return jsonBooks;
        }

        public IList<string> GetBooksByCategory(string category)
        {
            List<string> jsonBooks = new List<string>();

            foreach (var book in ctx.Books.Where(t => t.BookCategory.CategoryName == category))
            {
                jsonBooks.Add(JsonConvert.SerializeObject(book));
            }

            return jsonBooks;
        }

        public IList<string> GetCategories()
        {
            List<string> jsonCategories = new List<string>();

            foreach (var category in ctx.Categories)
            {
                jsonCategories.Add(JsonConvert.SerializeObject(category));
            }

            return jsonCategories;
        }

        public string GetUserInfo(int userId)
        {
            return JsonConvert.SerializeObject(ctx.Users.FirstOrDefault(t => t.UserId == userId));
        }

        public void RegisterUser(string name, string email, string password)
        {
            User newUser = new User() { UserEmail = email,
                                        UserName = name,
                                        UserPassword = BCrypt.Net.BCrypt.HashPassword(password, "abc")
            };

            newUser.Roles.Add(ctx.Roles.FirstOrDefault(role => role.RoleName == "User"));
            ctx.Users.Add(newUser);
            ctx.SaveChanges();
        }

        public string GetDbSetByType(string type)
        {
            return JsonConvert.SerializeObject(ctx.Set(Type.GetType(type)));
        }
    }
}
