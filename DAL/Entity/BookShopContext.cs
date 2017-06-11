using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class BookShopContext : DbContext
    {
        public BookShopContext() : base("BookShopDB")
        {
            Database.SetInitializer<BookShopContext>(null);
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
    }
}
