using System;
namespace FtpActivities
{
	[System.Serializable]
	public class CheckpointException : System.Exception
	{
		public CheckpointException()
		{
		}
		public CheckpointException(string message) : base(message)
		{
		}
	}
}
