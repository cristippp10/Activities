using System;
using System.Activities;
using System.ComponentModel;
using System.Threading;
using FtpActivities.Properties;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseType : BaseElementActivity
	{
		[Category("Input")]
		public InputMethod TypeMethod
		{
			get;
			set;
		}
		[Category("Common")]
		public InArgument<int> DelayMS
		{
			get;
			set;
		}
		[Category("Options")]
		public InArgument<int> DelayBetweenKeys
		{
			get;
			set;
		}
		[Category("Options")]
		public bool ClickBeforeTyping
		{
			get;
			set;
		}
		[Category("Options")]
		public bool EmptyField
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("DelayMS", typeof(int), ArgumentDirection.In, false));
			metadata.AddArgument(new RuntimeArgument("TypeMethod", typeof(InputMethod), ArgumentDirection.In, false));
			metadata.AddArgument(new RuntimeArgument("DelayBetweenKeys", typeof(int), ArgumentDirection.In, false));
			base.CacheMetadata(metadata);
		}
		protected void TypeText(string text)
		{
			try
			{
				UiElement element = this.GetElement();
				if (this.ClickBeforeTyping)
				{
					element.Click(ClickType.CLICK_SINGLE, MouseButton.BTN_LEFT, this.TypeMethod);
					System.Threading.Thread.Sleep(100);
				}
				if (this.EmptyField && this.TypeMethod != InputMethod.API)
				{
					element.WriteText(Resources.EmptyFieldSequence, this.TypeMethod);
				}
				int num = 0;
				if (this.DelayBetweenKeys != null)
				{
					num = this.DelayBetweenKeys.Get<int>();
				}
				if (num > 0)
				{
					element.WriteText(text, this.TypeMethod, num);
				}
				else
				{
					element.WriteText(text, this.TypeMethod);
				}
				System.Threading.Thread.Sleep(this.DelayMS.Get<int>());
			}
			catch (System.Exception ex)
			{
				base.HandleException(ex);
			}
		}
	}
}
