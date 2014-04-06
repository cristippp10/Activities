using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public class AppendLineDesigner : ActivityDesigner, IComponentConnector
	{
		private OpenFileDialog openFileDialog1 = new OpenFileDialog();
		internal FilePathControl FileNameTextBox;
		internal LinkPropertyControl TextSetter;
		internal ExpressionTextBox InputTextBox;
		private bool _contentLoaded;
		public AppendLineDesigner()
		{
			this.InitializeComponent();
			this.openFileDialog1.Title = "Select File";
			this.openFileDialog1.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
			this.openFileDialog1.FileOk += new CancelEventHandler(this.openFileDialog1_FileOk);
		}
		private void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			this.openFileDialog1.ShowDialog();
		}
		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			base.ModelItem.Properties["FileName"].SetValue(new InArgument<string>(this.openFileDialog1.FileName));
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/file/appendlinedesigner.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.FileNameTextBox = (FilePathControl)target;
				return;
			case 2:
				this.TextSetter = (LinkPropertyControl)target;
				return;
			case 3:
				this.InputTextBox = (ExpressionTextBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
