using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using FtpActivities.Properties;
using FtpActivities.Utilities;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseActivity : AsyncNativeActivity
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private static class BeginExecute_o__SiteContainer0
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site1;
			public static CallSite<Func<CallSite, object, object, object>> p__Site2;
		}
		public System.Collections.Generic.Dictionary<string, object> RuntimeProperties = new System.Collections.Generic.Dictionary<string, object>();
		protected Action ExecuteAsyncAction;
		protected bool HighlightElements;
		[Category("Common")]
		public InArgument<bool> ContinueOnError
		{
			get;
			set;
		}
		[Browsable(false)]
		public string DesignerSelector
		{
			get;
			set;
		}
		[Browsable(false)]
		public UiPath.Library.Region ElementPosition
		{
			get;
			set;
		}
		[Browsable(false), System.Obsolete]
		public string ImageBase64
		{
			get;
			set;
		}
		[Browsable(false)]
		public string InformativeScreenshot
		{
			get;
			set;
		}
		[Browsable(false)]
		public string ParentImageBase64
		{
			get;
			set;
		}
		[Browsable(false), System.Obsolete]
		public bool ShowScreenshot
		{
			get;
			set;
		}
		protected bool IsActivityCanceled
		{
			get;
			set;
		}
		protected static void HighlightElement(UiElement node)
		{
			try
			{
				node.StartHighlight(Color.Red);
				System.Threading.Thread.Sleep(int.Parse(Resources.HighlightSleepTime));
				node.StopHighlight();
			}
			catch
			{
			}
		}
		protected override System.IAsyncResult BeginExecute(NativeActivityContext context, System.AsyncCallback callback, object state)
		{
			try
			{
				this.CheckPermissions();
				System.Reflection.PropertyInfo[] properties = base.GetType().GetProperties();
				System.Reflection.PropertyInfo[] array = properties;
				for (int i = 0; i < array.Length; i++)
				{
					System.Reflection.PropertyInfo propertyInfo = array[i];
					if (propertyInfo.PropertyType.BaseType != null && propertyInfo.PropertyType.BaseType.Equals(typeof(InArgument)))
					{
						object obj = propertyInfo.GetValue(this, null);
						if (BaseActivity.BeginExecute_o__SiteContainer0.p__Site1 == null)
						{
							BaseActivity.BeginExecute_o__SiteContainer0.p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BaseActivity), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, bool> arg_101_0 = BaseActivity.BeginExecute_o__SiteContainer0.p__Site1.Target;
						CallSite arg_101_1 = BaseActivity.BeginExecute_o__SiteContainer0.p__Site1;
						if (BaseActivity.BeginExecute_o__SiteContainer0.p__Site2 == null)
						{
							BaseActivity.BeginExecute_o__SiteContainer0.p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(BaseActivity), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						if (!arg_101_0(arg_101_1, BaseActivity.BeginExecute_o__SiteContainer0.p__Site2.Target(BaseActivity.BeginExecute_o__SiteContainer0.p__Site2, obj, null)))
						{
							obj = (obj as InArgument).Get(context);
							this.RuntimeProperties[propertyInfo.Name] = obj;
						}
					}
				}
				System.Reflection.PropertyInfo property = base.GetType().GetProperty(Resources.ElementPropertyName);
				UiElement uiElement = context.Properties.Find(Resources.ElementPropertyName) as UiElement;
				if (property != null && uiElement != null && this.RuntimeProperties[Resources.ElementPropertyName] == null)
				{
					this.RuntimeProperties[Resources.ElementPropertyName] = uiElement;
				}
				if (this is BaseBrowserActivity)
				{
					System.Reflection.PropertyInfo property2 = base.GetType().GetProperty(Resources.BrowserPropertyName);
					Browser browser = context.Properties.Find(Resources.BrowserPropertyName) as Browser;
					if (property2 != null && browser != null && this.RuntimeProperties[Resources.BrowserPropertyName] == null)
					{
						this.RuntimeProperties[Resources.BrowserPropertyName] = browser;
					}
				}
			}
			catch (System.Exception ex)
			{
				this.HandleException(ex, this.ContinueOnError.Get(context));
			}
			try
			{
				System.Collections.Generic.Dictionary<string, object> extension = context.GetExtension<System.Collections.Generic.Dictionary<string, object>>();
				this.HighlightElements = (bool)extension["HighlightElements"];
			}
			catch
			{
			}
			this.ExecuteAsyncAction = new Action(this.ExecuteAsync);
			return this.ExecuteAsyncAction.BeginInvoke(callback, state);
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("ContinueOnError", typeof(bool), ArgumentDirection.In, false));
			base.CacheMetadata(metadata);
		}
		protected override void Cancel(NativeActivityContext context)
		{
			this.IsActivityCanceled = true;
			context.MarkCanceled();
			context.CancelChildren();
		}
		protected override void EndExecute(NativeActivityContext context, System.IAsyncResult result)
		{
			if (this.ExecuteAsyncAction != null)
			{
				this.ExecuteAsyncAction.EndInvoke(result);
			}
			try
			{
				System.Reflection.PropertyInfo[] properties = base.GetType().GetProperties();
				System.Reflection.PropertyInfo[] array = properties;
				for (int i = 0; i < array.Length; i++)
				{
					System.Reflection.PropertyInfo propertyInfo = array[i];
					if (propertyInfo.PropertyType.BaseType != null && propertyInfo.PropertyType.BaseType.Equals(typeof(OutArgument)) && this.RuntimeProperties.ContainsKey(propertyInfo.Name))
					{
						OutArgument outArgument = propertyInfo.GetValue(this, null) as OutArgument;
						outArgument.Set(context, this.RuntimeProperties[propertyInfo.Name]);
					}
				}
			}
			catch (System.Exception ex)
			{
				this.HandleException(ex, this.ContinueOnError.Get(context));
			}
		}
		protected abstract void ExecuteAsync();
		protected void HandleException(System.Exception ex)
		{
			this.HandleException(ex, this.ContinueOnError.Get<bool>());
		}
		protected void HandleException(System.Exception ex, bool continueOnError)
		{
			if (this.IsActivityCanceled)
			{
				return;
			}
			if (continueOnError)
			{
				return;
			}
			string message = string.Format("{0}: {1}", base.DisplayName, ex.Message);
			if (ex is System.Runtime.InteropServices.COMException)
			{
				System.Runtime.InteropServices.COMException ex2 = ex as System.Runtime.InteropServices.COMException;
				string a = ex2.ErrorCode.ToString("X");
				if (a == "80040212")
				{
					throw new SelectorException(message, ex);
				}
			}
			throw new WorkflowApplicationException(message, ex);
		}
	}
}
