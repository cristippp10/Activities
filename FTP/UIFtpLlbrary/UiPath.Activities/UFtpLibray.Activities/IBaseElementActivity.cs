using System;
using System.Activities;
using UiPath.Library;
namespace FtpActivities
{
	public interface IBaseElementActivity
	{
		InArgument<UiElement> ExistingUiElement
		{
			get;
			set;
		}
		Region ClippingRegion
		{
			get;
			set;
		}
	}
}
