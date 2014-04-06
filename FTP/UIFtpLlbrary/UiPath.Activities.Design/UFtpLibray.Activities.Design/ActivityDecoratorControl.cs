using System;
using System.Windows;
using System.Windows.Controls;
namespace FtpActivities.Design
{
	public class ActivityDecoratorControl : ContentControl
	{
		static ActivityDecoratorControl()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ActivityDecoratorControl), new FrameworkPropertyMetadata(typeof(ActivityDecoratorControl)));
		}
	}
}
