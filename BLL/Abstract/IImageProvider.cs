using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IImageProvider
    {
        bool SaveImage(string pathToImage);

        Bitmap GetImage(string pathToImage);

        Task<Bitmap> GetImage(int imageId);


    }
}
