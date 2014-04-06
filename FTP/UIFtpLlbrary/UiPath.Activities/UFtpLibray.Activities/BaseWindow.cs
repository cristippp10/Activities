using System;
using System.Activities;
using System.ComponentModel;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseWindow : BaseSelectorActivity
	{
		[Browsable(false)]
		public InArgument<UiElement> ExistingUiElement
		{
			get;
			set;
		}
		protected override UiElement GetElement()
		{
			Selector selector = new Selector(base.Selector.Get<string>());
			UiElement uiElement = this.ExistingUiElement.Get<UiElement>();
			UiElement uiElement2 = uiElement ?? new UiElement();
			base.AddUiElementProperties(uiElement2);
			if (uiElement == null)
			{
				uiElement2.Selector = selector;
			}
			else
			{
				if (!selector.IsEmpty())
				{
					uiElement2 = uiElement2.FindFirst(FindScope.FIND_DESCENDANTS, selector);
					base.AddUiElementProperties(uiElement2);
				}
			}
			if (this.HighlightElements)
			{
				BaseActivity.HighlightElement(uiElement2);
			}
			return uiElement2;
		}
		protected Window GetWindow()
		{
			return this.GetElement().GetTopLevelWindow();
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("ExistingUiElement", typeof(UiElement), ArgumentDirection.In, false));
			base.CacheMetadata(metadata);
		}
	}
}
