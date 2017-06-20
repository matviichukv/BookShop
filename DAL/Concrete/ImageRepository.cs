using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using System.IO;
using DAL.Entity;
using System.Drawing.Imaging;

namespace DAL.Concrete
{
    public class ImageRepository : IImageRepository
    {
        string imagesLocation =  new Uri(Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "DAL", "Images")).LocalPath;

        public bool SaveImage(Bitmap img, string fileName)
        {
            var fullPath = Path.Combine(imagesLocation, fileName + ".jpg");
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            img.Save(fullPath, jpgEncoder, myEncoderParameters);
            return false;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
