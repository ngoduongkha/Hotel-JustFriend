using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Hotel_JustFriend.Utility
{
    public class ImageHelper : IValueConverter
    {
        private BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
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

        private byte[] ConvertBitMapImageToByteArray(BitmapImage imageC)
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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    BitmapImage img = this.ConvertByteArrayToBitMapImage(value as byte[]);
                    return img;
                }
                return null;
            }
            catch { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    byte[] imageByteArray = ConvertBitMapImageToByteArray(value as BitmapImage);
                    return imageByteArray;
                }
                return null;
            }
            catch { return null; }
        }
    }
}
