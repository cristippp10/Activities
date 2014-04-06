using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace FtpActivities.Design
{
	internal class ImageFromBase64Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				try
				{
					string text = value as string;
					if (text != null)
					{
						byte[] buffer = System.Convert.FromBase64String(text);
						MemoryStream bitmapStream = new MemoryStream(buffer);
						BitmapDecoder bitmapDecoder = new PngBitmapDecoder(bitmapStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
						return bitmapDecoder.Frames[0];
					}
				}
				catch (Exception ex)
				{
					//AppLog.Instance.Info(ex.Message);
				}
			}
			return null;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
