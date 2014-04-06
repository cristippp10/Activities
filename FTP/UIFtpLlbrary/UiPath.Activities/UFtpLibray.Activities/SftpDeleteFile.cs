using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using System.Collections;
using System.Data;

using FtpActivities.Utilities;
using UiPath.Library;
using FtpActivities.Properties;

using Renci.SshNet.Common;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Classes;
using Renci.SshNet.Sftp;

using System.Net.FtpClient;
using FtpClientFunc;

using FtpActivities.Design;

namespace FtpActivities
{
    [Permission(Permissions.Automation)]
    [Designer(typeof(SftpListFilesDirSubDirDesigner))]
    public class SftpDeleteFile : BaseActivity
    {
        private FtpSessionGen SessionFromContainer;

        //---
        [Category("Target")]
        public InArgument<string> WorkPath
        {
            get;
            set;
        }
        [Category("Logon")]
        public InArgument<string> User
        {
            get;
            set;
        }
        [Category("Logon")]
        public InArgument<string> User_Pass
        {
            get;
            set;
        }
        [Category("Logon")]
        public InArgument<string> Host
        {
            get;
            set;
        }
        [Category("Logon")]
        public InArgument<int> Port
        {
            get;
            set;
        }
        [Category("Logon")]
        public InArgument<bool> Sftp
        {
            get;
            set;
        }
        [Category("Logon")]
        public InArgument<string> SKeyFiles
        {
            get;
            set;
        }
        //---

        [Category("Target")]
        public InArgument<string> RemotePath
        {
            get;
            set;
        }
        
        [Category("Input")]
        public InArgument<FtpSessionGen> FtpSession 
        {
            get;
            set;
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            metadata.AddArgument(new RuntimeArgument("FtpSession", typeof(FtpSessionGen), ArgumentDirection.In));

            metadata.AddArgument(new RuntimeArgument("WorkPath", typeof(string), ArgumentDirection.In)); //, true));
            metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("User_Pass", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Sftp", typeof(bool), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("SKeyFiles", typeof(string), ArgumentDirection.In));
          
            metadata.AddArgument(new RuntimeArgument("RemotePath", typeof(string), ArgumentDirection.In, true));

            base.CacheMetadata(metadata);
        }
        public SftpDeleteFile()
        {
        }

        protected override void ExecuteAsync()
        {
            try
            {
                FtpSessionGen sessiongen = null;
                sessiongen = FtpSession.Get<FtpSessionGen>();
                if (sessiongen == null)
                    if (SessionFromContainer != null)
                        sessiongen = SessionFromContainer;
                
                bool autonomy = false;
                if (sessiongen == null)
                {
                    int modeSftp = 0;
                    if (Sftp.Get<Boolean>() == false)
                        modeSftp = 1;
                    sessiongen = new FtpSessionGen(modeSftp, Host.Get<string>(), User.Get<string>(), User_Pass.Get<string>(), Port.Get<int>(), SKeyFiles.Get<string>());
                    autonomy = true;
                }
                string remotepath = RemotePath.Get<string>();

                if (sessiongen.RemoteFileExists(remotepath))
                {
                    sessiongen.DeleteFile(remotepath);
                }

                if (autonomy)
                    if (sessiongen.IsConnected())
                        sessiongen.Disconnect();
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
           
            foreach (PropertyDescriptor propertyInfo in array)
            {
                if (propertyInfo.PropertyType != null && propertyInfo.PropertyType.FullName.Contains("FtpSessionGen"))//("SftpClient"))
                {
                    object obj = propertyInfo.GetValue(cx.DataContext);
                    //SessionFromContainer = (FtpSessionGen)obj;
                    FtpSessionGen sess = (FtpSessionGen)obj;
                    if (sess != null)
                        if (sess.IsConnected())
                        {
                            SessionFromContainer = (FtpSessionGen)obj;
                            this.FtpSession.Set((FtpSessionGen)obj);
                            break;
                        }
                    this.FtpSession.Set((FtpSessionGen)obj);
                }
            }

            return base.BeginExecute(context, callback, state);
        }
    }
}
