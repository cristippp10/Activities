using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using FtpActivities.Properties;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseSelectorEventsActivity : BaseEventsActivity
	{
		private InArgument<string> selector;
		[Category("Target")]
		public InArgument<string> Selector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
				if (value == null)
				{
					this.ParentImageBase64 = null;
					this.InformativeScreenshot = null;
				}
			}
		}
		[Browsable(false), Category("Target"), System.Obsolete]
		public InArgument<UiElement> ExistingUiElement
		{
			get;
			set;
		}
		[Category("Target")]
		public UiPath.Library.Region ClippingRegion
		{
			get;
			set;
		}
		[Browsable(false), System.Obsolete]
		public string ImageBase64
		{
			get;
			set;
		}
		[Browsable(false)]
		public string ParentImageBase64
		{
			get;
			set;
		}
		[Browsable(false)]
		public string InformativeScreenshot
		{
			get;
			set;
		}
		[Browsable(false)]
		public UiPath.Library.Region ElementPosition
		{
			get;
			set;
		}
		[Browsable(false)]
		public string DesignerSelector
		{
			get;
			set;
		}
		[Browsable(false), System.Obsolete]
		public bool ShowScreenshot
		{
			get;
			set;
		}
		[Browsable(false), System.Obsolete]
		public InArgument<int> TimeoutMS
		{
			get;
			set;
		}
		[Browsable(false), System.Obsolete]
		public WaitForReady WaitForReady
		{
			get;
			set;
		}
		[Category("Event")]
		public EventType EventType
		{
			get;
			set;
		}
		[Category("Event"), DefaultValue(false)]
		public InArgument<bool> BlockEvent
		{
			get;
			set;
		}
		protected BaseSelectorEventsActivity()
		{
			this.BlockEvent = new InArgument<bool>(false);
			base.RepeatForever = true;
		}
		protected override void Execute(NativeActivityContext context)
		{
			try
			{
				base.CreateBookmark(context);
				Selector selector = new Selector(this.Selector.Get(context));
				System.Collections.Generic.Dictionary<string, object> extension = context.GetExtension<System.Collections.Generic.Dictionary<string, object>>();
				if (extension != null && extension.ContainsKey("HighlightElements") && (bool)extension["HighlightElements"])
				{
					try
					{
						using (UiElement uiElement = new UiElement(selector, 0))
						{
							uiElement.ClippingRegion = this.ClippingRegion;
							uiElement.StartHighlight(Color.Red);
							System.Threading.Thread.Sleep(int.Parse(Resources.HighlightSleepTime));
							uiElement.StopHighlight();
						}
					}
					catch
					{
					}
				}
				this.StartMonitor(context, selector);
			}
			catch (System.Exception ex)
			{
				base.DisposeMonitor();
				base.HandleException(ex, base.ContinueOnError.Get(context));
			}
		}
		protected override void BodyCompleted(NativeActivityContext context, ActivityInstance completedInstance)
		{
			this.TryReplayEvent(context);
			base.BodyCompleted(context, completedInstance);
		}
		protected void TryReplayEvent(NativeActivityContext context)
		{
			try
			{
				System.Collections.Generic.KeyValuePair<Activity, object> keyValuePair = this.EventQueue.Peek();
				EventMode eventMode = (this.BlockEvent.Get(context) && this.EventType == EventType.EVENT_SYNCHRONOUS) ? EventMode.EVENT_BLOCK : EventMode.EVENT_FORWARD;
				EventInfo eventInfo = keyValuePair.Value as EventInfo;
				if (this.EventType == EventType.EVENT_SYNCHRONOUS)
				{
					eventInfo.Forward = EventMode.EVENT_BLOCK;
					if (eventMode == EventMode.EVENT_BLOCK)
					{
						eventInfo.ReplayEvent = false;
					}
					else
					{
						eventInfo.ReplayEvent = true;
					}
				}
				else
				{
					eventInfo.Forward = eventMode;
				}
				if (eventInfo.ReplayEvent)
				{
					eventInfo.Replay();
				}
			}
			catch (System.Exception ex)
			{
				AppLog.Instance.Info(ex.Message);
			}
		}
		protected abstract void StartMonitor(NativeActivityContext context, Selector selector);
	}
}
