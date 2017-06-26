using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DAL.Concrete;

namespace BLL.Concrete
{
    public class ImageProvider : IImageProvider
    {
        private string imagesCacheLocation = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Images", "Cache")).LocalPath;
        ImageRepository repo = new ImageRepository();

        public Bitmap GetImage(string pathToImage)
        {
            return repo.GetImage(pathToImage);
        }

        public async Task<Bitmap> GetImage(int imageId)
        {
            if (Directory.GetFiles(imagesCacheLocation, "*.jpg").Contains(Path.Combine(imagesCacheLocation, $"img_{imageId}.jpg")))
            {
                return new Bitmap(Image.FromFile(Path.Combine(imagesCacheLocation, $"img_{imageId}.jpg")));
            }
            else
            {
                var image = await repo.GetImage(imageId);
                image.Save(Path.Combine(imagesCacheLocation, $"img_{imageId}.jpg"));
                return image;
            }
        }

        public bool SaveImage(string pathToImage)
        {
            var image = Image.FromFile(pathToImage);
            Bitmap bmp = new Bitmap(image);
            Guid id = Guid.NewGuid();
            repo.SaveImage(bmp, id.ToString());
            return false;
        }
    }
}
