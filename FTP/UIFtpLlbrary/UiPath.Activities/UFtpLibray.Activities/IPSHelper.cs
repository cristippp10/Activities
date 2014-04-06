using System;
using System.Activities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
namespace FtpActivities
{
	internal class IPSHelper
	{
		public static void CacheMetadataHelper(ActivityMetadata metadata, InArgument<System.Collections.ObjectModel.Collection<PSObject>> input, OutArgument<System.Collections.ObjectModel.Collection<ErrorRecord>> errors, InArgument<string> commandText, string typeName, string displayName, System.Collections.Generic.IDictionary<string, Argument> variables, System.Collections.Generic.IDictionary<string, InArgument> parameters, System.Collections.Generic.IDictionary<string, Argument> childVariables, System.Collections.Generic.IDictionary<string, InArgument> childParameters)
		{
			childVariables.Clear();
			childParameters.Clear();
			RuntimeArgument argument = new RuntimeArgument("Input", typeof(System.Collections.ObjectModel.Collection<PSObject>), ArgumentDirection.In);
			metadata.Bind(input, argument);
			metadata.AddArgument(argument);
			RuntimeArgument argument2 = new RuntimeArgument("Errors", typeof(System.Collections.ObjectModel.Collection<ErrorRecord>), ArgumentDirection.Out);
			metadata.Bind(errors, argument2);
			metadata.AddArgument(argument2);
			if (commandText == null)
			{
				metadata.AddValidationError(string.Format("CommandText must be set before InvokePowerShell activity '{0}' can be used.", displayName));
			}
			foreach (System.Collections.Generic.KeyValuePair<string, Argument> current in variables)
			{
				string key = current.Key;
				Argument value = current.Value;
				RuntimeArgument argument3 = new RuntimeArgument(key, value.ArgumentType, value.Direction, true);
				metadata.Bind(value, argument3);
				metadata.AddArgument(argument3);
				Argument.Create(value.ArgumentType, value.Direction);
				childVariables.Add(key, Argument.CreateReference(value, key));
			}
			foreach (System.Collections.Generic.KeyValuePair<string, InArgument> current2 in parameters)
			{
				string key2 = current2.Key;
				InArgument value2 = current2.Value;
				RuntimeArgument argument4;
				if (value2.ArgumentType == typeof(bool))
				{
					argument4 = new RuntimeArgument(key2, value2.ArgumentType, value2.Direction, false);
				}
				else
				{
					argument4 = new RuntimeArgument(key2, value2.ArgumentType, value2.Direction, true);
				}
				metadata.Bind(value2, argument4);
				metadata.AddArgument(argument4);
				Argument.Create(value2.ArgumentType, value2.Direction);
				childParameters.Add(key2, Argument.CreateReference(value2, key2) as InArgument);
			}
		}
	}
}
