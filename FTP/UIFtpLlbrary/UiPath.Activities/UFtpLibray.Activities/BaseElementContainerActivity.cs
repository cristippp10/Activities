using System;
using System.Activities;
using System.ComponentModel;
namespace FtpActivities
{
	public abstract class BaseElementContainerActivity : BaseElementActivity, IContainerActivity
	{
		protected bool ScheduleBody;
		[Browsable(false)]
		public Activity Body
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddChild(this.Body);
			base.CacheMetadata(metadata);
		}
		protected override void EndExecute(NativeActivityContext context, System.IAsyncResult result)
		{
			base.EndExecute(context, result);
			if (this.ScheduleBody)
			{
				context.ScheduleActivity(this.Body);
			}
		}
	}
}
