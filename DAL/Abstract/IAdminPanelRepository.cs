using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IAdminPanelRepository
    {
        List<string> GetTableNamesList();

        DbSet GetDbSetByType<T>() where T : class;

        void ContextSaveChanges();
    }
}
