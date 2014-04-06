using System;
using System.Activities.Presentation.PropertyEditing;
using System.Windows;
namespace FtpActivities.Design
{
	internal class RegionEditor : ExtendedPropertyValueEditor
	{
		public RegionEditor()
		{
			base.InlineEditorTemplate = (DataTemplate)EditorResources.GetResources()["RegionEditor"];
			base.ExtendedEditorTemplate = (DataTemplate)EditorResources.GetResources()["RegionExtendedEditorTemplate"];
		}
	}
}
