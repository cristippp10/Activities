using System;
using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class UiContainerDesigner : ActivityDesigner, IComponentConnector
    {
        //internal TargetSelectorControl TargetSetter;
        //private bool _contentLoaded;
        public UiContainerDesigner()
        {
            this.InitializeComponent();
        }
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/uicontainerdesigner.xaml", UriKind.Relative);
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
        //    if (connectionId == 1)
        //    {
        //        this.TargetSetter = (TargetSelectorControl)target;
        //        return;
        //    }
        //    this._contentLoaded = true;
        //}
	}
}
