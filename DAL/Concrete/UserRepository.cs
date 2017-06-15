using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        BookShopContext ctx = new BookShopContext();

        public User CreateUser(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }
    }
}
