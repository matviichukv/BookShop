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

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopContext ctx = new BookShopContext();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void TableSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PluralizationService service = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-US"));
            var dbset = ctx.Set(typeof(Book).Assembly.GetTypes().FirstOrDefault(t => t.Name == service.Singularize(TableSelectionComboBox.SelectedItem.ToString() )));
            TableDataGrid.DataContext = dbset.Local;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            TableDataGrid.Items.Refresh();
        }
    }
}
