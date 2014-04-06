using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace FtpActivities
{
	public class DynamicProxy : DynamicObject
	{
		public System.Type ProxyType
		{
			get
			{
				return base.ObjectType;
			}
		}
		public object Proxy
		{
			get
			{
				return base.ObjectInstance;
			}
		}
		public DynamicProxy(System.Type proxyType, Binding binding, EndpointAddress address) : base(proxyType)
		{
			base.CallConstructor(new System.Type[]
			{
				typeof(Binding),
				typeof(EndpointAddress)
			}, new object[]
			{
				binding,
				address
			});
		}
		public void Close()
		{
			base.CallMethod("Close", new object[0]);
		}
	}
}
