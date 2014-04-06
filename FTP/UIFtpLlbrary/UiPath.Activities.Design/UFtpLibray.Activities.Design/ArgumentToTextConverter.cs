using System;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows.Data;
namespace FtpActivities.Design
{
	internal class ArgumentToTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				ModelItem modelItem = value as ModelItem;
				if (value == null)
				{
					result = null;
				}
				else
				{
					InArgument<string> inArgument;
					if (modelItem != null)
					{
						inArgument = (modelItem.GetCurrentValue() as InArgument<string>);
					}
					else
					{
						inArgument = (value as InArgument<string>);
					}
					if (inArgument != null)
					{
						string text;
						if (inArgument.Expression != null)
						{
							text = inArgument.Expression.ToString();
							if (string.IsNullOrEmpty(text))
							{
								text = null;
							}
						}
						else
						{
							text = null;
						}
						result = text;
					}
					else
					{
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				//AppLog.Instance.Info(ex.Message);
				result = null;
			}
			return result;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}
			string text = value.ToString();
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			return new InArgument<string>(text);
		}
	}
}
