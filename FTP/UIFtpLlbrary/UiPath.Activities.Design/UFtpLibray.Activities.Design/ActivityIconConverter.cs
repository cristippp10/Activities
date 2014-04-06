using System;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
namespace FtpActivities.Design
{
	internal class ActivityIconConverter : IValueConverter
	{
		private static ResourceDictionary IconResources = new ResourceDictionary
		{
            Source = new Uri("pack://application:,,,/UFtpLibray.Activities.Design;component/themes/icons.xaml")
		};
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				if (value == null)
				{
					result = null;
				}
				else
				{
					Type itemType = (value as ModelItem).ItemType;
					string text = itemType.Name;
					if (itemType.IsGenericType)
					{
						text = text.Split(new char[]
						{
							'`'
						})[0];
					}
					text += "Icon";
					DrawingBrush drawingBrush = ActivityIconConverter.IconResources[text] as DrawingBrush;
					if (drawingBrush == null)
					{
						drawingBrush = (Application.Current.Resources[text] as DrawingBrush);
					}
					if (drawingBrush == null)
					{
						drawingBrush = (Application.Current.Resources["GenericLeafActivityIcon"] as DrawingBrush);
					}
					result = drawingBrush.Drawing;
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
