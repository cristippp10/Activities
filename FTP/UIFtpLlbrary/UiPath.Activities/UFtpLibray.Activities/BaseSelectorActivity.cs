using System;
using System.Activities;
using System.ComponentModel;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseSelectorActivity : BaseActivity, IBaseSelectorActivity
	{
		private WaitForReady waitForReady = WaitForReady.INTERACTIVE;
		[Category("Target")]
		public InArgument<string> Selector
		{
			get;
			set;
		}
		[Category("Target")]
		public InArgument<int> TimeoutMS
		{
			get;
			set;
		}
		[Category("Common")]
		public WaitForReady WaitForReady
		{
			get
			{
				return this.waitForReady;
			}
			set
			{
				this.waitForReady = value;
			}
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("Selector", typeof(string), ArgumentDirection.In, false));
			metadata.AddArgument(new RuntimeArgument("TimeoutMS", typeof(int), ArgumentDirection.In, true));
			base.CacheMetadata(metadata);
		}
		protected virtual UiElement GetElement()
		{
			UiElement uiElement = new UiElement();
			this.AddUiElementProperties(uiElement);
			Selector selector = new Selector(this.Selector.Get<string>());
			if (selector != null && !selector.IsEmpty())
			{
				uiElement.Selector = new Selector();
			}
			if (this.HighlightElements)
			{
				BaseActivity.HighlightElement(uiElement);
			}
			return uiElement;
		}
		protected void node_Cancel(out bool pbCancel)
		{
			pbCancel = base.IsActivityCanceled;
		}
		protected void AddUiElementProperties(UiElement element)
		{
			element.Cancel += new UiElement.CancelEventHandler(this.node_Cancel);
			element.WaitForReadyLevel = this.WaitForReady;
			element.Timeout = this.TimeoutMS.Get<int>();
		}
	}
}
