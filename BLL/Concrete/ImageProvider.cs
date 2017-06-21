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
        ImageRepository repo = new ImageRepository();

        public Bitmap GetImage(string pathToImage)
        {
            return repo.GetImage(pathToImage);
        }

        public Bitmap GetImage(int imageId)
        {
            return repo.GetImage(imageId);
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
