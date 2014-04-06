using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;
namespace FtpActivities.Design
{
	internal class VariableActivityDisplayConverter : IValueConverter
	{
		[CompilerGenerated]
		private static class ConvertStatic
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site1;
			public static CallSite<Func<CallSite, object, object, object>> p__Site2;
			public static CallSite<Func<CallSite, object, bool>> p__Site3;
			public static CallSite<Func<CallSite, object, object, object>> p__Site4;
			public static CallSite<Func<CallSite, object, object>> p__Site5;
			public static CallSite<Func<CallSite, object, string>> p__Site6;
			public static CallSite<Func<CallSite, object, object>> p__Site7;
			public static CallSite<Func<CallSite, object, object>> p__Site8;
			public static CallSite<Func<CallSite, object, string>> p__Site9;
			public static CallSite<Func<CallSite, object, object>> p__Sitea;
		}
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				ModelItem modelItem = value as ModelItem;
				if (value == null)
				{
					object result = null;
					return result;
				}
				InArgument inArgument;
				if (modelItem != null)
				{
					inArgument = (modelItem.GetCurrentValue() as InArgument);
				}
				else
				{
					inArgument = (value as InArgument);
				}
				if (inArgument != null)
				{
					object expression = inArgument.Expression;
					if (VariableActivityDisplayConverter.ConvertStatic.p__Site1 == null)
					{
						VariableActivityDisplayConverter.ConvertStatic.p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> arg_D7_0 = VariableActivityDisplayConverter.ConvertStatic.p__Site1.Target;
					CallSite arg_D7_1 = VariableActivityDisplayConverter.ConvertStatic.p__Site1;
					if (VariableActivityDisplayConverter.ConvertStatic.p__Site2 == null)
					{
						VariableActivityDisplayConverter.ConvertStatic.p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					if (arg_D7_0(arg_D7_1, VariableActivityDisplayConverter.ConvertStatic.p__Site2.Target(VariableActivityDisplayConverter.ConvertStatic.p__Site2, expression, null)))
					{
						object result = inArgument.ToString();
						return result;
					}
					if (VariableActivityDisplayConverter.ConvertStatic.p__Site3 == null)
					{
						VariableActivityDisplayConverter.ConvertStatic.p__Site3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> arg_1DC_0 = VariableActivityDisplayConverter.ConvertStatic.p__Site3.Target;
					CallSite arg_1DC_1 = VariableActivityDisplayConverter.ConvertStatic.p__Site3;
					if (VariableActivityDisplayConverter.ConvertStatic.p__Site4 == null)
					{
						VariableActivityDisplayConverter.ConvertStatic.p__Site4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> arg_1D7_0 = VariableActivityDisplayConverter.ConvertStatic.p__Site4.Target;
					CallSite arg_1D7_1 = VariableActivityDisplayConverter.ConvertStatic.p__Site4;
					if (VariableActivityDisplayConverter.ConvertStatic.p__Site5 == null)
					{
						VariableActivityDisplayConverter.ConvertStatic.p__Site5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					string text;
					if (arg_1DC_0(arg_1DC_1, arg_1D7_0(arg_1D7_1, VariableActivityDisplayConverter.ConvertStatic.p__Site5.Target(VariableActivityDisplayConverter.ConvertStatic.p__Site5, expression), null)))
					{
						if (VariableActivityDisplayConverter.ConvertStatic.p__Site6 == null)
						{
							VariableActivityDisplayConverter.ConvertStatic.p__Site6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(VariableActivityDisplayConverter)));
						}
						Func<CallSite, object, string> arg_2C0_0 = VariableActivityDisplayConverter.ConvertStatic.p__Site6.Target;
						CallSite arg_2C0_1 = VariableActivityDisplayConverter.ConvertStatic.p__Site6;
						if (VariableActivityDisplayConverter.ConvertStatic.p__Site7 == null)
						{
							VariableActivityDisplayConverter.ConvertStatic.p__Site7 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, object> arg_2BB_0 = VariableActivityDisplayConverter.ConvertStatic.p__Site7.Target;
						CallSite arg_2BB_1 = VariableActivityDisplayConverter.ConvertStatic.p__Site7;
						if (VariableActivityDisplayConverter.ConvertStatic.p__Site8 == null)
						{
							VariableActivityDisplayConverter.ConvertStatic.p__Site8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						text = arg_2C0_0(arg_2C0_1, arg_2BB_0(arg_2BB_1, VariableActivityDisplayConverter.ConvertStatic.p__Site8.Target(VariableActivityDisplayConverter.ConvertStatic.p__Site8, expression)));
					}
					else
					{
						if (VariableActivityDisplayConverter.ConvertStatic.p__Site9 == null)
						{
							VariableActivityDisplayConverter.ConvertStatic.p__Site9 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(VariableActivityDisplayConverter)));
						}
						Func<CallSite, object, string> arg_356_0 = VariableActivityDisplayConverter.ConvertStatic.p__Site9.Target;
						CallSite arg_356_1 = VariableActivityDisplayConverter.ConvertStatic.p__Site9;
						if (VariableActivityDisplayConverter.ConvertStatic.p__Sitea == null)
						{
							VariableActivityDisplayConverter.ConvertStatic.p__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(VariableActivityDisplayConverter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						text = arg_356_0(arg_356_1, VariableActivityDisplayConverter.ConvertStatic.p__Sitea.Target(VariableActivityDisplayConverter.ConvertStatic.p__Sitea, expression));
					}
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = text;
						Dictionary<string, Activity> objectBrowser = TargetHelper.GetObjectBrowser(modelItem);
						if (objectBrowser.ContainsKey(text))
						{
							string text3 = objectBrowser[text].DisplayName;
							int num = 32;
							if (text3.Length > num)
							{
								text3 = text3.Remove(num);
								text3 += "...";
							}
							text2 = string.Format("{0} ({1})", text2, text3);
						}
						else
						{
							if (modelItem.Source != null && !string.IsNullOrWhiteSpace(modelItem.Source.Name))
							{
								text2 = string.Format("{0}", text2);
							}
						}
						object result = text2;
						return result;
					}
				}
			}
			catch (Exception ex)
			{
				//modif
                //AppLog.Instance.Info(ex.Message);
			}
			return null;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
