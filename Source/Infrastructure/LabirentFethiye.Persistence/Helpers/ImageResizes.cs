using System.Drawing.Drawing2D;
using SD = System.Drawing;

namespace VillaProject.Persistence.Helpers
{
    public static class ImageResizes
    {
        public static string ResimYukseklik(string MevcutResimYolu, string YeniResimYolu, int BuyukResimGenislik, int BuyukResimYukseklik, string ResimAdiBastanEkle, string ResimAdi)
        {
            SD.Image imgPhotoVert = SD.Image.FromFile(MevcutResimYolu + ResimAdi);
            SD.Image imgPhoto = null;
            imgPhoto = ResimBoyutlandirYukseklik(imgPhotoVert, BuyukResimYukseklik, BuyukResimGenislik);
            imgPhotoVert.Dispose();
            imgPhoto.Save(YeniResimYolu + ResimAdiBastanEkle + ResimAdi, SD.Imaging.ImageFormat.Jpeg);
            imgPhoto.Dispose();
            return ResimAdi;
        }
        static SD.Image ResimBoyutlandirYukseklik(SD.Image imgPhoto, int yukseklik, int MinGenislik)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            int destHeight = yukseklik;
            int destWidth = sourceWidth * yukseklik / imgPhoto.Height; //Resmin bozulmaması için en boy ayarını veriyoruz.

            while (destWidth < MinGenislik)
            {
                if (MinGenislik - destWidth > 100) { destHeight = destHeight + 100; }
                else if (MinGenislik - destWidth > 50) { destHeight = destHeight + 50; }
                else if (MinGenislik - destWidth > 10) { destHeight = destHeight + 10; }
                else { destHeight = destHeight + 1; }
                destWidth = sourceWidth * destHeight / imgPhoto.Height;
            }

            SD.Bitmap bmPhoto = new SD.Bitmap(destWidth, destHeight);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            SD.Graphics grPhoto = SD.Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic; // Resmin kalitesini ayarlıyoruz.
            grPhoto.FillRectangle(SD.Brushes.Transparent, 0, 0, destWidth, destHeight);

            grPhoto.DrawImage(imgPhoto, new SD.Rectangle(0, 0, destWidth, destHeight), new SD.Rectangle(0, 0, sourceWidth, sourceHeight), SD.GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
