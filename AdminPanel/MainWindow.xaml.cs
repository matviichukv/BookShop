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
using Microsoft.Win32;
using System.Drawing;
using System.IO;

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

        //private void UploadButton_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    if (dialog.ShowDialog().Value)
        //    {
        //        ImageProvider provider = new ImageProvider();
        //        provider.SaveImage(dialog.FileName);
        //        ImagesDataGrid.Items.Refresh();
        //    }
        //}

        //private async void ImagesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ImageProvider provider = new ImageProvider();
        //    var image = await provider.GetImage((ImagesDataGrid.SelectedItem as DAL.Entity.Image).ImageId);
        //    ImagePreview.Source = BitmapToImageSource(image);
        //}

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
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
