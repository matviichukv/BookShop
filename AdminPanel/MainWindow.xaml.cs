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

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void TableSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdminPanelLogic logic = new AdminPanelLogic();
            var sth = logic.GetDbSetByType(TableSelectionComboBox.SelectedItem.ToString());
            
            TableDataGrid.DataContext = sth.Local;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AdminPanelLogic logic = new AdminPanelLogic();
            logic.SaveChangesInDb();
            TableDataGrid.Items.Refresh();
        }
    }
}
