using System;
using System.Activities;
using System.ComponentModel;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseElementActivity : BaseSelectorActivity, IBaseElementActivity
	{
		[Browsable(false), Category("Target")]
		public InArgument<UiElement> ExistingUiElement
		{
			get;
			set;
		}
		[Category("Target")]
		public Region ClippingRegion
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
			if (uiElement == null && selector != null)
			{
				uiElement2.Selector = selector;
			}
			else
			{
				if (selector != null && !selector.IsEmpty())
				{
					if (selector.IsTopLevel())
					{
						uiElement2 = uiElement2.FindFirst(FindScope.FIND_PROCESS, selector);
					}
					else
					{
						uiElement2 = uiElement2.FindFirst(FindScope.FIND_DESCENDANTS, selector);
					}
					base.AddUiElementProperties(uiElement2);
				}
			}
			if (this.ClippingRegion != null && this.ClippingRegion.Rectangle.HasValue)
			{
				uiElement2.ClippingRegion = this.ClippingRegion;
			}
			if (this.HighlightElements)
			{
				BaseActivity.HighlightElement(uiElement2);
			}
			return uiElement2;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("ExistingUiElement", typeof(UiElement), ArgumentDirection.In, false));
			base.CacheMetadata(metadata);
		}
	}
}
