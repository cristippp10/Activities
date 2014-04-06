using System;
using FtpActivities.Properties;
namespace FtpActivities
{
	[System.Serializable]
	public class RegistrationException : System.Exception
	{
		public RegistrationException() : base(Resources.RegistrationError)
		{
		}
	}
}
