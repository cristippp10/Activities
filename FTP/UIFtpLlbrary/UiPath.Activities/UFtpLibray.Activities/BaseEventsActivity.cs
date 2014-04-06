using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel;
using FtpActivities.Utilities;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseEventsActivity : NativeActivity, IContainerActivity
	{
		protected Monitor UiEventsProducer;
		protected Queue<System.Collections.Generic.KeyValuePair<Activity, object>> EventQueue = new Queue<System.Collections.Generic.KeyValuePair<Activity, object>>();
		protected override bool CanInduceIdle
		{
			get
			{
				return true;
			}
		}
		[Category("Key Modifier")]
		public bool Alt
		{
			get;
			set;
		}
		[Category("Key Modifier")]
		public bool Ctrl
		{
			get;
			set;
		}
		[Category("Key Modifier")]
		public bool Shift
		{
			get;
			set;
		}
		[Category("Key Modifier")]
		public bool Win
		{
			get;
			set;
		}
		[Category("Event")]
		public Activity<bool> RepeatForever
		{
			get;
			set;
		}
		[Category("Common")]
		public InArgument<bool> ContinueOnError
		{
			get;
			set;
		}
		[Browsable(false)]
		public Activity Body
		{
			get;
			set;
		}
		public BaseEventsActivity()
		{
			this.Body = new Sequence
			{
				DisplayName = "Do"
			};
			this.RepeatForever = true;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddChild(this.RepeatForever);
			metadata.AddChild(this.Body);
			metadata.RequireExtension<UiMonitorExtension>();
			metadata.AddDefaultExtensionProvider<UiMonitorExtension>(() => new UiMonitorExtension());
			base.CacheMetadata(metadata);
		}
		protected void CreateBookmark(NativeActivityContext context)
		{
			this.CheckPermissions();
			this.EventQueue.Clear();
			this.UiEventsProducer = new Monitor();
			Bookmark bookmark = context.CreateBookmark("UiMonitorBookmark", new BookmarkCallback(this.OnMonitorTrigger), BookmarkOptions.MultipleResume);
			context.GetExtension<UiMonitorExtension>().SetBookmark(bookmark);
		}
		protected override void Cancel(NativeActivityContext context)
		{
			this.DisposeMonitor();
			base.Cancel(context);
		}
		private void OnMonitorTrigger(NativeActivityContext context, Bookmark bookmark, object value)
		{
			this.EventQueue.Enqueue(new System.Collections.Generic.KeyValuePair<Activity, object>(this.Body, value));
			if (this.EventQueue.Count == 1)
			{
				this.ExecuteEventHandler(context);
			}
		}
		protected abstract void ExecuteEventHandler(NativeActivityContext context);
		protected virtual void BodyCompleted(NativeActivityContext context, ActivityInstance completedInstance)
		{
			context.ScheduleActivity<bool>(this.RepeatForever, new CompletionCallback<bool>(this.RepeatForeverCompleted), null);
		}
		protected void RepeatForeverCompleted(NativeActivityContext context, ActivityInstance completedInstance, bool result)
		{
			if (!result)
			{
				this.DisposeMonitor();
				context.RemoveAllBookmarks();
				return;
			}
			this.EventQueue.Dequeue();
			if (this.EventQueue.Count > 0)
			{
				this.ExecuteEventHandler(context);
			}
		}
		protected void BodyFaulted(NativeActivityFaultContext faultContext, System.Exception propagatedException, ActivityInstance propagatedFrom)
		{
			this.DisposeMonitor();
			faultContext.RemoveAllBookmarks();
			this.HandleException(propagatedException, this.ContinueOnError.Get(faultContext));
		}
		protected void DisposeMonitor()
		{
			if (this.UiEventsProducer != null)
			{
				this.UiEventsProducer.StopMonitoring();
				this.UiEventsProducer.Dispose();
				this.UiEventsProducer = null;
			}
		}
		protected void HandleException(System.Exception ex, bool continueOnError)
		{
			if (continueOnError)
			{
				return;
			}
			throw new WorkflowApplicationException(string.Format("{0}: {1}", base.DisplayName, ex.Message), ex);
		}
		protected KeyModifier ComposeKeyModifier()
		{
			int num = 0;
			if (this.Alt)
			{
				num++;
			}
			if (this.Ctrl)
			{
				num += 2;
			}
			if (this.Shift)
			{
				num += 4;
			}
			if (this.Win)
			{
				num += 8;
			}
			return (KeyModifier)num;
		}
	}
}
