using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Entity;

namespace DAL.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        BookShopContext ctx = new BookShopContext();

        public List<string> GetNameCategories()
        {
            return ctx.Categories.Select(i => i.CategoryName).ToList(); 
        }
    }
}
