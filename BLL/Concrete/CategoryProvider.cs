using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Models;
using DAL.Concrete;
using DAL.Entity;
using DAL.Abstract;

namespace BLL.Concrete
{
    public class CategoryProvider : ICategoryProvider
    {
        ICategoryRepository category = new CategoryRepository();

        public List<CategoryViewModel> GetNameCategories()
        {
            List<string> nameCategories = category.GetNameCategories();
            var result = nameCategories.Select(i => new CategoryViewModel() { NameCategory = i }).ToList();
            return result;
        }
    }
}
