using System;
using System.Activities.Presentation.Model;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
namespace FtpActivities.Design
{
	public class InArgumentToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			try
			{
				TypeDescriptor.GetConverter(typeof(bool));
				if (value == null)
				{
					object result = true;
					return result;
				}
				ModelItem modelItem = value as ModelItem;
				if (modelItem == null)
				{
					object result = true;
					return result;
				}
				flag = (modelItem.Properties["Expression"].ComputedValue == null);
			}
			catch (Exception ex)
			{
				//AppLog.Instance.Info(ex.Message);
			}
			return flag;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
