using System;
using System.Activities.Presentation.Converters;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.PropertyEditing;
using System.Windows;
namespace FtpActivities.Design
{
	internal class TextEditor : DialogPropertyValueEditor
	{
		public TextEditor()
		{
			base.InlineEditorTemplate = (DataTemplate)EditorResources.GetResources()["TextEditorTemplate"];
		}
		public override void ShowDialog(PropertyValue propertyValue, IInputElement commandSource)
		{
			try
			{
				ModelItem modelItem = new ModelPropertyEntryToOwnerActivityConverter().Convert(propertyValue.ParentProperty, typeof(ModelItem), false, null) as ModelItem;
				using (ModelEditingScope modelEditingScope = modelItem.BeginEdit())
				{
					TextEditorWindow textEditorWindow = new TextEditorWindow(modelItem);
					if (textEditorWindow.ShowOkCancel())
					{
						modelEditingScope.Complete();
					}
					else
					{
						modelEditingScope.Revert();
					}
				}
			}
			catch
			{
			}
		}
	}
}
