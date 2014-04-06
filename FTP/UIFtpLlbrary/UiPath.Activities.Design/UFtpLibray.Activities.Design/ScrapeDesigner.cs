using System;
using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using UiPath.Library;
using UiPath.Workflow.Wizards;
namespace FtpActivities.Design
{
	public partial class ScrapeDesigner : ActivityDesigner, IComponentConnector
	{
        //internal TargetSelectorControl TargetSelectorCtrl;
        //internal TextBlock OpenUiStudio;
        //private bool _contentLoaded;
		public ScrapeDesigner()
		{
			this.InitializeComponent();
		}
		private void OpenUiStudio_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string selector = null;
			try
			{
				selector = base.ModelItem.Properties["DesignerSelector"].ComputedValue.ToString();
				dictionary = CommonActions.SetOptionsDictionary(new string[]
				{
					"ScrapingMethod",
					"ExtractWordInfo",
					"OCREngine",
					"OCRLanguage",
					"AllowedCharacters",
					"DeniedCharacters",
					"Correction",
					"IgnoreHidden",
					"FormattedText",
					"ExtractFont"
				}, base.ModelItem);
			}
			catch
			{
			}
			try
			{
				NativeWindow.HideMain();
				ScrapeWizard scrapeWizard = new ScrapeWizard(dictionary, selector, false);
				if (scrapeWizard.ShowDialog(base.ModelItem.Properties["ClippingRegion"].ComputedValue as Region))
				{
					CommonActions.SaveScrapeOptions(dictionary, base.ModelItem);
				}
			}
			finally
			{
				NativeWindow.ShowMain();
			}
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/text/scrapedesigner.xaml", UriKind.Relative);
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
        //        this.TargetSelectorCtrl = (TargetSelectorControl)target;
        //        return;
        //    case 2:
        //        this.OpenUiStudio = (TextBlock)target;
        //        this.OpenUiStudio.MouseDown += new MouseButtonEventHandler(this.OpenUiStudio_MouseDown);
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
