using System;
using System.Activities.Presentation.Model;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
namespace FtpActivities.Design
{
	public class ActivityDescriptionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string text = string.Empty;
			object result;
			try
			{
				ModelItem modelItem = value as ModelItem;
				Attribute attribute = modelItem.Attributes[typeof(DescriptionAttribute)];
				if (attribute != null)
				{
					text = (attribute as DescriptionAttribute).Description;
					if (text.Contains("API reference"))
					{
						text = text.Remove(text.IndexOf("API reference"));
					}
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					text = ((value as ModelItem).Properties["DisplayName"].ComputedValue as string);
				}
				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
