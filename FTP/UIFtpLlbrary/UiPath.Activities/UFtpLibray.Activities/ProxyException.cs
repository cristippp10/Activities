using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Text;
namespace FtpActivities
{
	public class ProxyException : System.ApplicationException
	{
		private System.Collections.Generic.IEnumerable<MetadataConversionError> ImportErrors;
		private System.Collections.Generic.IEnumerable<MetadataConversionError> CodegenErrors;
		private System.Collections.Generic.IEnumerable<CompilerError> CompilerErrors;
		public System.Collections.Generic.IEnumerable<MetadataConversionError> MetadataImportErrors
		{
			get
			{
				return this.ImportErrors;
			}
			internal set
			{
				this.ImportErrors = value;
			}
		}
		public System.Collections.Generic.IEnumerable<MetadataConversionError> CodeGenerationErrors
		{
			get
			{
				return this.CodegenErrors;
			}
			internal set
			{
				this.CodegenErrors = value;
			}
		}
		public System.Collections.Generic.IEnumerable<CompilerError> CompilationErrors
		{
			get
			{
				return this.CompilerErrors;
			}
			internal set
			{
				this.CompilerErrors = value;
			}
		}
		public ProxyException(string message) : base(message)
		{
		}
		public ProxyException(string message, System.Exception innerException) : base(message, innerException)
		{
		}
		public override string ToString()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			if (this.MetadataImportErrors != null)
			{
				stringBuilder.AppendLine("Metadata Import Errors:");
				stringBuilder.AppendLine(DynamicProxyFactory.ToString(this.MetadataImportErrors));
			}
			if (this.CodeGenerationErrors != null)
			{
				stringBuilder.AppendLine("Code Generation Errors:");
				stringBuilder.AppendLine(DynamicProxyFactory.ToString(this.CodeGenerationErrors));
			}
			if (this.CompilationErrors != null)
			{
				stringBuilder.AppendLine("Compilation Errors:");
				stringBuilder.AppendLine(DynamicProxyFactory.ToString(this.CompilationErrors));
			}
			return stringBuilder.ToString();
		}
	}
}
