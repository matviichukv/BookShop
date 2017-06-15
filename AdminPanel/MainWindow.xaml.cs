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
            #region dictionary fill
            DbSets.Add("Users", ctx.Users);
            DbSets.Add("Roles", ctx.Roles);
            DbSets.Add("Authors", ctx.Authors);
            DbSets.Add("Books", ctx.Books);
            DbSets.Add("Categories", ctx.Categories);
            DbSets.Add("Images", ctx.Images);
            DbSets.Add("Nationalities", ctx.Nationalities);
            DbSets.Add("Orders", null);
            DbSets.Add("Publishers", ctx.Publishers);
            DbSets.Add("Reviews", ctx.Reviews);
            #endregion
        }

        private void TableSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dbset = DbSets[TableSelectionComboBox.SelectedItem.ToString()];
            dbset.Load();
            TableDataGrid.DataContext = dbset.Local;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            TableDataGrid.Items.Refresh();
        }
    }
}
