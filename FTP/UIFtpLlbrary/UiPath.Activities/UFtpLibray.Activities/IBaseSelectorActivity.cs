using System;
using System.Activities;
using UiPath.Library;
namespace FtpActivities
{
	public interface IBaseSelectorActivity
	{
		InArgument<string> Selector
		{
			get;
			set;
		}
		InArgument<int> TimeoutMS
		{
			get;
			set;
		}
		WaitForReady WaitForReady
		{
			get;
			set;
		}
	}
}
