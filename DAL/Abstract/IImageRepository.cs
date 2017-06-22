using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DAL.Abstract
{
    public interface IImageRepository
    {
        bool SaveImage(Bitmap img, string fileName);

        Bitmap GetImage(string pathToImage);

        Task<Bitmap> GetImage(int imageId);
    }
}

