using System;
using System.Activities;
using System.Reflection;
namespace FtpActivities
{
	internal static class ExtensionMethods
	{
		public static void Set(this Argument argument, object value)
		{
			ExtensionMethods.GetOwner(argument).RuntimeProperties[ExtensionMethods.GetName(argument)] = value;
		}
		public static T Get<T>(this InArgument<T> argument)
		{
			return (T)((object)ExtensionMethods.GetOwner(argument).RuntimeProperties[ExtensionMethods.GetName(argument)]);
		}
		public static T Get<T>(this OutArgument<T> argument)
		{
			return (T)((object)ExtensionMethods.GetOwner(argument).RuntimeProperties[ExtensionMethods.GetName(argument)]);
		}
		private static BaseActivity GetOwner(Argument argument)
		{
			object value = argument.GetType().GetProperty("RuntimeArgument", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(argument, null);
			return value.GetType().GetProperty("Owner", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(value, null) as BaseActivity;
		}
		private static string GetName(Argument argument)
		{
			object value = argument.GetType().GetProperty("RuntimeArgument", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(argument, null);
			return value.GetType().GetProperty("NameCore", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(value, null) as string;
		}
	}
}
