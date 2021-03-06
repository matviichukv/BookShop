﻿using System;
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
using System.Collections;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;

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
                    if (tableName == "Images")
                    {
                        var tabItem = new TabItemModel { Header = tableName, TableDbSet = dbSet };
                        tabItem.TableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                        tabItem.TableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
                        var dataGrid = new DataGrid() { Name = $"{tableName}DataGrid" };
                        dataGrid.SetValue(Grid.ColumnProperty, 1);
                        dataGrid.ItemsSource = dbSet.Local;
                        dataGrid.SelectedIndex = 1;
                        dataGrid.SelectionChanged += ImagesDataGrid_SelectionChanged;
                        tabItem.TableGrid.Children.Add(dataGrid);

                        var stackPanel = new StackPanel() { Name = $"{tableName}StackPanel" };
                        stackPanel.SetValue(Grid.ColumnProperty, 0);
                        tabItem.TableGrid.Children.Add(stackPanel);
                        Button button1 = new Button();
                        button1.Content = "Upload image";
                        button1.Click += UploadButton_Click;
                        stackPanel.Children.Add(button1);
                        Button button2 = new Button();
                        button2.Content = "Save changes";
                        button2.Click += SaveChanges_Click;
                        stackPanel.Children.Add(button2);
                        System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                        stackPanel.Children.Add(img);
                        result.Add(new Tuple<string, TabItemModel>(tableName, tabItem));
                    }
                    else
                    {
                        var tabItem = new TabItemModel { Header = tableName, TableDbSet = dbSet };
                        tabItem.TableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                        tabItem.TableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
                        var dataGrid = new DataGrid() { Name = $"{tableName}DataGrid" };
                        dataGrid.SetValue(Grid.ColumnProperty, 1);
                        dataGrid.ItemsSource = dbSet.Local;
                        dataGrid.SelectedIndex = 1;
                        dataGrid.SelectionChanged += TableDataGridOnSelectionChanged;
                        dataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
                        dataGrid.AddingNewItem += DataGrid_AddingNewItem;
                        tabItem.TableGrid.Children.Add(dataGrid);

                        var stackPanel = new StackPanel() { Name = $"{tableName}StackPanel" };
                        stackPanel.SetValue(Grid.ColumnProperty, 0);
                        tabItem.TableGrid.Children.Add(stackPanel);
                        foreach (var propertyInfo in type.GetProperties())
                        {

                            if ((propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(string)) && !propertyInfo.Name.Contains("Id"))
                            {
                                tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(new Label() { Content = propertyInfo.Name });
                                if (dataGrid.Items.Count > 1)
                                {
                                    var textBox = new TextBox() { Name = tableName + propertyInfo.Name };
                                    textBox.TextChanged += TextBoxOnTextChanged;
                                    tabItem.TableGrid.Children.OfType<StackPanel>()
                                        .FirstOrDefault()?
                                        .Children.Add(textBox);
                                }
                            }
                            else if (propertyInfo.Name.Contains("Id") && service.Singularize(tableName) + "Id" != propertyInfo.Name)
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
                                    string itemValue = "";
                                    try
                                    {
                                        itemValue = item.GetType()
                                            .GetProperty(set.ElementType.Name + "Id")
                                            .GetValue(item) + " - " + item.GetType()
                                                                      .GetProperty(set.ElementType.Name + "Name")
                                                                      .GetValue(item);
                                    }
                                    catch
                                    {
                                        itemValue = item.GetType()
                                            .GetProperty(set.ElementType.Name + "Id")
                                            .GetValue(item).ToString();
                                    }
                                    finally
                                    {
                                        comboBox.Items.Add(itemValue);
                                    }
                                }
                            }
                        }
                        var button = new Button() { Content = "Save changes" };
                        button.Click += SaveChanges_Click;
                        tabItem.TableGrid.Children.OfType<StackPanel>().FirstOrDefault()?.Children.Add(button);
                        result.Add(new Tuple<string, TabItemModel>(tableName, tabItem));
                    }
                }

            }
            return result;
        }

        private void DataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            (sender as DataGrid).Items.Refresh();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!e.PropertyType.IsPrimitive)
            {
                if (!e.PropertyType.IsInstanceOfType(""))
                    if (!e.PropertyType.IsInstanceOfType(DateTime.Now))
                        if(!e.PropertyType.IsInstanceOfType(new int?(1)))
                            e.Cancel = true;
            }
            
        }

        private void ForeignKeyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid =
                (((sender as ComboBox).Parent as StackPanel).Parent as Grid).Children.OfType<DataGrid>().FirstOrDefault();
            var property = dataGrid.SelectedItem.GetType().GetProperty((sender as ComboBox).Name.Replace(dataGrid.Name.Replace("DataGrid", ""), ""));
            property.SetValue( dataGrid.SelectedItem, Int32.Parse( ( (sender as ComboBox).SelectedItem.ToString().Split(' ')[0] ) ) );
            dataGrid.Items.Refresh();
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var dataGrid =
                (((sender as TextBox).Parent as StackPanel).Parent as Grid).Children.OfType<DataGrid>().FirstOrDefault();
            var property = dataGrid.SelectedItem.GetType().GetProperty((sender as TextBox).Name.Replace(dataGrid.Name.Replace("DataGrid", ""), ""));
            if (property.PropertyType == typeof(int))
            {
                property.SetValue(dataGrid.SelectedItem, Int32.Parse((sender as TextBox).Text));
            }
            else if (property.PropertyType == typeof(decimal))
            {
                property.SetValue(dataGrid.SelectedItem, Decimal.Parse((sender as TextBox).Text));
            }
            else
            {
                property.SetValue(dataGrid.SelectedItem, (sender as TextBox).Text);
            }
            if ((sender as TextBox).Text != "" && (sender as TextBox).Text != "0")
            {
                repo.ContextSaveChanges();
                dataGrid.Items.Refresh();
            }
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

                }
                
            }
            foreach(var comboBox in stackPanel.Children.OfType<ComboBox>())
            {
                try
                {
                    int fkId = (int)(sender as DataGrid).SelectedItem.GetType() //foreign key id
                    .GetProperty(comboBox.Name.Replace(stackPanel.Name.Replace("StackPanel", ""), ""))
                    .GetValue((sender as DataGrid).SelectedItem);
                    foreach (var item in comboBox.Items)
                    {
                        if (item.ToString().Substring(0, 1) == fkId.ToString())
                        {
                            comboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                repo.ContextSaveChanges();
                (((sender as Button).Parent as StackPanel).Parent as Grid).Children.OfType<DataGrid>().FirstOrDefault().Items.Refresh();
            }
            catch
            {

            }
        }

        public DbSet GetTableByName(string name)
        {
            var service = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-US"));
            Type type = Type.GetType($"DAL.Entity.{service.Singularize(name)}, DAL");

            var dbset = repo
                          .GetType()
                          .GetMethods()
                          .FirstOrDefault(m => m.Name == "GetDbSetByType")
                          .MakeGenericMethod(type)
                          .Invoke(repo, null) as DbSet;
            return dbset;
        }

        private async void ImagesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ImageProvider provider = new ImageProvider();
            //var image = await provider.GetImage(((sender as DataGrid).SelectedItem as DAL.Entity.Image).ImageId);
            //((sender as DataGrid).Parent as Grid).Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<System.Windows.Controls.Image>().FirstOrDefault().Source = BitmapToImageSource(image);
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog().Value)
            {
                ImageProvider provider = new ImageProvider();
                provider.SaveImage(dialog.FileName);
                (((sender as Button).Parent as StackPanel).Parent as Grid).Children.OfType<DataGrid>().FirstOrDefault().Items.Refresh();
            }
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
