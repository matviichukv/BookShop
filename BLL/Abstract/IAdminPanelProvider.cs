using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections;

namespace BLL.Abstract
{
    public interface IAdminPanelProvider
    {
        List<Tuple<string, TabItemModel>> GetTablesAndNamesList();

        DbSet GetTableByName(string name);
    }
}
