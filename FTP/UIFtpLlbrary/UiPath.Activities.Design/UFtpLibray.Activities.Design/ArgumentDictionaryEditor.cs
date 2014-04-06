using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Converters;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.PropertyEditing;
using System.Windows;
namespace FtpActivities.Design
{
	internal class ArgumentDictionaryEditor : DialogPropertyValueEditor
	{
		public ArgumentDictionaryEditor()
		{
			base.InlineEditorTemplate = (DataTemplate)EditorResources.GetResources()["ArgumentDictionaryEditor"];
		}
		public override void ShowDialog(PropertyValue propertyValue, IInputElement commandSource)
		{
			string propertyName = propertyValue.ParentProperty.PropertyName;
			ModelItem modelItem = new ModelPropertyEntryToOwnerActivityConverter().Convert(propertyValue.ParentProperty, typeof(ModelItem), false, null) as ModelItem;
			EditingContext editingContext = modelItem.GetEditingContext();
			DynamicArgumentDesignerOptions options = new DynamicArgumentDesignerOptions
			{
				Title = propertyName
			};
			ModelItem dictionary = modelItem.Properties[propertyName].Dictionary;
			using (ModelEditingScope modelEditingScope = dictionary.BeginEdit(propertyName + "Editing"))
			{
				if (DynamicArgumentDialog.ShowDialog(modelItem, dictionary, editingContext, modelItem.View, options))
				{
					modelEditingScope.Complete();
				}
				else
				{
					modelEditingScope.Revert();
				}
			}
		}
	}
}
