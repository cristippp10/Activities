using System;
using FtpActivities.Properties;
namespace FtpActivities
{
	[System.Serializable]
	public class ActivityTimeoutException : System.Exception
	{
		public ActivityTimeoutException() : base(Resources.ActivityTimeout)
		{
		}
	}
}
