using System;
using System.Activities;
namespace FtpActivities
{
	public interface IContainerActivity
	{
		Activity Body
		{
			get;
			set;
		}
	}
}
