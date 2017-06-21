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

namespace DAL.Concrete
{
    public class ImageRepository : IImageRepository
    {
        string imagesLocation =  new Uri(Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "DAL", "Images")).LocalPath;

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

        public void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
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
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            var test = new Bitmap(newImage);

            //newImage.Save(filePath, imageCodecInfo, encoderParameters);
            //newImage.Save(filePath, ImageFormat.Jpeg);

            MemoryStream ms = new MemoryStream();
            newImage.Save(ms, imageCodecInfo, encoderParameters);
            using (var dropboxClient = new DropboxClient("m54yWAzHPNAAAAAAAAAAFpUT15YHZUSl11Vcmy7qVna4qIoa19ThMUDCgjp6nHQW"))
            {
                dropboxClient.Files.UploadAsync(new Dropbox.Api.Files.CommitInfo(@"/Images/" + filePath, Dropbox.Api.Files.WriteMode.Add.Instance), ms);
            }
        }


        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public Bitmap GetImage(string imageName)
        {
            return new Bitmap(Path.Combine(imagesLocation, imageName));
        }

        public Bitmap GetImage(int imageId)
        {
            using (var ctx = new BookShopContext())
            {
                return new Bitmap( Path.Combine(imagesLocation, ctx.Images.FirstOrDefault(i => i.ImageId == imageId).PathToImageFile));
            }
        }

       
    }
}
