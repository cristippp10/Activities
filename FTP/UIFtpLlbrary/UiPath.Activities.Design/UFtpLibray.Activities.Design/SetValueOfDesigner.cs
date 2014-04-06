using System;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class SetValueOfDesigner : ActivityDesigner, IComponentConnector
	{
        //internal TargetSelectorControl TargetSelector;
        //internal LinkPropertyControl TextSetter;
        //internal ExpressionTextBox TextInputBox;
        //private bool _contentLoaded;
		public SetValueOfDesigner()
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
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/text/setvalueofdesigner.xaml", UriKind.Relative);
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
        //        this.TextSetter = (LinkPropertyControl)target;
        //        return;
        //    case 3:
        //        this.TextInputBox = (ExpressionTextBox)target;
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
