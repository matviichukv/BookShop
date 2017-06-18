using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Abstract
{
    public interface IAdminPanelProvider
    {
        List<Tuple<string, TabItemModel>> GetTablesAndNamesList();
    }
}
