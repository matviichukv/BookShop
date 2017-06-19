using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BLL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using DAL.Entity;
using BLL.Models;

namespace BLL.Concrete
{
    public class AdminPanelProvider : IAdminPanelProvider
    {
        private IAdminPanelRepository repo = new AdminPanelRepository(new BookShopContext());
        // Name, tab code, dbset
        public List<Tuple<string, TabItemModel>> GetTablesAndNamesList()
        {
            var service = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-US"));
            List<Tuple<string, TabItemModel>> result = new List<Tuple<string, TabItemModel>>();

            foreach (var tableName in repo.GetTableNamesList())
            {
                Type type = Type.GetType($"DAL.Entity.{service.Singularize(tableName)}, DAL");

                var firstOrDefault = repo.GetType().GetMethods().FirstOrDefault(m => m.Name == "GetDbSetByType");
                if (firstOrDefault != null)
                {
                    var dbSet = firstOrDefault
                        .MakeGenericMethod(type)
                        .Invoke(repo, null) as DbSet;

                    var tabItem = new TabItemModel {Header = tableName, TableDbSet = dbSet};
                    tabItem.TableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                    tabItem.TableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
                    var dataGrid = new DataGrid() {Name = $"{tableName}DataGrid"};
                    dataGrid.SetValue(Grid.ColumnProperty, 1);
                    dataGrid.ItemsSource = dbSet.Local;
                    dataGrid.SelectedIndex = 1;
                    dataGrid.SelectionChanged += TableDataGridOnSelectionChanged;
                    
                    tabItem.TableGrid.Children.Add(dataGrid);

                    var stackPanel = new StackPanel(){Name = $"{tableName}StackPanel"};
                    stackPanel.SetValue(Grid.ColumnProperty, 0);
                    tabItem.TableGrid.Children.Add(stackPanel);
                    foreach (var propertyInfo in type.GetProperties())
                    {

                        if ((propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(string)) && !propertyInfo.Name.Contains("Id"))
                        {
                            tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(new Label() { Content = propertyInfo.Name });
                            if (dataGrid.Items.Count > 1)
                            {
                                var textBox = new TextBox(){ Name = tableName + propertyInfo.Name};
                                textBox.TextChanged += TextBoxOnTextChanged;
                                tabItem.TableGrid.Children.OfType<StackPanel>()
                                    .FirstOrDefault()?
                                    .Children.Add(textBox);
                            }
                        }
                        else if (propertyInfo.Name.Contains("Id") && service.Singularize(tableName) + "Id" != propertyInfo.Name && propertyInfo.PropertyType != typeof(DAL.Entity.Image) && propertyInfo.Name != "ImageId")
                        {
                            Type setType = Type.GetType($"DAL.Entity.{ propertyInfo.Name.Replace("Id", "")}, DAL");
                            var set = (DbSet)repo.GetType()
                                                 .GetMethods()
                                                 .FirstOrDefault(m => m.Name == "GetDbSetByType")
                                                 ?.MakeGenericMethod(setType)
                                                 .Invoke(repo, null);

                            tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(new Label() { Content = propertyInfo.Name });
                            var comboBox = new ComboBox() { Name = tableName + propertyInfo.Name };
                            comboBox.SelectionChanged += ForeignKeyComboBox_SelectionChanged;
                            tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(comboBox);
                            foreach (var item in set)
                            {
                                string itemValue = item.GetType()
                                    .GetProperty(set.ElementType.Name + "Id")
                                    .GetValue(item) + " - " + item.GetType()
                                    .GetProperty(set.ElementType.Name + "Name")
                                    .GetValue(item);
                                comboBox.Items.Add(itemValue);
                            }
                        }
                    }
                    var button = new Button(){Content = "Save changes"};
                    button.Click += SaveChanges_Click;
                    tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(button);
                    result.Add(new Tuple<string, TabItemModel>(tableName, tabItem));
                }

            }
            return result;
        }

        private void ForeignKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid =
                (((sender as ComboBox).Parent as StackPanel).Parent as Grid).Children.OfType<DataGrid>().FirstOrDefault();
            var property = dataGrid.SelectedItem.GetType().GetProperty((sender as ComboBox).Name.Replace(dataGrid.Name.Replace("DataGrid", ""), ""));
            property.SetValue(dataGrid.SelectedItem, (sender as ComboBox).SelectedIndex + 1);
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var dataGrid =
                (((sender as TextBox).Parent as StackPanel).Parent as Grid).Children.OfType<DataGrid>().FirstOrDefault();
            var property = dataGrid.SelectedItem.GetType().GetProperty((sender as TextBox).Name.Replace(dataGrid.Name.Replace("DataGrid", ""), ""));
            if (property.PropertyType == typeof(int))
            {
                property.SetValue(dataGrid.SelectedItem, Int32.Parse( (sender as TextBox).Text) );
            }
            else
            {
                property.SetValue(dataGrid.SelectedItem, (sender as TextBox).Text);
            }
            repo.ContextSaveChanges();
            dataGrid.Items.Refresh();
        }

        private void TableDataGridOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var stackPanel = ((sender as DataGrid).Parent as Grid).Children.OfType<StackPanel>().FirstOrDefault();
            foreach (var textBox in stackPanel.Children.OfType<TextBox>())
            {
                try
                {
                    textBox.Text =
                    (sender as DataGrid).SelectedItem.GetType()
                    .GetProperty(textBox.Name.Replace(stackPanel.Name.Replace("StackPanel", ""), ""))
                    .GetValue((sender as DataGrid).SelectedItem)
                    .ToString();
                }
                catch (NullReferenceException e)
                {
                    //(sender as DataGrid)..Add(Activator.CreateInstance((sender as DataGrid).Items[0].GetType()));
                }
                
            }
            foreach(var comboBox in stackPanel.Children.OfType<ComboBox>())
            {
                comboBox.SelectedIndex =
                (int)(sender as DataGrid).SelectedItem.GetType()
                .GetProperty(comboBox.Name.Replace(stackPanel.Name.Replace("StackPanel", ""), ""))
                .GetValue((sender as DataGrid).SelectedItem) - 1;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            repo.ContextSaveChanges();
        }
    }
}
