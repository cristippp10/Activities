using System;
using System.Activities;
using System.ComponentModel;
namespace FtpActivities
{
	public class SendHotkey : BaseType
	{
		[Category("Key Modifier")]
		public bool Alt
		{
			get;
			set;
		}
		[Category("Key Modifier")]
		public bool Ctrl
		{
			get;
			set;
		}
		[Category("Key Modifier")]
		public bool Shift
		{
			get;
			set;
		}
		[Category("Key Modifier")]
		public bool Win
		{
			get;
			set;
		}
		[Category("Input")]
		public string Key
		{
			get;
			set;
		}
		[Browsable(false)]
		public bool SpecialKey
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			if (string.IsNullOrEmpty(this.Key))
			{
				metadata.AddValidationError("Value for required property 'Key' was not supplied.");
			}
			base.CacheMetadata(metadata);
		}
		protected override void ExecuteAsync()
		{
			try
			{
				string text = this.ComposeText();
				base.TypeText(text);
			}
			catch (System.Exception ex)
			{
				base.HandleException(ex);
			}
		}
		private string ComposeText()
		{
			string text = "";
			string text2 = "";
			if (this.Alt)
			{
				text += "d(alt)";
				text2 = "u(alt)" + text2;
			}
			if (this.Ctrl)
			{
				text += "d(ctrl)";
				text2 = "u(ctrl)" + text2;
			}
			if (this.Shift)
			{
				text += "d(shift)";
				text2 = "u(shift)" + text2;
			}
			if (this.Win)
			{
				text += "d(lwin)";
				text2 = "u(lwin)" + text2;
			}
			string text3 = this.Key;
			if (this.SpecialKey)
			{
				text3 = "[k(" + text3 + ")]";
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				text3 = string.Concat(new string[]
				{
					"[",
					text,
					"]",
					text3,
					"[",
					text2,
					"]"
				});
			}
			return text3;
		}
	}
}
