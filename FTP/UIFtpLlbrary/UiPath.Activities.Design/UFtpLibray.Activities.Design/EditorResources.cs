using System;
using System.Activities.Presentation.Model;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using UiPath.Library;
namespace FtpActivities.Design
{
	public class EditorResources : ResourceDictionary, IComponentConnector, IStyleConnector
	{
		private static ResourceDictionary resources;
		private OCREngine LastOcrEngine;
		private bool _contentLoaded;
		internal static ResourceDictionary GetResources()
		{
			if (EditorResources.resources == null)
			{
				EditorResources.resources = new EditorResources();
			}
			return EditorResources.resources;
		}
		public EditorResources()
		{
			this.InitializeComponent();
		}
		private void ComboBox_DropDownOpened(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			ModelItem modelItem = comboBox.Tag as ModelItem;
			OCREngine oCREngine = (OCREngine)modelItem.Properties["OCREngine"].ComputedValue;
			if (oCREngine != this.LastOcrEngine)
			{
				this.LastOcrEngine = oCREngine;
				comboBox.ItemsSource = Setup.GetOCRLanguages(oCREngine);
				comboBox.SelectedItem = ((oCREngine == null) ? "eng" : "English");
			}
		}
		private void ComboBox_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			ModelItem modelItem = comboBox.Tag as ModelItem;
			this.LastOcrEngine = (OCREngine)modelItem.Properties["OCREngine"].ComputedValue;
			comboBox.ItemsSource = Setup.GetOCRLanguages(this.LastOcrEngine);
			comboBox.SelectedItem = modelItem.Properties["OCRLanguage"].ComputedValue;
		}
		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;
			ModelItem modelItem = comboBox.Tag as ModelItem;
			if (comboBox.SelectedValue != null)
			{
				modelItem.Properties["OCRLanguage"].SetValue(comboBox.SelectedValue.ToString());
			}
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/FtpActivities.Design;component/editors/editorresources.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				return;
			}
			((ComboBox)target).Loaded += new RoutedEventHandler(this.ComboBox_Loaded);
			((ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
			((ComboBox)target).DropDownOpened += new EventHandler(this.ComboBox_DropDownOpened);
		}
	}
}
