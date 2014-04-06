using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class TextEditorWindow : WorkflowElementDialog, IComponentConnector
	{
        //internal TextBox TextBoxEditor;
        //private bool _contentLoaded;
        public TextEditorWindow(ModelItem ownerActivity)
        {
            base.ModelItem = ownerActivity;
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
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/windows/texteditorwindow.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    if (connectionId == 1)
        //    {
        //        this.TextBoxEditor = (TextBox)target;
        //        return;
        //    }
        //    this._contentLoaded = true;
        //}
	}
}
