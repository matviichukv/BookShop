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
                    dataGrid.SelectedIndex = 0;
                    tabItem.TableGrid.Children.Add(dataGrid);

                    var stackPanel = new StackPanel(){Name = $"{tableName}StackPanel"};
                    stackPanel.SetValue(Grid.ColumnProperty, 0);
                    tabItem.TableGrid.Children.Add(stackPanel);
                    foreach (var propertyInfo in type.GetProperties())
                    {

                        if ((propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(string)) && !propertyInfo.Name.Contains("Id"))
                        {
                            tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(new Label() { Content = propertyInfo.Name });
                            Binding binding = new Binding { Mode = BindingMode.TwoWay, ElementName = $"{tableName}DataGrid"};
                            tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(new TextBox() { Name = tableName + "_" + propertyInfo.Name, DataContext = binding});
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
                            tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(new ComboBox() { Name = tableName + "_" + propertyInfo.Name });
                            foreach (var item in set)
                            {
                                string itemValue = item.GetType().GetProperty(set.ElementType.Name + "Id").GetValue(item) + " - " + item.GetType().GetProperty(set.ElementType.Name + "Name").GetValue(item);
                                tabItem.TableGrid
                                    .Children.OfType<ComboBox>()
                                    .FirstOrDefault(c => c.Name == tableName + "_" + propertyInfo.Name)
                                    ?.Items.Add(itemValue);
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

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            repo.ContextSaveChanges();
        }
    }
}
