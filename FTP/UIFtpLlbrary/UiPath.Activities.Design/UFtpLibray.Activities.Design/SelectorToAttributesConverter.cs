using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows.Data;
using UiPath.Library;
namespace FtpActivities.Design
{
	internal class SelectorToAttributesConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				ModelItem modelItem = value as ModelItem;
				if (modelItem != null)
				{
					InArgument<string> inArgument = modelItem.GetCurrentValue() as InArgument<string>;
					if (inArgument != null)
					{
						Activity<string> expression = inArgument.Expression;
						Literal<string> literal = expression as Literal<string>;
						if (literal != null)
						{
							try
							{
								using (UiElement uiElement = new UiElement())
								{
									uiElement.Timeout = 0;
									uiElement.Selector = new Selector(literal.Value);
									return uiElement.Attributes;
								}
							}
							catch (Exception ex)
							{
								//AppLog.Instance.Info(ex.Message);
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				//AppLog.Instance.Info(ex2.Message);
			}
			return null;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
