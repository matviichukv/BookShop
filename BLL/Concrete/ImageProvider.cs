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
        public bool SaveImage(string pathToImage)
        {
            Bitmap bmp = new Bitmap(Image.FromFile(pathToImage));
            ImageRepository repo = new ImageRepository();
            Guid id = Guid.NewGuid();
            repo.SaveImage(bmp, id.ToString());
            return false;
        }
    }
}
