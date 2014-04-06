using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Activities;
using System.Activities.Presentation.PropertyEditing;
using System.Runtime.CompilerServices;
using System.Windows;
using UiPath.Workflow.Wizards;
namespace FtpActivities.Design
{
	public class XmlEditor : DialogPropertyValueEditor
	{
		[CompilerGenerated]
		private static class ShowDialogStatic
		{
			public static CallSite<Func<CallSite, object, string>> p__Site1;
			public static CallSite<Func<CallSite, object, object>> p__Site2;
			public static CallSite<Func<CallSite, object, object>> p__Site3;
		}
		public XmlEditor()
		{
			base.InlineEditorTemplate = (DataTemplate)EditorResources.GetResources()["XmlEditorTemplate"];
		}
		public override void ShowDialog(PropertyValue propertyValue, IInputElement commandSource)
		{
			string xmlText = string.Empty;
			try
			{
				if (propertyValue != null && propertyValue.Value != null)
				{
					object value = propertyValue.Value;
					if (XmlEditor.ShowDialogStatic.p__Site1 == null)
					{
						XmlEditor.ShowDialogStatic.p__Site1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(XmlEditor)));
					}
					Func<CallSite, object, string> arg_F4_0 = XmlEditor.ShowDialogStatic.p__Site1.Target;
					CallSite arg_F4_1 = XmlEditor.ShowDialogStatic.p__Site1;
					if (XmlEditor.ShowDialogStatic.p__Site2 == null)
					{
						XmlEditor.ShowDialogStatic.p__Site2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(XmlEditor), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> arg_EF_0 = XmlEditor.ShowDialogStatic.p__Site2.Target;
					CallSite arg_EF_1 = XmlEditor.ShowDialogStatic.p__Site2;
					if (XmlEditor.ShowDialogStatic.p__Site3 == null)
					{
						XmlEditor.ShowDialogStatic.p__Site3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(XmlEditor), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					xmlText = arg_F4_0(arg_F4_1, arg_EF_0(arg_EF_1, XmlEditor.ShowDialogStatic.p__Site3.Target(XmlEditor.ShowDialogStatic.p__Site3, value)));
				}
				EditMetadataWnd editMetadataWnd = new EditMetadataWnd(xmlText);
				if (editMetadataWnd.ShowDialog() == true)
				{
					propertyValue.Value = new InArgument<string>(editMetadataWnd.XmlMetadata);
				}
			}
			catch
			{
			}
		}
	}
}
