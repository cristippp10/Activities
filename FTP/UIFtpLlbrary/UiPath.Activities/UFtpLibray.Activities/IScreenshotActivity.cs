using System;
using System.Activities;
using UiPath.Library;
namespace FtpActivities
{
	public interface IScreenshotActivity
	{
		InArgument<Image> Image
		{
			get;
			set;
		}
	}
}
