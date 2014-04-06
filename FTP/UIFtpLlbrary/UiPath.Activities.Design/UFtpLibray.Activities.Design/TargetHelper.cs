using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic.Activities;
using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UiPath.Library;
namespace FtpActivities.Design
{
	public static class TargetHelper
	{
        public interface IContainerActivity
        {
            Activity Body
            {
                get;
                set;
            }
        }

		[CompilerGenerated]
		private static class GetObjectBrowserStatic
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site1;
			public static CallSite<Func<CallSite, object, object, object>> p__Site2;
			public static CallSite<Func<CallSite, object, bool>> p__Site3;
			public static CallSite<Func<CallSite, object, object, object>> p__Site4;
			public static CallSite<Func<CallSite, object, object, object>> p__Site5;
			public static CallSite<Func<CallSite, object, object>> p__Site6;
			public static CallSite<Func<CallSite, Dictionary<string, Activity>, object, Activity, object>> p__Site7;
			public static CallSite<Func<CallSite, object, object>> p__Site8;
			public static CallSite<Func<CallSite, object, object>> p__Site9;
		}
		[CompilerGenerated]
		private static class SetInArgumentValueStatic	{
			public static CallSite<Func<CallSite, object, bool>> p__Sitef;
			public static CallSite<Func<CallSite, object, object, object>> p__Site10;
			public static CallSite<Func<CallSite, object, string>> p__Site11;
			public static CallSite<Func<CallSite, object, object>> p__Site12;
			public static CallSite<Func<CallSite, object, object>> p__Site13;
			public static CallSite<Func<CallSite, object, string>> p__Site14;
			public static CallSite<Func<CallSite, object, object>> p__Site15;
			public static CallSite<Func<CallSite, object, object>> p__Site16;
			public static CallSite<Func<CallSite, object, string, object>> p__Site17;
			public static CallSite<Func<CallSite, object, string, object>> p__Site18;
			public static CallSite<Func<CallSite, object, object, object>> p__Site19;
			public static CallSite<Action<CallSite, ModelProperty, object>> p__Site1a;
		}
		[CompilerGenerated]
		private static class GetVariablesStatic
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site1c;
			public static CallSite<Func<CallSite, object, object, object>> p__Site1d;
			public static CallSite<Func<CallSite, object, object>> p__Site1e;
			public static CallSite<Func<CallSite, object, string>> p__Site1f;
			public static CallSite<Func<CallSite, object, object>> p__Site20;
			public static CallSite<Func<CallSite, object, object>> p__Site21;
			public static CallSite<Func<CallSite, object, object>> p__Site22;
		}
		public static Dictionary<string, Activity> GetObjectBrowser(ModelItem modelItem)
		{
			Dictionary<string, Activity> dictionary = new Dictionary<string, Activity>();
			ModelService service = modelItem.GetEditingContext().Services.GetService<ModelService>();
			IEnumerable<Activity> enumerable = 
				from x in service.Find(service.Root, typeof(Activity))
				select x.GetCurrentValue() as Activity;
			foreach (Activity current in enumerable)
			{
				foreach (PropertyInfo current2 in 
					from x in current.GetType().GetProperties()
					where x.PropertyType.BaseType == typeof(OutArgument)
					select x)
				{
					object arg = current2.GetValue(current, null) as OutArgument;
					if (TargetHelper.GetObjectBrowserStatic.p__Site1 == null)
					{
						TargetHelper.GetObjectBrowserStatic.p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> arg_2B2_0 = TargetHelper.GetObjectBrowserStatic.p__Site1.Target;
					CallSite arg_2B2_1 = TargetHelper.GetObjectBrowserStatic.p__Site1;
					if (TargetHelper.GetObjectBrowserStatic.p__Site2 == null)
					{
						TargetHelper.GetObjectBrowserStatic.p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					object obj = TargetHelper.GetObjectBrowserStatic.p__Site2.Target(TargetHelper.GetObjectBrowserStatic.p__Site2, arg, null);
					if (TargetHelper.GetObjectBrowserStatic.p__Site3 == null)
					{
						TargetHelper.GetObjectBrowserStatic.p__Site3 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					object arg_2B2_2;
					if (!TargetHelper.GetObjectBrowserStatic.p__Site3.Target(TargetHelper.GetObjectBrowserStatic.p__Site3, obj))
					{
						if (TargetHelper.GetObjectBrowserStatic.p__Site4 == null)
						{
							TargetHelper.GetObjectBrowserStatic.p__Site4 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(TargetHelper), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, object, object> arg_2A9_0 = TargetHelper.GetObjectBrowserStatic.p__Site4.Target;
						CallSite arg_2A9_1 = TargetHelper.GetObjectBrowserStatic.p__Site4;
						object arg_2A9_2 = obj;
						if (TargetHelper.GetObjectBrowserStatic.p__Site5 == null)
						{
							TargetHelper.GetObjectBrowserStatic.p__Site5 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(TargetHelper), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						Func<CallSite, object, object, object> arg_2A4_0 = TargetHelper.GetObjectBrowserStatic.p__Site5.Target;
						CallSite arg_2A4_1 = TargetHelper.GetObjectBrowserStatic.p__Site5;
						if (TargetHelper.GetObjectBrowserStatic.p__Site6 == null)
						{
							TargetHelper.GetObjectBrowserStatic.p__Site6 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(TargetHelper), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						arg_2B2_2 = arg_2A9_0(arg_2A9_1, arg_2A9_2, arg_2A4_0(arg_2A4_1, TargetHelper.GetObjectBrowserStatic.p__Site6.Target(TargetHelper.GetObjectBrowserStatic.p__Site6, arg), null));
					}
					else
					{
						arg_2B2_2 = obj;
					}
					if (arg_2B2_0(arg_2B2_1, arg_2B2_2))
					{
						if (TargetHelper.GetObjectBrowserStatic.p__Site7 == null)
						{
							TargetHelper.GetObjectBrowserStatic.p__Site7 = CallSite<Func<CallSite, Dictionary<string, Activity>, object, Activity, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetIndex(CSharpBinderFlags.None, typeof(TargetHelper), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						Func<CallSite, Dictionary<string, Activity>, object, Activity, object> arg_3B9_0 = TargetHelper.GetObjectBrowserStatic.p__Site7.Target;
						CallSite arg_3B9_1 = TargetHelper.GetObjectBrowserStatic.p__Site7;
						Dictionary<string, Activity> arg_3B9_2 = dictionary;
						if (TargetHelper.GetObjectBrowserStatic.p__Site8 == null)
						{
							TargetHelper.GetObjectBrowserStatic.p__Site8 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(TargetHelper), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, object> arg_3B3_0 = TargetHelper.GetObjectBrowserStatic.p__Site8.Target;
						CallSite arg_3B3_1 = TargetHelper.GetObjectBrowserStatic.p__Site8;
						if (TargetHelper.GetObjectBrowserStatic.p__Site9 == null)
						{
							TargetHelper.GetObjectBrowserStatic.p__Site9 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(TargetHelper), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						arg_3B9_0(arg_3B9_1, arg_3B9_2, arg_3B3_0(arg_3B3_1, TargetHelper.GetObjectBrowserStatic.p__Site9.Target(TargetHelper.GetObjectBrowserStatic.p__Site9, arg)), current);
					}
				}
			}
			return dictionary;
		}
		private static ModelItem GetDefiningActivity(ModelItem activity, Dictionary<string, Activity> variableToActivityMap)
		{
			ModelProperty modelProperty = activity.Properties.Find("ExistingUiElement");
			if (modelProperty == null)
			{
				return null;
			}
			InArgument<UiElement> inArgument = modelProperty.ComputedValue as InArgument<UiElement>;
			if (inArgument == null)
			{
				return null;
			}
			VisualBasicValue<UiElement> visualBasicValue = inArgument.Expression as VisualBasicValue<UiElement>;
			if (visualBasicValue == null)
			{
				return null;
			}
			string expressionText = visualBasicValue.ExpressionText;
			if (variableToActivityMap.ContainsKey(expressionText))
			{
				Activity item = variableToActivityMap[expressionText];
				return ModelFactory.CreateItem(activity.GetEditingContext(), item);
			}
			return null;
		}
		private static string ComputeActivitySelector(ModelItem activity, Dictionary<string, Activity> variableToActivityMap, int step = 0)
		{
			if (activity == null)
			{
				return null;
			}
            //modif
            //if (activity.ItemType == typeof(GenerateSelector) && TargetHelper.GetDefiningActivity(activity, variableToActivityMap) == null)
            //{
            //    return (activity.GetCurrentValue() as GenerateSelector).DesignerSelector;
            //}
			ModelProperty modelProperty = activity.Properties.Find("Selector");
			if (modelProperty == null)
			{
				return null;
			}
			InArgument<string> inArgument = modelProperty.ComputedValue as InArgument<string>;
			ModelItem definingActivity = TargetHelper.GetDefiningActivity(activity, variableToActivityMap);
			if (inArgument == null && definingActivity == null)
			{
				return null;
			}
			if (inArgument == null)
			{
                //modif
                //if (definingActivity.GetCurrentValue() is OpenPage && activity.Properties["DesignerSelector"] != null)
                //{
                //    return activity.Properties["DesignerSelector"].ComputedValue as string;
                //}
				return TargetHelper.ComputeActivitySelector(definingActivity, variableToActivityMap, step + 1);
			}
			else
			{
				string selectorValue = TargetHelper.GetSelectorValue(activity, inArgument, variableToActivityMap, step);
				if (new Selector(selectorValue).IsTopLevel())
				{
					return selectorValue;
				}
				if (definingActivity == null)
				{
					return null;
				}
                //modif
                //if (definingActivity.GetCurrentValue() is OpenPage && activity.Properties.Find("DesignerSelector") != null)
                //{
                //    return activity.Properties["DesignerSelector"].ComputedValue as string;
                //}
				string text = TargetHelper.ComputeActivitySelector(definingActivity, variableToActivityMap, 0);
				if (!string.IsNullOrWhiteSpace(text))
				{
					return text + selectorValue;
				}
				return null;
			}
		}
		private static string GetSelectorValue(ModelItem activity, InArgument<string> selectorArgument, Dictionary<string, Activity> variableToActivityMap, int step)
		{
			VisualBasicValue<string> visualBasicValue = selectorArgument.Expression as VisualBasicValue<string>;
			if (visualBasicValue == null)
			{
				Literal<string> literal = selectorArgument.Expression as Literal<string>;
				if (literal == null)
				{
					return null;
				}
				return literal.Value;
			}
			else
			{
				if (!variableToActivityMap.ContainsKey(visualBasicValue.ExpressionText))
				{
					return null;
				}
				Activity item = variableToActivityMap[visualBasicValue.ExpressionText];
				ModelItem activity2 = ModelFactory.CreateItem(activity.GetEditingContext(), item);
				return TargetHelper.ComputeActivitySelector(activity2, variableToActivityMap, step + 1);
			}
		}
        //modif
        public static string GetParentSelector(ModelItem childActivity)
        {
            Dictionary<string, Activity> objectBrowser = TargetHelper.GetObjectBrowser(childActivity);
            if (typeof(IContainerActivity).IsAssignableFrom(childActivity.ItemType))
            {
                ModelItem definingActivity = TargetHelper.GetDefiningActivity(childActivity, objectBrowser);
                return TargetHelper.ComputeActivitySelector(definingActivity, objectBrowser, 0);
            }
            ModelItem parent = childActivity.Parent.Parent.Parent;
            return TargetHelper.ComputeActivitySelector(parent, objectBrowser, 0);
        }
        //modif
        public static bool IsInContainer(ModelItem activity)
        {
            if (activity == null || activity.Parent == null || activity.Parent.Parent == null || activity.Parent.Parent.Parent == null)
            {
                return false;
            }
            ModelItem parent = activity.Parent.Parent.Parent;
            return typeof(IContainerActivity).IsAssignableFrom(parent.ItemType);
        }
		public static void SetInArgumentValue(ModelProperty property, string expression)
		{
			Type type = typeof(VisualBasicValue<>).MakeGenericType(property.PropertyType.GetGenericArguments());
			Activator.CreateInstance(type);
			object computedValue = property.ComputedValue;
			if (TargetHelper.SetInArgumentValueStatic.p__Sitef == null)
			{
				TargetHelper.SetInArgumentValueStatic.p__Sitef = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(TargetHelper), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> arg_C9_0 = TargetHelper.SetInArgumentValueStatic.p__Sitef.Target;
			CallSite arg_C9_1 = TargetHelper.SetInArgumentValueStatic.p__Sitef;
			if (TargetHelper.SetInArgumentValueStatic.p__Site10 == null)
			{
				TargetHelper.SetInArgumentValueStatic.p__Site10 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(TargetHelper), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			string text;
			if (arg_C9_0(arg_C9_1, TargetHelper.SetInArgumentValueStatic.p__Site10.Target(TargetHelper.SetInArgumentValueStatic.p__Site10, computedValue, null)))
			{
				text = string.Empty;
			}
			else
			{
				try
				{
					if (TargetHelper.SetInArgumentValueStatic.p__Site11 == null)
					{
						TargetHelper.SetInArgumentValueStatic.p__Site11 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(TargetHelper)));
					}
					Func<CallSite, object, string> arg_1B4_0 = TargetHelper.SetInArgumentValueStatic.p__Site11.Target;
					CallSite arg_1B4_1 = TargetHelper.SetInArgumentValueStatic.p__Site11;
					if (TargetHelper.SetInArgumentValueStatic.p__Site12 == null)
					{
						TargetHelper.SetInArgumentValueStatic.p__Site12 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> arg_1AF_0 = TargetHelper.SetInArgumentValueStatic.p__Site12.Target;
					CallSite arg_1AF_1 = TargetHelper.SetInArgumentValueStatic.p__Site12;
					if (TargetHelper.SetInArgumentValueStatic.p__Site13 == null)
					{
						TargetHelper.SetInArgumentValueStatic.p__Site13 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					text = arg_1B4_0(arg_1B4_1, arg_1AF_0(arg_1AF_1, TargetHelper.SetInArgumentValueStatic.p__Site13.Target(TargetHelper.SetInArgumentValueStatic.p__Site13, computedValue)));
				}
				catch
				{
					if (TargetHelper.SetInArgumentValueStatic.p__Site14 == null)
					{
						TargetHelper.SetInArgumentValueStatic.p__Site14 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(TargetHelper)));
					}
					Func<CallSite, object, string> arg_299_0 = TargetHelper.SetInArgumentValueStatic.p__Site14.Target;
					CallSite arg_299_1 = TargetHelper.SetInArgumentValueStatic.p__Site14;
					if (TargetHelper.SetInArgumentValueStatic.p__Site15 == null)
					{
						TargetHelper.SetInArgumentValueStatic.p__Site15 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Value", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> arg_294_0 = TargetHelper.SetInArgumentValueStatic.p__Site15.Target;
					CallSite arg_294_1 = TargetHelper.SetInArgumentValueStatic.p__Site15;
					if (TargetHelper.SetInArgumentValueStatic.p__Site16 == null)
					{
						TargetHelper.SetInArgumentValueStatic.p__Site16 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					text = arg_299_0(arg_299_1, arg_294_0(arg_294_1, TargetHelper.SetInArgumentValueStatic.p__Site16.Target(TargetHelper.SetInArgumentValueStatic.p__Site16, computedValue)));
				}
			}
			object obj = Activator.CreateInstance(type);
			if (Regex.IsMatch(text, "\\s*[+]\\s*$"))
			{
				if (TargetHelper.SetInArgumentValueStatic.p__Site17 == null)
				{
					TargetHelper.SetInArgumentValueStatic.p__Site17 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "ExpressionText", typeof(TargetHelper), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				TargetHelper.SetInArgumentValueStatic.p__Site17.Target(TargetHelper.SetInArgumentValueStatic.p__Site17, obj, text + expression);
			}
			else
			{
				if (TargetHelper.SetInArgumentValueStatic.p__Site18 == null)
				{
					TargetHelper.SetInArgumentValueStatic.p__Site18 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "ExpressionText", typeof(TargetHelper), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				TargetHelper.SetInArgumentValueStatic.p__Site18.Target(TargetHelper.SetInArgumentValueStatic.p__Site18, obj, expression);
			}
			object obj2 = Activator.CreateInstance(property.PropertyType);
			if (TargetHelper.SetInArgumentValueStatic.p__Site19 == null)
			{
				TargetHelper.SetInArgumentValueStatic.p__Site19 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "Expression", typeof(TargetHelper), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			TargetHelper.SetInArgumentValueStatic.p__Site19.Target(TargetHelper.SetInArgumentValueStatic.p__Site19, obj2, obj);
			if (TargetHelper.SetInArgumentValueStatic.p__Site1a == null)
			{
				TargetHelper.SetInArgumentValueStatic.p__Site1a = CallSite<Action<CallSite, ModelProperty, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetValue", null, typeof(TargetHelper), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			TargetHelper.SetInArgumentValueStatic.p__Site1a.Target(TargetHelper.SetInArgumentValueStatic.p__Site1a, property, obj2);
		}
		public static Dictionary<string, string> GetVariables(ModelItem item, Type type)
		{
			Activity activity = item.GetCurrentValue() as Activity;
			if (activity == null)
			{
				return null;
			}
			List<string> currentVars = new List<string>();
			foreach (PropertyInfo current in 
				from x in activity.GetType().GetProperties()
				where x.PropertyType.BaseType.Equals(typeof(OutArgument))
				select x)
			{
				object arg = item.Properties[current.Name];
				if (TargetHelper.GetVariablesStatic.p__Site1c == null)
				{
					TargetHelper.GetVariablesStatic.p__Site1c = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(TargetHelper), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> arg_171_0 = TargetHelper.GetVariablesStatic.p__Site1c.Target;
				CallSite arg_171_1 = TargetHelper.GetVariablesStatic.p__Site1c;
				if (TargetHelper.GetVariablesStatic.p__Site1d == null)
				{
					TargetHelper.GetVariablesStatic.p__Site1d = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(TargetHelper), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Func<CallSite, object, object, object> arg_16C_0 = TargetHelper.GetVariablesStatic.p__Site1d.Target;
				CallSite arg_16C_1 = TargetHelper.GetVariablesStatic.p__Site1d;
				if (TargetHelper.GetVariablesStatic.p__Site1e == null)
				{
					TargetHelper.GetVariablesStatic.p__Site1e = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ComputedValue", typeof(TargetHelper), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				if (arg_171_0(arg_171_1, arg_16C_0(arg_16C_1, TargetHelper.GetVariablesStatic.p__Site1e.Target(TargetHelper.GetVariablesStatic.p__Site1e, arg), null)))
				{
					if (TargetHelper.GetVariablesStatic.p__Site1f == null)
					{
						TargetHelper.GetVariablesStatic.p__Site1f = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(TargetHelper)));
					}
					Func<CallSite, object, string> arg_2A3_0 = TargetHelper.GetVariablesStatic.p__Site1f.Target;
					CallSite arg_2A3_1 = TargetHelper.GetVariablesStatic.p__Site1f;
					if (TargetHelper.GetVariablesStatic.p__Site20 == null)
					{
						TargetHelper.GetVariablesStatic.p__Site20 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> arg_29E_0 = TargetHelper.GetVariablesStatic.p__Site20.Target;
					CallSite arg_29E_1 = TargetHelper.GetVariablesStatic.p__Site20;
					if (TargetHelper.GetVariablesStatic.p__Site21 == null)
					{
						TargetHelper.GetVariablesStatic.p__Site21 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> arg_299_0 = TargetHelper.GetVariablesStatic.p__Site21.Target;
					CallSite arg_299_1 = TargetHelper.GetVariablesStatic.p__Site21;
					if (TargetHelper.GetVariablesStatic.p__Site22 == null)
					{
						TargetHelper.GetVariablesStatic.p__Site22 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ComputedValue", typeof(TargetHelper), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					string item2 = arg_2A3_0(arg_2A3_1, arg_29E_0(arg_29E_1, arg_299_0(arg_299_1, TargetHelper.GetVariablesStatic.p__Site22.Target(TargetHelper.GetVariablesStatic.p__Site22, arg))));
					currentVars.Add(item2);
				}
			}
			IEnumerable<ModelItem> parents = TargetHelper.GetParents(item);
			IEnumerable<ModelItem> enumerable = new Collection<ModelItem>();
			foreach (ModelItem current2 in parents)
			{
				if (current2.Properties["Variables"] != null)
				{
					enumerable = enumerable.Concat(current2.Properties["Variables"].Collection.Where(delegate(ModelItem x)
					{
						string item3 = x.Properties["Name"].ComputedValue.ToString();
						if (currentVars.Contains(item3))
						{
							return false;
						}
						Type type2 = x.ItemType.GetGenericArguments()[0];
						return type2.Equals(type) || (type.Equals(typeof(string)) && type2.Equals(typeof(object)));
					}));
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, Activity> dictionary2 = new Dictionary<string, Activity>();
			try
			{
				dictionary2 = TargetHelper.GetObjectBrowser(item);
			}
			catch (Exception ex)
			{
				//AppLog.Instance.Info(ex.Message);
			}
			foreach (ModelItem current3 in enumerable)
			{
				string text = current3.Properties["Name"].ComputedValue.ToString();
				string value = string.Empty;
				if (dictionary2.ContainsKey(text))
				{
					Activity activity2 = dictionary2[text];
					value = activity2.DisplayName;
				}
				if (type.Equals(typeof(string)) && (current3.Properties["Type"].ComputedValue as Type).Equals(typeof(object)))
				{
					text += ".ToString()";
				}
				dictionary[text] = value;
			}
			return dictionary;
		}
		private static Collection<ModelItem> GetParents(ModelItem modelActivity)
		{
			Collection<ModelItem> collection = new Collection<ModelItem>();
			while (modelActivity.Parent != null && !modelActivity.ItemType.Equals(typeof(ActivityBuilder)))
			{
				collection.Add(modelActivity);
				modelActivity = modelActivity.Parent;
			}
			return collection;
		}
        //internal static bool IsScreenshotActivity(ModelItem ModelItem)
        //{
        //    return ModelItem.ItemType.Equals(typeof(ClickScreenshot)) || ModelItem.ItemType.Equals(typeof(DoubleClickScreenshot)) || ModelItem.ItemType.Equals(typeof(HoverScreenshot)) || ModelItem.ItemType.Equals(typeof(OnClickScreenshot)) || ModelItem.ItemType.Equals(typeof(ScreenshotFound)) || ModelItem.ItemType.BaseType.Equals(typeof(WaitScreenshot)) || ModelItem.ItemType.Equals(typeof(SaveScreenshot)) || ModelItem.ItemType.Equals(typeof(FindAllScreenshotMatches));
        //}
		public static void OpenContextMenu(ContextMenu contextMenu, UIElement sender)
		{
			if (sender != null)
			{
				contextMenu.PlacementTarget = sender;
			}
			contextMenu.IsOpen = true;
		}
	}
}
