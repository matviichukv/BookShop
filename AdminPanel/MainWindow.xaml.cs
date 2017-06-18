using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using DAL.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Data.Entity;
using System.Reflection;
using System.Windows.Markup;
using BLL.Concrete;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopContext ctx = new BookShopContext();
        private readonly Dictionary<string, DbSet> DbSets = new Dictionary<string, DbSet>();

        public MainWindow()
        {
            InitializeComponent();
            AdminPanelProvider p = new AdminPanelProvider();
            var result = p.GetTablesAndNamesList();
            TablesTabControl.DataContext = result.Select(item => item.Item2);
            
        }
    }
}
