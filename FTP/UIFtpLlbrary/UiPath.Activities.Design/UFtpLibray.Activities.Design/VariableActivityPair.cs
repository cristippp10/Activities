using System;
namespace FtpActivities.Design
{
	public class VariableActivityPair
	{
		public string VariableName
		{
			get;
			set;
		}
		public string ActivityName
		{
			get;
			set;
		}
		public VariableActivityPair(string variableName)
		{
			this.VariableName = variableName;
		}
		public VariableActivityPair(string variableName, string activityName)
		{
			this.VariableName = variableName;
			this.ActivityName = activityName;
		}
		public override string ToString()
		{
			if (string.IsNullOrWhiteSpace(this.ActivityName))
			{
				return this.VariableName;
			}
			string text = this.ActivityName;
			int num = 32;
			if (this.ActivityName.Length > num)
			{
				text = text.Remove(num);
				text += "...";
			}
			return this.VariableName + " (" + text + ")";
		}
	}
}
