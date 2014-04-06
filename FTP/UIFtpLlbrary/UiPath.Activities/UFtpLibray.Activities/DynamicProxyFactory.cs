using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace FtpActivities
{
	public class DynamicProxyFactory
	{
		internal const string DefaultNamespace = "http://tempuri.org/";
		private string wsdlUri;
		private DynamicProxyFactoryOptions options;
		private CodeCompileUnit codeCompileUnit;
		private CodeDomProvider codeDomProvider;
		private ServiceContractGenerator contractGenerator;
		private System.Collections.ObjectModel.Collection<MetadataSection> metadataCollection;
		private System.Collections.Generic.IEnumerable<System.ServiceModel.Channels.Binding> bindings;
		private System.Collections.Generic.IEnumerable<ContractDescription> contracts;
		private ServiceEndpointCollection endpoints;
		private System.Collections.Generic.IEnumerable<MetadataConversionError> importWarnings;
		private System.Collections.Generic.IEnumerable<MetadataConversionError> codegenWarnings;
		private System.Collections.Generic.IEnumerable<CompilerError> compilerWarnings;
		public System.Reflection.Assembly proxyAssembly;
		private string proxyCode;
		public System.Collections.Generic.IEnumerable<MetadataSection> Metadata
		{
			get
			{
				return this.metadataCollection;
			}
		}
		public System.Collections.Generic.IEnumerable<System.ServiceModel.Channels.Binding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}
		public System.Collections.Generic.IEnumerable<ContractDescription> Contracts
		{
			get
			{
				return this.contracts;
			}
		}
		public System.Collections.Generic.IEnumerable<ServiceEndpoint> Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}
		public System.Reflection.Assembly ProxyAssembly
		{
			get
			{
				return this.proxyAssembly;
			}
		}
		public string ProxyCode
		{
			get
			{
				return this.proxyCode;
			}
		}
		public System.Collections.Generic.IEnumerable<MetadataConversionError> MetadataImportWarnings
		{
			get
			{
				return this.importWarnings;
			}
		}
		public System.Collections.Generic.IEnumerable<MetadataConversionError> CodeGenerationWarnings
		{
			get
			{
				return this.codegenWarnings;
			}
		}
		public System.Collections.Generic.IEnumerable<CompilerError> CompilationWarnings
		{
			get
			{
				return this.compilerWarnings;
			}
		}
		public DynamicProxyFactory(string wsdlUri, DynamicProxyFactoryOptions options)
		{
			if (wsdlUri == null)
			{
				throw new System.ArgumentNullException("wsdlUri");
			}
			if (options == null)
			{
				throw new System.ArgumentNullException("options");
			}
			this.wsdlUri = wsdlUri;
			this.options = options;
			this.DownloadMetadata();
			this.ImportMetadata();
			this.CreateProxy();
			this.WriteCode();
			this.CompileProxy();
		}
		public DynamicProxyFactory(string wsdlUri) : this(wsdlUri, new DynamicProxyFactoryOptions())
		{
		}
		private void DownloadMetadata()
		{
			new EndpointAddress(this.wsdlUri);
			DiscoveryClientProtocol discoveryClientProtocol = new DiscoveryClientProtocol();
			discoveryClientProtocol.Credentials = CredentialCache.DefaultNetworkCredentials;
			discoveryClientProtocol.AllowAutoRedirect = true;
			discoveryClientProtocol.DiscoverAny(this.wsdlUri);
			discoveryClientProtocol.ResolveAll();
			System.Collections.ObjectModel.Collection<MetadataSection> results = new System.Collections.ObjectModel.Collection<MetadataSection>();
			foreach (object current in discoveryClientProtocol.Documents.Values)
			{
				this.AddDocumentToResults(current, results);
			}
			this.metadataCollection = results;
		}
		private void AddDocumentToResults(object document, System.Collections.ObjectModel.Collection<MetadataSection> results)
		{
			System.Web.Services.Description.ServiceDescription serviceDescription = document as System.Web.Services.Description.ServiceDescription;
			XmlSchema xmlSchema = document as XmlSchema;
			XmlElement xmlElement = document as XmlElement;
			if (serviceDescription != null)
			{
				results.Add(MetadataSection.CreateFromServiceDescription(serviceDescription));
				return;
			}
			if (xmlSchema != null)
			{
				results.Add(MetadataSection.CreateFromSchema(xmlSchema));
				return;
			}
			if (xmlElement != null && xmlElement.LocalName == "Policy")
			{
				results.Add(MetadataSection.CreateFromPolicy(xmlElement, null));
				return;
			}
			results.Add(new MetadataSection
			{
				Metadata = document
			});
		}
		private void ImportMetadata()
		{
			this.codeCompileUnit = new CodeCompileUnit();
			this.CreateCodeDomProvider();
			WsdlImporter wsdlImporter = new WsdlImporter(new MetadataSet(this.metadataCollection));
			this.AddStateForDataContractSerializerImport(wsdlImporter);
			this.AddStateForXmlSerializerImport(wsdlImporter);
			this.bindings = wsdlImporter.ImportAllBindings();
			this.contracts = wsdlImporter.ImportAllContracts();
			this.endpoints = wsdlImporter.ImportAllEndpoints();
			this.importWarnings = wsdlImporter.Errors;
		}
		private void AddStateForXmlSerializerImport(WsdlImporter importer)
		{
			XmlSerializerImportOptions xmlSerializerImportOptions = new XmlSerializerImportOptions(this.codeCompileUnit);
			xmlSerializerImportOptions.CodeProvider = this.codeDomProvider;
			xmlSerializerImportOptions.WebReferenceOptions = new WebReferenceOptions();
			xmlSerializerImportOptions.WebReferenceOptions.CodeGenerationOptions = (CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateOrder);
			xmlSerializerImportOptions.WebReferenceOptions.SchemaImporterExtensions.Add(typeof(DataSetSchemaImporterExtension).AssemblyQualifiedName);
			importer.State.Add(typeof(XmlSerializerImportOptions), xmlSerializerImportOptions);
		}
		private void AddStateForDataContractSerializerImport(WsdlImporter importer)
		{
			XsdDataContractImporter xsdDataContractImporter = new XsdDataContractImporter(this.codeCompileUnit);
			xsdDataContractImporter.Options = new ImportOptions();
			xsdDataContractImporter.Options.ImportXmlType = (this.options.FormatMode == DynamicProxyFactoryOptions.FormatModeOptions.DataContractSerializer);
			xsdDataContractImporter.Options.CodeProvider = this.codeDomProvider;
			importer.State.Add(typeof(XsdDataContractImporter), xsdDataContractImporter);
			foreach (IWsdlImportExtension current in importer.WsdlImportExtensions)
			{
				DataContractSerializerMessageContractImporter dataContractSerializerMessageContractImporter = current as DataContractSerializerMessageContractImporter;
				if (dataContractSerializerMessageContractImporter != null)
				{
					dataContractSerializerMessageContractImporter.Enabled = (this.options.FormatMode != DynamicProxyFactoryOptions.FormatModeOptions.XmlSerializer);
				}
			}
		}
		private void CreateProxy()
		{
			this.CreateServiceContractGenerator();
			foreach (ContractDescription current in this.contracts)
			{
				this.contractGenerator.GenerateServiceContractType(current);
			}
			bool flag = true;
			this.codegenWarnings = this.contractGenerator.Errors;
			if (this.codegenWarnings != null)
			{
				foreach (MetadataConversionError current2 in this.codegenWarnings)
				{
					if (!current2.IsWarning)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				throw new ProxyException("There was an error in generating the proxy code.")
				{
					CodeGenerationErrors = this.codegenWarnings
				};
			}
		}
		private void CompileProxy()
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			this.AddAssemblyReference(typeof(ServiceContractAttribute).Assembly, compilerParameters.ReferencedAssemblies);
			this.AddAssemblyReference(typeof(System.Web.Services.Description.ServiceDescription).Assembly, compilerParameters.ReferencedAssemblies);
			this.AddAssemblyReference(typeof(DataContractAttribute).Assembly, compilerParameters.ReferencedAssemblies);
			this.AddAssemblyReference(typeof(XmlElement).Assembly, compilerParameters.ReferencedAssemblies);
			this.AddAssemblyReference(typeof(Uri).Assembly, compilerParameters.ReferencedAssemblies);
			this.AddAssemblyReference(typeof(DataSet).Assembly, compilerParameters.ReferencedAssemblies);
			CompilerResults compilerResults = this.codeDomProvider.CompileAssemblyFromSource(compilerParameters, new string[]
			{
				this.proxyCode
			});
			if (compilerResults.Errors != null && compilerResults.Errors.HasErrors)
			{
				throw new ProxyException("There was an error in compiling the proxy code.")
				{
					CompilationErrors = DynamicProxyFactory.ToEnumerable(compilerResults.Errors)
				};
			}
			this.compilerWarnings = DynamicProxyFactory.ToEnumerable(compilerResults.Errors);
			this.proxyAssembly = System.Reflection.Assembly.LoadFile(compilerResults.PathToAssembly);
		}
		private void WriteCode()
		{
			using (System.IO.StringWriter stringWriter = new System.IO.StringWriter())
			{
				CodeGeneratorOptions codeGeneratorOptions = new CodeGeneratorOptions();
				codeGeneratorOptions.BracingStyle = "C";
				this.codeDomProvider.GenerateCodeFromCompileUnit(this.codeCompileUnit, stringWriter, codeGeneratorOptions);
				stringWriter.Flush();
				this.proxyCode = stringWriter.ToString();
			}
			if (this.options.CodeModifier != null)
			{
				this.proxyCode = this.options.CodeModifier(this.proxyCode);
			}
		}
		private void AddAssemblyReference(System.Reflection.Assembly referencedAssembly, StringCollection refAssemblies)
		{
			string fullPath = System.IO.Path.GetFullPath(referencedAssembly.Location);
			string fileName = System.IO.Path.GetFileName(fullPath);
			if (!refAssemblies.Contains(fileName) && !refAssemblies.Contains(fullPath))
			{
				refAssemblies.Add(fullPath);
			}
		}
		public ServiceEndpoint GetEndpoint(string contractName)
		{
			return this.GetEndpoint(contractName, null);
		}
		public ServiceEndpoint GetEndpoint(string contractName, string contractNamespace)
		{
			ServiceEndpoint serviceEndpoint = null;
			foreach (ServiceEndpoint current in this.Endpoints)
			{
				if (this.ContractNameMatch(current.Contract, contractName) && this.ContractNsMatch(current.Contract, contractNamespace))
				{
					serviceEndpoint = current;
					break;
				}
			}
			if (serviceEndpoint == null)
			{
				throw new System.ArgumentException(string.Format("The endpoint associated with contract {1}:{0} is not found.", contractName, contractNamespace));
			}
			return serviceEndpoint;
		}
		public bool HasEndpoint(string contractName, string contractNamespace = null)
		{
			foreach (ServiceEndpoint current in this.Endpoints)
			{
				if (this.ContractNameMatch(current.Contract, contractName) && this.ContractNsMatch(current.Contract, contractNamespace))
				{
					return true;
				}
			}
			return false;
		}
		private bool ContractNameMatch(ContractDescription cDesc, string name)
		{
			return string.Compare(cDesc.Name, name, true) == 0;
		}
		private bool ContractNsMatch(ContractDescription cDesc, string ns)
		{
			return ns == null || string.Compare(cDesc.Namespace, ns, true) == 0;
		}
		public DynamicProxy CreateProxy(string contractName)
		{
			return this.CreateProxy(contractName, null);
		}
		public DynamicProxy CreateProxy(string contractName, string contractNamespace)
		{
			ServiceEndpoint endpoint = this.GetEndpoint(contractName, contractNamespace);
			return this.CreateProxy(endpoint);
		}
		public DynamicProxy CreateProxy(ServiceEndpoint endpoint)
		{
			System.Type contractType = this.GetContractType(endpoint.Contract.Name, endpoint.Contract.Namespace);
			System.Type proxyType = this.GetProxyType(contractType);
			return new DynamicProxy(proxyType, endpoint.Binding, endpoint.Address);
		}
		private System.Type GetContractType(string contractName, string contractNamespace)
		{
			System.Type[] types = this.proxyAssembly.GetTypes();
			System.Type type = null;
			System.Type[] array = types;
			for (int i = 0; i < array.Length; i++)
			{
				System.Type type2 = array[i];
				if (type2.IsInterface)
				{
					object[] customAttributes = type2.GetCustomAttributes(typeof(ServiceContractAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						ServiceContractAttribute serviceContractAttribute = (ServiceContractAttribute)customAttributes[0];
						XmlQualifiedName contractName2 = DynamicProxyFactory.GetContractName(type2, serviceContractAttribute.Name, serviceContractAttribute.Namespace);
						if (string.Compare(contractName2.Name, contractName, true) == 0 && string.Compare(contractName2.Namespace, contractNamespace, true) == 0)
						{
							type = type2;
							break;
						}
					}
				}
			}
			if (type == null)
			{
				throw new System.ArgumentException("The specified contract is not found in the proxy assembly.");
			}
			return type;
		}
		internal static XmlQualifiedName GetContractName(System.Type contractType, string name, string ns)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = contractType.Name;
			}
			if (ns == null)
			{
				ns = "http://tempuri.org/";
			}
			else
			{
				ns = Uri.EscapeUriString(ns);
			}
			return new XmlQualifiedName(name, ns);
		}
		private System.Type GetProxyType(System.Type contractType)
		{
			System.Type c = typeof(ClientBase<>).MakeGenericType(new System.Type[]
			{
				contractType
			});
			System.Type[] types = this.ProxyAssembly.GetTypes();
			System.Type type = null;
			System.Type[] array = types;
			for (int i = 0; i < array.Length; i++)
			{
				System.Type type2 = array[i];
				if (type2.IsClass && contractType.IsAssignableFrom(type2) && type2.IsSubclassOf(c))
				{
					type = type2;
					break;
				}
			}
			if (type == null)
			{
				throw new ProxyException(string.Format("The proxy that implements the service contract {0} is not found.", contractType.FullName));
			}
			return type;
		}
		private void CreateCodeDomProvider()
		{
			this.codeDomProvider = CodeDomProvider.CreateProvider(this.options.Language.ToString());
		}
		private void CreateServiceContractGenerator()
		{
			this.contractGenerator = new ServiceContractGenerator(this.codeCompileUnit);
			this.contractGenerator.Options |= ServiceContractGenerationOptions.ClientClass;
		}
		public static string ToString(System.Collections.Generic.IEnumerable<MetadataConversionError> importErrors)
		{
			if (importErrors == null)
			{
				return null;
			}
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (MetadataConversionError current in importErrors)
			{
				if (current.IsWarning)
				{
					stringBuilder.AppendLine("Warning : " + current.Message);
				}
				else
				{
					stringBuilder.AppendLine("Error : " + current.Message);
				}
			}
			return stringBuilder.ToString();
		}
		public static string ToString(System.Collections.Generic.IEnumerable<CompilerError> compilerErrors)
		{
			if (compilerErrors == null)
			{
				return null;
			}
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (CompilerError current in compilerErrors)
			{
				stringBuilder.AppendLine(current.ToString());
			}
			return stringBuilder.ToString();
		}
		private static System.Collections.Generic.IEnumerable<CompilerError> ToEnumerable(CompilerErrorCollection collection)
		{
			if (collection == null)
			{
				return null;
			}
			System.Collections.Generic.List<CompilerError> list = new System.Collections.Generic.List<CompilerError>();
			foreach (CompilerError item in collection)
			{
				list.Add(item);
			}
			return list;
		}
	}
}
