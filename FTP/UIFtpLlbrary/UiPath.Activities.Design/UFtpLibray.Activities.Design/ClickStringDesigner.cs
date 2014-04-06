using System;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using UiPath.Workflow.Wizards;
namespace FtpActivities.Design
{
	public partial class ClickStringDesigner : ActivityDesigner, IComponentConnector
    {
    //    internal TargetSelectorControl TargetSelector;
    //    internal LinkPropertyControl TextSetter;
    //    internal ExpressionTextBox TextInputBox;
    //    private bool _contentLoaded;
		public ClickStringDesigner()
		{
			this.InitializeComponent();
		}
		private void OpenUiStudio_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Dictionary<string, object> dictionary = CommonActions.SetOptionsDictionary(new string[]
			{
				"ScrapingMethod",
				"OCREngine",
				"OCRLanguage",
				"AllowedCharacters",
				"DeniedCharacters",
				"Correction",
				"Invert",
				"FormattedText"
			}, base.ModelItem);
			string selector = null;
			try
			{
				selector = base.ModelItem.Properties["DesignerSelector"].ComputedValue.ToString();
			}
			catch
			{
			}
			NativeWindow.HideMain();
			try
			{
				ScrapeWizard scrapeWizard = new ScrapeWizard(dictionary, selector, true);
				if (scrapeWizard.ShowDialog(null))
				{
					foreach (string current in dictionary.Keys)
					{
						if (base.ModelItem.Properties.Find(current) != null)
						{
							base.ModelItem.Properties[current].SetValue(dictionary[current]);
						}
					}
				}
			}
			finally
			{
				NativeWindow.ShowMain();
			}
		}
		private void ResetLink_MouseDown(object sender, MouseButtonEventArgs e)
		{
			base.ModelItem.Properties["Text"].SetValue(null);
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/text/clickstringdesigner.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //internal Delegate _CreateDelegate(Type delegateType, string handler)
        //{
        //    return Delegate.CreateDelegate(delegateType, this, handler);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.TargetSelector = (TargetSelectorControl)target;
        //        return;
        //    case 2:
        //        this.TextSetter = (LinkPropertyControl)target;
        //        return;
        //    case 3:
        //        this.TextInputBox = (ExpressionTextBox)target;
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
