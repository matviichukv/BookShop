using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;

namespace BLL
{
    public static class AdminPanelLogic
    {
        public static IList GetDbSetByType(string type)
        {
            using (var service = new ServiceReference.BookShopServiceClient())
            {
                var dbset = (DbSet)JsonConvert.DeserializeObject( service.GetDbSetByType(type));
                dbset.Load();
                return dbset.Local;
            }
        }
    }
}
