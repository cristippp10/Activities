using System;
using System.Activities;
using System.ComponentModel;
using FtpActivities.Properties;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseContainerActivity : BaseActivity, IContainerActivity
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
		protected void SetOutputElement(NativeActivityContext context, OutArgument<UiElement> outputElement)
		{
			UiElement uiElement = outputElement.Get<UiElement>();
			if (uiElement != null)
			{
				context.Properties.Add(Resources.ElementPropertyName, uiElement);
			}
		}
	}
}
