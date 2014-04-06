using System;
using System.Activities.Presentation;
using UiPath.Workflow.Wizards;
namespace FtpActivities.Design
{
	public class DesignerSettings : ContextItem
	{
		public override Type ItemType
		{
			get
			{
				return typeof(DesignerSettings);
			}
		}
		public InformativeScreenshot InformativeScreenshotSetting
		{
			get;
			set;
		}
		public string WorkspacePath
		{
			get;
			set;
		}
	}
}
