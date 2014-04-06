using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic.Activities;
using System;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;
namespace FtpActivities.Design
{
	internal class VariableToTextConverter : IValueConverter
	{
		[CompilerGenerated]
		private static class ConvertStatic
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site1;
			public static CallSite<Func<CallSite, object, object, object>> p__Site2;
			public static CallSite<Func<CallSite, object, object>> p__Site3;
			public static CallSite<Func<CallSite, object, string>> p__Site4;
			public static CallSite<Func<CallSite, object, object>> p__Site5;
			public static CallSite<Func<CallSite, object, object>> p__Site6;
		}
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ModelItem modelItem = value as ModelItem;
			if (value == null)
			{
				return null;
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
			if (inArgument == null)
			{
				return null;
			}
			if (inArgument.Expression == null)
			{
				return inArgument.ToString();
			}
			object expression = inArgument.Expression;
			string text;
			try
			{
				if (VariableToTextConverter.ConvertStatic.p__Site1 == null)
				{
					VariableToTextConverter.ConvertStatic.p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(VariableToTextConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> arg_12F_0 = VariableToTextConverter.ConvertStatic.p__Site1.Target;
                CallSite arg_12F_1 = VariableToTextConverter.ConvertStatic.p__Site1;
				if (VariableToTextConverter.ConvertStatic.p__Site2 == null)
				{
					VariableToTextConverter.ConvertStatic.p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(VariableToTextConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Func<CallSite, object, object, object> arg_12A_0 = VariableToTextConverter.ConvertStatic.p__Site2.Target;
				CallSite arg_12A_1 = VariableToTextConverter.ConvertStatic.p__Site2;
				if (VariableToTextConverter.ConvertStatic.p__Site3 == null)
				{
					VariableToTextConverter.ConvertStatic.p__Site3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(VariableToTextConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				if (arg_12F_0(arg_12F_1, arg_12A_0(arg_12A_1, VariableToTextConverter.ConvertStatic.p__Site3.Target(VariableToTextConverter.ConvertStatic.p__Site3, expression), null)))
				{
					object result = null;
					return result;
				}
				if (VariableToTextConverter.ConvertStatic.p__Site4 == null)
				{
					VariableToTextConverter.ConvertStatic.p__Site4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(VariableToTextConverter)));
				}
				Func<CallSite, object, string> arg_218_0 = VariableToTextConverter.ConvertStatic.p__Site4.Target;
				CallSite arg_218_1 = VariableToTextConverter.ConvertStatic.p__Site4;
				if (VariableToTextConverter.ConvertStatic.p__Site5 == null)
				{
					VariableToTextConverter.ConvertStatic.p__Site5 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(VariableToTextConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object> arg_213_0 = VariableToTextConverter.ConvertStatic.p__Site5.Target;
				CallSite arg_213_1 = VariableToTextConverter.ConvertStatic.p__Site5;
				if (VariableToTextConverter.ConvertStatic.p__Site6 == null)
				{
					VariableToTextConverter.ConvertStatic.p__Site6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(VariableToTextConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				text = arg_218_0(arg_218_1, arg_213_0(arg_213_1, VariableToTextConverter.ConvertStatic.p__Site6.Target(VariableToTextConverter.ConvertStatic.p__Site6, expression)));
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
			}
			catch
			{
				object result = inArgument.Expression.ToString();
				return result;
			}
			return text;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result;
			try
			{
				InArgument<string> inArgument = new InArgument<string>();
				if (value == null)
				{
					inArgument.Expression = null;
				}
				string text = value.ToString();
				if (string.IsNullOrWhiteSpace(text))
				{
					inArgument.Expression = null;
				}
				inArgument.Expression = new VisualBasicValue<string>(text);
				result = inArgument;
			}
			catch (Exception ex)
			{
                //AppLog.Instance.Info(ex.Message);
				result = null;
			}
			return result;
		}
	}
}
