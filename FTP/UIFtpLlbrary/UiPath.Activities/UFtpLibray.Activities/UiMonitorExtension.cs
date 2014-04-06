using System;
using System.Activities;
using System.Activities.Hosting;
using System.Collections.Generic;
using UiPath.Library;
namespace FtpActivities
{
	internal class UiMonitorExtension : IWorkflowInstanceExtension
	{
		private WorkflowInstanceProxy Instance;
		private Bookmark Bookmark;
		public System.Collections.Generic.IEnumerable<object> GetAdditionalExtensions()
		{
			yield break;
		}
		public void SetInstance(WorkflowInstanceProxy instance)
		{
			this.Instance = instance;
		}
		public void SetBookmark(Bookmark bookmark)
		{
			this.Bookmark = bookmark;
		}
		public void MonitorKey(Monitor monitor, string key, KeyModifier keyModifier)
		{
			monitor.MonitorHotkey(key, keyModifier);
			monitor.Keyboard += new Monitor.KeyboardEventHandler(this.UiEventsProducer_Handler);
		}
		public void MonitorKey(Monitor monitor, string key, KeyModifier keyModifier, EventType eventType, Selector selector, bool includeChildren)
		{
			monitor.MonitorHotkey(key, keyModifier, eventType, selector, includeChildren);
			monitor.Keyboard += new Monitor.KeyboardEventHandler(this.UiEventsProducer_Handler);
		}
		public void MonitorClick(Monitor monitor, MouseButton mouseButton, KeyModifier keyModifier, EventMode eventMode)
		{
			monitor.MonitorClick(mouseButton, keyModifier, eventMode);
			monitor.Mouse += new Monitor.MouseEventHandler(this.UiEventsProducer_Handler);
		}
		public void MonitorClick(Monitor monitor, MouseButton mouseButton, KeyModifier keyModifier, EventType eventType, Selector selector, bool includeChildren, Region clippingRegion)
		{
			monitor.MonitorClick(mouseButton, keyModifier, eventType, selector, includeChildren, clippingRegion);
			monitor.Mouse += new Monitor.MouseEventHandler(this.UiEventsProducer_Handler);
		}
		public void MonitorClickImage(Monitor monitor, MouseButton mouseButton, KeyModifier keyModifier, EventType eventType, Selector selector, Region clippingRegion, Image image, double accuracy)
		{
			monitor.MonitorClickOnImage(mouseButton, keyModifier, eventType, selector, true, clippingRegion, image, accuracy);
			monitor.Mouse += new Monitor.MouseEventHandler(this.UiEventsProducer_Handler);
		}
		private void UiEventsProducer_Handler(EventInfo eventInfo)
		{
			eventInfo.Forward = EventMode.EVENT_BLOCK;
			this.Instance.BeginResumeBookmark(this.Bookmark, eventInfo, null, null);
		}
	}
}
