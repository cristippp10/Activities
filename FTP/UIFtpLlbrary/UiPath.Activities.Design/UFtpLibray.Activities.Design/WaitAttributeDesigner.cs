using System;
using System.Activities;
using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class WaitAttributeDesigner : ActivityDesigner, IComponentConnector
	{
        //internal WaitAttributeDesigner WaitAttribute;
        //internal ComboboxControl ComboboxControl;
        //private bool _contentLoaded;
		public static List<string> Attributes
		{
			get;
			set;
		}
		public WaitAttributeDesigner()
		{
			WaitAttributeDesigner.Attributes = new List<string>
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
				"IsJavaBridgeEnabled",
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
			this.InitializeComponent();
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
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/control/waitattributedesigner.xaml", UriKind.Relative);
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
        //        this.WaitAttribute = (WaitAttributeDesigner)target;
        //        return;
        //    case 2:
        //        this.ComboboxControl = (ComboboxControl)target;
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
