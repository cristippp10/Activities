using System;
using System.Activities;
using System.Activities.Hosting;
using System.Collections.Generic;
namespace FtpActivities
{
	internal class BookmarkResumptionHelper : IWorkflowInstanceExtension
	{
		private WorkflowInstanceProxy instance;
		public void ResumeBookmark(Bookmark bookmark, object value)
		{
			this.instance.EndResumeBookmark(this.instance.BeginResumeBookmark(bookmark, value, null, null));
		}
		System.Collections.Generic.IEnumerable<object> IWorkflowInstanceExtension.GetAdditionalExtensions()
		{
			yield break;
		}
		void IWorkflowInstanceExtension.SetInstance(WorkflowInstanceProxy instance)
		{
			this.instance = instance;
		}
	}
}
