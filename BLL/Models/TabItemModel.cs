using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BLL.Models
{
    public class TabItemModel
    {
        public string Header { get; set; }

        public Grid TableGrid { get; set; } = new Grid();

        public DbSet TableDbSet { get; set; }
    }
}
