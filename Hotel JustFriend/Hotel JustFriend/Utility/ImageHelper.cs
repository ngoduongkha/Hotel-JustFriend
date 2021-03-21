using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Hotel_JustFriend.Utility
{
    public class ImageHelper
    {
        private static ImageHelper _Instance;

        public static ImageHelper Instance { 
            get
            {
                if (_Instance == null)
                    _Instance = new ImageHelper();
                return _Instance;
            }
        }

        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            try
            {
                BitmapImage img = new BitmapImage();
                using (MemoryStream memStream = new MemoryStream(imageByteArray))
                {
                    memStream.Position = 0;
                    img.BeginInit();
                    img.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.UriSource = null;
                    img.StreamSource = memStream;
                    img.EndInit();
                }
                return img;
            }
            catch { return null; }
        }

        public byte[] ConvertBitMapImageToByteArray(BitmapImage imageC)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageC));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
            catch { return null; }
        }
    }
}
