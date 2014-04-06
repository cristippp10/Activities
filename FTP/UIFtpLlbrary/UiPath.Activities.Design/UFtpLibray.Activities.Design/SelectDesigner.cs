using System;
using System.Activities;
using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class SelectDesigner : ActivityDesigner, IComponentConnector
	{
        //internal TargetSelectorControl TargetSelector;
        //internal ComboboxControl ComboboxControl;
        //private bool _contentLoaded;
		public SelectDesigner()
		{
			this.InitializeComponent();
		}
		private void ComboboxControl_SelectionChanged(object sender, RoutedEventArgs e)
		{
			object selectedValue = (e.OriginalSource as ComboBox).SelectedValue;
			if (selectedValue != null)
			{
				base.ModelItem.Properties["Item"].SetValue(new InArgument<string>(selectedValue.ToString()));
				return;
			}
			base.ModelItem.Properties["Item"].SetValue(null);
		}
		private void ComboboxControl_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			this.ComboboxControl.SetCurrentValue(ComboboxControl.ItemsSourceProperty, base.ModelItem.Properties["Items"].ComputedValue as List<string>);
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/control/selectdesigner.xaml", UriKind.Relative);
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
