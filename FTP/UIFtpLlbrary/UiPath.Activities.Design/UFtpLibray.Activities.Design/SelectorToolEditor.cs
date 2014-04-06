using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Activities;
using System.Activities.Presentation.Converters;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.PropertyEditing;
using System.Runtime.CompilerServices;
using System.Windows;
using UiPath.Library;
namespace FtpActivities.Design
{
	internal class SelectorToolEditor : DialogPropertyValueEditor
	{
		[CompilerGenerated]
		private static class ShowDialogStatic
		{
			public static CallSite<Func<CallSite, object, string>> p__Site1;
			public static CallSite<Func<CallSite, object, object>> p__Site2;
			public static CallSite<Func<CallSite, object, object>> p__Site3;
		}
		public SelectorToolEditor()
		{
			base.InlineEditorTemplate = (DataTemplate)EditorResources.GetResources()["SelectorTool"];
		}
		public override void ShowDialog(PropertyValue propertyValue, IInputElement commandSource)
		{
			ModelItem modelItem = new ModelPropertyEntryToOwnerActivityConverter().Convert(propertyValue.ParentProperty, typeof(ModelItem), false, null) as ModelItem;
			if (modelItem == null)
			{
				return;
			}
			try
			{
				if (propertyValue != null && propertyValue.Value != null)
				{
					object value = propertyValue.Value;
					if (SelectorToolEditor.ShowDialogStatic.p__Site1 == null)
					{
						SelectorToolEditor.ShowDialogStatic.p__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(SelectorToolEditor)));
					}
					Func<CallSite, object, string> arg_11C_0 = SelectorToolEditor.ShowDialogStatic.p__Site1.Target;
					CallSite arg_11C_1 = SelectorToolEditor.ShowDialogStatic.p__Site1;
					if (SelectorToolEditor.ShowDialogStatic.p__Site2 == null)
					{
						SelectorToolEditor.ShowDialogStatic.p__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(SelectorToolEditor), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> arg_117_0 = SelectorToolEditor.ShowDialogStatic.p__Site2.Target;
					CallSite arg_117_1 = SelectorToolEditor.ShowDialogStatic.p__Site2;
					if (SelectorToolEditor.ShowDialogStatic.p__Site3 == null)
					{
						SelectorToolEditor.ShowDialogStatic.p__Site3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(SelectorToolEditor), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					string text = arg_11C_0(arg_11C_1, arg_117_0(arg_117_1, SelectorToolEditor.ShowDialogStatic.p__Site3.Target(SelectorToolEditor.ShowDialogStatic.p__Site3, value)));
					Selector selector = new Selector(text);
					if (selector.IsTopLevel())
					{
						text = this.OpenUiExplorer(text, null);
						this.UpdateSelectorProperty(propertyValue, text);
					}
					else
					{
                        //modif
                        string parentSelector = TargetHelper.GetParentSelector(modelItem);
                        text = this.OpenUiExplorer(parentSelector, text);
                        this.UpdateSelectorProperty(propertyValue, text);
					}
				}
			}
			catch
			{
				NativeMessageBox.ShowError("Selector cannot be edited", "Error");
			}
		}
		private void UpdateSelectorProperty(PropertyValue propertyValue, string currentActivitySelector)
		{
			if (!string.IsNullOrWhiteSpace(currentActivitySelector))
			{
				propertyValue.Value = new InArgument<string>(currentActivitySelector);
			}
		}
		private string IndicateParent()
		{
			NativeMessageBox.ShowInfo("Parent element could not be recreated. Please select it on screen.", "Info");
			string result;
			using (UiElement uiElement = UiElement.SelectNode(false, false, true))
			{
				if (uiElement == null)
				{
					result = null;
				}
				else
				{
					result = uiElement.Selector.Text;
				}
			}
			return result;
		}
		private string OpenUiExplorer(string parentSelector, string partialSelector = null)
		{
			string result;
			try
			{
				NativeWindow.HideMain();
				if (string.IsNullOrWhiteSpace(parentSelector))
				{
					parentSelector = this.IndicateParent();
					if (string.IsNullOrWhiteSpace(parentSelector))
					{
						throw new WorkflowApplicationException("Invalid parent");
					}
				}
				if (string.IsNullOrWhiteSpace(partialSelector))
				{
					result = Setup.OpenUiExplorer(parentSelector, null);
				}
				else
				{
					result = Setup.OpenUiExplorer(partialSelector, parentSelector);
				}
			}
			finally
			{
				NativeWindow.ShowMain();
			}
			return result;
		}
	}
}
