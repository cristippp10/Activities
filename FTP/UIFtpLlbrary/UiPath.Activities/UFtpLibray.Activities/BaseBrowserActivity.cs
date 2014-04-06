using System;
using System.Activities;
using System.ComponentModel;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseBrowserActivity : BaseActivity
	{
		[Browsable(false), Category("Target")]
		public InArgument<Browser> ExistingUiBrowser
		{
			get;
			set;
		}
		[Category("Target")]
		public InArgument<int> TimeoutMS
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("ExistingUiBrowser", typeof(Browser), ArgumentDirection.In, false));
			metadata.AddArgument(new RuntimeArgument("TimeoutMS", typeof(int), ArgumentDirection.In, true));
			base.CacheMetadata(metadata);
		}
	}
}
