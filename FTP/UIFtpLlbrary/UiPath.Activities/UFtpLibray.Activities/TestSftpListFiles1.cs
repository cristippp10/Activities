
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using FtpActivities.Utilities;
using UiPath.Library;

using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using FtpActivities.Properties;
using FtpActivities.Utilities;


using Renci.SshNet.Common;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Classes;
using Renci.SshNet.Sftp;

namespace FtpActivities
{
	[Permission(Permissions.Automation)]
	public class TestSftpListFiles1 : BaseActivity
	{
        private Renci.SshNet.SftpClient Sftpclientfromcontainer;
        //[Category("Logon")]
        //public InArgument<string> User
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<string> Password
        //{
        //    get;
        //    set;
        //}
        //[Category("Host")]
        //public InArgument<string> Host
        //{
        //    get;
        //    set;
        //}
        //[Category("Host")]
        //public InArgument<int> Port
        //{
        //    get;
        //    set;
        //}
        //[Category("Host")]
        //public bool EnableSSL
        //{
        //    get;
        //    set;
        //}
        //[Category("Input")]
        //public InArgument<int> Top
        //{
        //    get;
        //    set;
        //}

        [Category("Input")]
        public InArgument<Renci.SshNet.SftpClient> Client //Renci.SshNet.SftpClient
        {
            get;
            set;
        }
       
		[Category("Output")]
		public OutArgument<System.Collections.Generic.List<SftpFile>> Files
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
            //metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In, true));
            //metadata.AddArgument(new RuntimeArgument("Password", typeof(string), ArgumentDirection.In, true));
            //metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In, true));
            //metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In, true));
            //metadata.AddArgument(new RuntimeArgument("Top", typeof(int), ArgumentDirection.In, false));
            metadata.AddArgument(new RuntimeArgument("Client", typeof(Renci.SshNet.SftpClient), ArgumentDirection.In )); //, true)); // Renci.SshNet.SftpClient

            metadata.AddArgument(new RuntimeArgument("Files", typeof(System.Collections.Generic.List<SftpFile>), ArgumentDirection.Out));
			
            base.CacheMetadata(metadata);
		}
        public TestSftpListFiles1()
		{
            //this.Protocol = EmailProtocol.IMAP;
			//this.EnableSSL = true;
		}
		protected override void ExecuteAsync()
		{
			try
			{
                //ActivityContext ctx;
                
                //new InArgument<Renci.SshNet.SftpClient>((ActivityContext ctx) => this.Client.Get(ctx));
                Renci.SshNet.SftpClient sftpClient = null;
                sftpClient = Client.Get<Renci.SshNet.SftpClient>();

                if (sftpClient == null) 
                    if (Sftpclientfromcontainer != null)
                        sftpClient =  Sftpclientfromcontainer;// Client.Get<Renci.SshNet.SftpClient>(); 
                if (!sftpClient.IsConnected)
                    sftpClient.Connect();
                //Renci.SshNet.ConnectionInfo coinfo = sftpClient.ConnectionInfo;
                IEnumerable<SftpFile> ief = sftpClient.ListDirectory("\\");

                System.Collections.Generic.List<SftpFile> list = new System.Collections.Generic.List<SftpFile>();
                list = ief as System.Collections.Generic.List<SftpFile>;
                this.Files.Set(list);
                //SftpFile sf = sftpClient.Get("\\Test1.txt");
                //string[] strLines = sftpClient.ReadAllLines(sf.FullName);

                //sftpClient.Disconnect();

              
            }
			catch (System.Exception ex)
			{
				base.HandleException(ex);
			}
		}

        protected override System.IAsyncResult BeginExecute(NativeActivityContext context, System.AsyncCallback callback, object state)
        {
            NativeActivityContext cx = context;


            PropertyDescriptorCollection array = cx.DataContext.GetProperties(); //base.GetType().GetProperties();
            //System.Reflection.PropertyInfo[] array = propDesCol; // properties;
            foreach (PropertyDescriptor propertyInfo in array)
            {
                //propertyInfo = array[i];
                if (propertyInfo.PropertyType != null && propertyInfo.PropertyType.FullName.Contains("SftpClient"))
                {   
                    object obj = propertyInfo.GetValue(cx.DataContext);
                    Sftpclientfromcontainer = (Renci.SshNet.SftpClient)obj;
                    //this.Client.Set((Renci.SshNet.SftpClient)obj);
                }
            }
            

                //CallSite<Func<CallSite, object, bool>> p__Site1 = null;
                //CallSite<Func<CallSite, object, object, object>> p__Site2 = null;
                //System.Reflection.PropertyInfo[] properties = base.GetType().GetProperties();
                //System.Reflection.PropertyInfo[] array = properties;
                //for (int i = 0; i < array.Length; i++)
                //{
                //    System.Reflection.PropertyInfo propertyInfo = array[i];
                //    if (propertyInfo.PropertyType.GetType() != null ) //&& propertyInfo.PropertyType.BaseType.Equals(typeof(InArgument)))
                //    {
                //        object obj = propertyInfo.GetValue(this, null);
                //        if (p__Site1 == null)
                //        {
                //            p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BaseActivity), new CSharpArgumentInfo[]
                //            {
                //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                //            }));
                //        }
                //        Func<CallSite, object, bool> arg_101_0 = p__Site1.Target;
                //        CallSite arg_101_1 = p__Site1;
                //        if (p__Site2 == null)
                //        {
                //            p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(BaseActivity), new CSharpArgumentInfo[]
                //            {
                //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                //                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
                //            }));
                //        }
                //        if (!arg_101_0(arg_101_1, p__Site2.Target(p__Site2, obj, null)))
                //        {
                //            obj = (obj as InArgument).Get(context);
                //            this.RuntimeProperties[propertyInfo.Name] = obj;
                //        }
                //    }
                //}

            return base.BeginExecute(context, callback, state);
        }
	}
}
