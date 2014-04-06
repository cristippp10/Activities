using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using UiPath.Library;
namespace FtpActivities.Design
{
	public partial class GetValueDesigner : ActivityDesigner, IComponentConnector
	{
		private static string[] attributes = new string[]
		{
			"aaname",
			"aastate",
			"app",
			"AppPath",
			"automationId",
			"checked",
			"cls",
			"cookie",
			"ctrlId",
			"ctrlName",
			"foreground",
			"hasFocus",
			"hwnd",
			"innerHtml",
			"innerText",
			"IsUiPathJavaBridgeEnabled",
			"IsJavaWindow",
			"items",
			"javaState",
			"labeledBy",
			"name",
			"outerHtml",
			"outerText",
			"parentClass",
			"parentId",
			"parentName",
			"PID",
			"position",
			"readyState",
			"role",
			"selectedItem",
			"subsystem",
			"tag",
			"text",
			"TID",
			"title",
			"url",
			"virtualName"
		};
        //internal TargetSelectorControl TargetSelector;
        //internal ComboboxControl ComboboxControl;
        //private bool _contentLoaded;
		public GetValueDesigner()
		{
			this.InitializeComponent();
		}
		private void ComboboxControl_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			try
			{
				UiElement uiElement = new UiElement();
				uiElement.Timeout = 0;
				uiElement.Selector = new Selector(((base.ModelItem.Properties["Selector"].ComputedValue as InArgument<string>).Expression as Literal<string>).Value);
				if (uiElement.Attributes != null && uiElement.Attributes.Count<string>() > 0)
				{
					this.ComboboxControl.SetCurrentValue(ComboboxControl.ItemsSourceProperty, uiElement.Attributes.ToList<string>());
				}
				else
				{
					this.ComboboxControl.SetCurrentValue(ComboboxControl.ItemsSourceProperty, GetValueDesigner.attributes.ToList<string>());
				}
			}
			catch
			{
				this.ComboboxControl.SetCurrentValue(ComboboxControl.ItemsSourceProperty, GetValueDesigner.attributes.ToList<string>());
			}
		}
		private void ComboboxControl_SelectionChanged(object sender, RoutedEventArgs e)
		{
			object selectedValue = (e.OriginalSource as ComboBox).SelectedValue;
			if (selectedValue != null)
			{
				base.ModelItem.Properties["Attribute"].SetValue(new InArgument<string>(selectedValue.ToString()));
				return;
			}
			base.ModelItem.Properties["Attribute"].SetValue(null);
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/control/getvaluedesigner.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //internal Delegate _CreateDelegate(Type delegateType, string handler)
        //{
        //    return Delegate.CreateDelegate(delegateType, this, handler);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.TargetSelector = (TargetSelectorControl)target;
        //        return;
        //    case 2:
        //        this.ComboboxControl = (ComboboxControl)target;
        //        this.ComboboxControl.AddHandler(Binding.TargetUpdatedEvent, new EventHandler<DataTransferEventArgs>(this.ComboboxControl_TargetUpdated));
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
