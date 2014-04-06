using System;
namespace FtpActivities
{
	[System.Serializable]
	public class SelectorException : System.Exception
	{
		public SelectorException()
		{
		}
		public SelectorException(string message) : base(message)
		{
		}
		public SelectorException(string message, System.Exception innerException) : base(message, innerException)
		{
		}
	}
}
