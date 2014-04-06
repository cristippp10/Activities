using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.XamlIntegration;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public class ExecuteXamlWorkflowDesigner : ActivityDesigner, IComponentConnector
	{
		private string CurrentlyLoadedWorkflowPath;
		internal FilePathControl FileNameTextBox;
		private bool _contentLoaded;
		public ExecuteXamlWorkflowDesigner()
		{
			this.InitializeComponent();
		}
		private void FileDialogButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "UIWF Workflow | *.uiwf"
			};
			if (string.IsNullOrWhiteSpace(this.CurrentlyLoadedWorkflowPath))
			{
				DesignerSettings value = base.ModelItem.GetEditingContext().Items.GetValue<DesignerSettings>();
				if (value != null && !string.IsNullOrEmpty(value.WorkspacePath))
				{
					openFileDialog.InitialDirectory = value.WorkspacePath;
				}
			}
			else
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(this.CurrentlyLoadedWorkflowPath);
			}
			if (openFileDialog.ShowDialog() == true)
			{
				base.ModelItem.Properties["WorkflowFileName"].SetValue(new InArgument<string>(Path.GetFileName(openFileDialog.FileName)));
			}
		}
		private void DefineArgsButton_Click(object sender, RoutedEventArgs e)
		{
			this.InitDynamicArgumentDialog();
			this.EditArgsButton_Click(sender, e);
		}
		private void EditArgsButton_Click(object sender, RoutedEventArgs e)
		{
			DynamicArgumentDesignerOptions options = new DynamicArgumentDesignerOptions
			{
				Title = "Invoked workflow's arguments"
			};
			ModelItem dictionary = base.ModelItem.Properties["Arguments"].Dictionary;
			using (ModelEditingScope modelEditingScope = dictionary.BeginEdit("ChildArgumentEditing"))
			{
				if (DynamicArgumentDialog.ShowDialog(base.ModelItem, dictionary, base.Context, base.ModelItem.View, options))
				{
					modelEditingScope.Complete();
				}
				else
				{
					modelEditingScope.Revert();
				}
			}
		}
		private void InitDynamicArgumentDialog()
		{
			ModelProperty modelProperty = base.ModelItem.Properties["WorkflowFileName"];
			if (modelProperty == null)
			{
				return;
			}
			string text = new VariableToTextConverter().Convert(modelProperty.ComputedValue as InArgument<string>, typeof(string), null, CultureInfo.CurrentCulture) as string;
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			string workflowFullPath = this.GetWorkflowFullPath(text);
			Dictionary<string, Argument> dictionary = new Dictionary<string, Argument>();
			if (base.ModelItem.Properties["Arguments"].ComputedValue == null)
			{
				base.ModelItem.Properties["Arguments"].SetValue(new Dictionary<string, Argument>());
			}
			this.CurrentlyLoadedWorkflowPath = workflowFullPath;
			try
			{
				DynamicActivity dynamicActivity = ActivityXamlServices.Load(workflowFullPath) as DynamicActivity;
				if (dynamicActivity != null)
				{
					foreach (DynamicActivityProperty current in dynamicActivity.Properties)
					{
						Argument argument = null;
						if (current.Type.GetGenericTypeDefinition().BaseType == typeof(InArgument))
						{
							argument = Argument.Create(current.Type.GetGenericArguments()[0], ArgumentDirection.In);
						}
						if (current.Type.GetGenericTypeDefinition().BaseType == typeof(OutArgument))
						{
							argument = Argument.Create(current.Type.GetGenericArguments()[0], ArgumentDirection.Out);
						}
						if (current.Type.GetGenericTypeDefinition().BaseType == typeof(InOutArgument))
						{
							argument = Argument.Create(current.Type.GetGenericArguments()[0], ArgumentDirection.InOut);
						}
						if (argument != null)
						{
							dictionary.Add(current.Name, argument);
						}
					}
					base.ModelItem.Properties["Arguments"].SetValue(dictionary);
				}
			}
			catch
			{
			}
		}
		private string GetWorkflowFullPath(string workflowFile)
		{
			if (File.Exists(workflowFile))
			{
				return workflowFile;
			}
			DesignerSettings value = base.ModelItem.GetEditingContext().Items.GetValue<DesignerSettings>();
			if (value == null || string.IsNullOrEmpty(value.WorkspacePath))
			{
				return workflowFile;
			}
			return Path.Combine(value.WorkspacePath, workflowFile);
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/execute/executexamlworkflowdesigner.xaml", UriKind.Relative);
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
				((Button)target).Click += new RoutedEventHandler(this.EditArgsButton_Click);
				return;
			case 3:
				((Button)target).Click += new RoutedEventHandler(this.DefineArgsButton_Click);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
