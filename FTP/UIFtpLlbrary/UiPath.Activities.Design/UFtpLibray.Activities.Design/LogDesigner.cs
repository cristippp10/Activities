using System;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public class LogDesigner : ActivityDesigner, IComponentConnector
	{
		internal LogDesigner Log;
		internal Label LogLevelLabel;
		internal ComboBox LogLevelComboBox;
		internal LinkPropertyControl LogSetter;
		internal Label LogDataLabel;
		internal ExpressionTextBox LogDataTextBox;
		private bool _contentLoaded;
		public LogDesigner()
		{
			this.InitializeComponent();
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/system/logdesigner.xaml", UriKind.Relative);
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
				this.Log = (LogDesigner)target;
				return;
			case 2:
				this.LogLevelLabel = (Label)target;
				return;
			case 3:
				this.LogLevelComboBox = (ComboBox)target;
				return;
			case 4:
				this.LogSetter = (LinkPropertyControl)target;
				return;
			case 5:
				this.LogDataLabel = (Label)target;
				return;
			case 6:
				this.LogDataTextBox = (ExpressionTextBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
