using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using System.IO;
using DAL.Entity;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using Dropbox.Api;
using System.Text;

namespace DAL.Concrete
{
    public class ImageRepository : IImageRepository
    {
        string imagesLocation = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "DAL", "Images")).LocalPath;

        public bool SaveImage(Bitmap img, string fileName)
        {
            try
            {
                var fullPath = Path.Combine(imagesLocation, fileName + ".jpg");
                Save(img, 512, 512, 70, fileName + ".jpg");
                DAL.Entity.Image newImage = new Entity.Image { PathToImageFile = fileName + ".jpg" };
                using (var ctx = new BookShopContext())
                {
                    ctx.Images.Add(newImage);
                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
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

        public async void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            //newImage.Save(filePath, imageCodecInfo, encoderParameters);
            //newImage.Save(filePath, ImageFormat.Jpeg);

            //MemoryStream ms = new MemoryStream();
            //
            //newImage.Save(filePath);
            using (var mem = new MemoryStream(GetBytesOfImage(newImage)))
            {
                using (var dropboxClient = new DropboxClient("m54yWAzHPNAAAAAAAAAAFpUT15YHZUSl11Vcmy7qVna4qIoa19ThMUDCgjp6nHQW"))
                {
                    //newImage.Save(mem, ImageFormat.Png);
                    var meta = await dropboxClient.Files.UploadAsync("/Images/" + filePath, Dropbox.Api.Files.WriteMode.Overwrite.Instance, false, DateTime.Now, body: mem);

                }
            }


        }

        private byte[] GetBytesOfImage(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public Bitmap GetImage(string imageName)
        {
            return new Bitmap(Path.Combine(imagesLocation, imageName));
        }

        public async Task<Bitmap> GetImage(int imageId)
        {
            using (var ctx = new BookShopContext())
            {
                string imageFile = ctx.Images.FirstOrDefault(image => image.ImageId == imageId).PathToImageFile;
                using (var dropboxClient = new DropboxClient("m54yWAzHPNAAAAAAAAAAFpUT15YHZUSl11Vcmy7qVna4qIoa19ThMUDCgjp6nHQW"))
                {
                    var result = await dropboxClient.Files.DownloadAsync("/Images/" + imageFile);
                    return new Bitmap(await result.GetContentAsStreamAsync());
                }
            }
            
        }


    }
}
