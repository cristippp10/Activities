using System;
using System.Reflection;
namespace FtpActivities
{
	public class DynamicObject
	{
		private System.Type objType;
		private object objectInstance;
		private System.Reflection.BindingFlags CommonBindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public;
		public System.Type ObjectType
		{
			get
			{
				return this.objType;
			}
		}
		public object ObjectInstance
		{
			get
			{
				return this.objectInstance;
			}
		}
		public System.Reflection.BindingFlags BindingFlags
		{
			get
			{
				return this.CommonBindingFlags;
			}
			set
			{
				this.CommonBindingFlags = value;
			}
		}
		public DynamicObject(object obj)
		{
			if (obj == null)
			{
				throw new System.ArgumentNullException("objectInstance");
			}
			this.objectInstance = obj;
			this.objType = obj.GetType();
		}
		public DynamicObject(System.Type objType)
		{
			if (objType == null)
			{
				throw new System.ArgumentNullException("objType");
			}
			this.objType = objType;
		}
		public void CallConstructor()
		{
			this.CallConstructor(new System.Type[0], new object[0]);
		}
		public void CallConstructor(System.Type[] paramTypes, object[] paramValues)
		{
			System.Reflection.ConstructorInfo constructor = this.objType.GetConstructor(paramTypes);
			if (constructor == null)
			{
				throw new ProxyException("The constructor matching the specified parameter types is not found.");
			}
			this.objectInstance = constructor.Invoke(paramValues);
		}
		public object GetProperty(string property)
		{
			return this.GetMember(property, System.Reflection.BindingFlags.GetProperty);
		}
		public object SetProperty(string property, object value)
		{
			return this.SetMember(property, value, System.Reflection.BindingFlags.SetProperty);
		}
		public object GetField(string field)
		{
			return this.GetMember(field, System.Reflection.BindingFlags.GetField | this.CommonBindingFlags);
		}
		public object SetField(string field, object value)
		{
			return this.SetMember(field, value, System.Reflection.BindingFlags.SetField);
		}
		private object GetMember(string memeberName, System.Reflection.BindingFlags flags)
		{
			return this.objType.InvokeMember(memeberName, flags | this.CommonBindingFlags, null, this.objectInstance, null);
		}
		private object SetMember(string memeberName, object value, System.Reflection.BindingFlags flags)
		{
			return this.objType.InvokeMember(memeberName, flags | this.CommonBindingFlags, null, this.objectInstance, new object[]
			{
				value
			});
		}
		public object CallMethod(string method, params object[] parameters)
		{
			return this.objType.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod | this.CommonBindingFlags, null, this.objectInstance, parameters);
		}
		public object CallMethod(string method, System.Type[] types, object[] parameters)
		{
			if (types.Length != parameters.Length)
			{
				throw new System.ArgumentException("The type for each parameter values must be specified.");
			}
			System.Reflection.MethodInfo method2 = this.objType.GetMethod(method, types);
			if (method2 == null)
			{
				throw new System.ApplicationException(string.Format("The method {0} is not found.", method));
			}
			return method2.Invoke(this.objectInstance, this.CommonBindingFlags, null, parameters, null);
		}
	}
}
