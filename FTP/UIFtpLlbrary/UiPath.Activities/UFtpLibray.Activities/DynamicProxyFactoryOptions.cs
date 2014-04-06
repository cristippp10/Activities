using System;
using System.Text;
namespace FtpActivities
{
	public class DynamicProxyFactoryOptions
	{
		public enum LanguageOptions
		{
			CS,
			VB
		}
		public enum FormatModeOptions
		{
			Auto,
			XmlSerializer,
			DataContractSerializer
		}
		private DynamicProxyFactoryOptions.LanguageOptions lang;
		private DynamicProxyFactoryOptions.FormatModeOptions mode;
		private ProxyCodeModifier codeModifier;
		public DynamicProxyFactoryOptions.LanguageOptions Language
		{
			get
			{
				return this.lang;
			}
			set
			{
				this.lang = value;
			}
		}
		public DynamicProxyFactoryOptions.FormatModeOptions FormatMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}
		public ProxyCodeModifier CodeModifier
		{
			get
			{
				return this.codeModifier;
			}
			set
			{
				this.codeModifier = value;
			}
		}
		public DynamicProxyFactoryOptions()
		{
			this.lang = DynamicProxyFactoryOptions.LanguageOptions.CS;
			this.mode = DynamicProxyFactoryOptions.FormatModeOptions.Auto;
			this.codeModifier = null;
		}
		public override string ToString()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("DynamicProxyFactoryOptions[");
			stringBuilder.Append("Language=" + this.Language);
			stringBuilder.Append(",FormatMode=" + this.FormatMode);
			stringBuilder.Append(",CodeModifier=" + this.CodeModifier);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
