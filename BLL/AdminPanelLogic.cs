using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DAL.Entity;

namespace BLL
{
    public class AdminPanelLogic
    {
        BookShopContext ctx = new BookShopContext();
        public void SaveChangesInDb()
        {
            ctx.SaveChanges();
        }

        public DbSet GetDbSetByType(string type)
        {
            var pluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-US"));
            var dbSet = ctx.Set(typeof(Book).Assembly.GetTypes().FirstOrDefault(t => t.Name == pluralizationService.Singularize(type)));
            return dbSet;

        }
    }
}
