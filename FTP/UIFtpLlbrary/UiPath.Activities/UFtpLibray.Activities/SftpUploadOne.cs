using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;

using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Collections;

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
    [Designer(typeof(SftpUploadOneDesigner))]
    public class SftpUploadOne : BaseActivity
    {
        private FtpSessionGen SessionFromContainer;

        [Category("Target")]
        public InArgument<string> LocalPath
        {
            get;
            set;
        }
        
        [Category("Target")]
        public InArgument<string> RemotePath
        {
            get;
            set;
        }

        
        [Category("Input")]
        public InArgument<FtpSessionGen> FtpSession //Renci.SshNet.SftpClient
        {
            get;
            set;
        }

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

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            metadata.AddArgument(new RuntimeArgument("FtpSession", typeof(FtpSessionGen), ArgumentDirection.In));

            metadata.AddArgument(new RuntimeArgument("LocalPath", typeof(string), ArgumentDirection.In, true));
            metadata.AddArgument(new RuntimeArgument("RemotePath", typeof(string), ArgumentDirection.In, true));

            metadata.AddArgument(new RuntimeArgument("WorkPath", typeof(string), ArgumentDirection.In)); //, true));
            metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("User_Pass", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Sftp", typeof(bool), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("SKeyFiles", typeof(string), ArgumentDirection.In));

            base.CacheMetadata(metadata);
        }
        public SftpUploadOne()
        {
        }

        protected override void ExecuteAsync()
        {
            try
            {
                //Renci.SshNet.SftpClient sftpClient = null;
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

                string localpath = LocalPath.Get<string>(); 
                string remotepath = RemotePath.Get<string>();

                //string[] paths = null;
                ArrayList listFilesNotUp= new ArrayList();
                ArrayList listFilesUp = new ArrayList();

                if (localpath.Contains("|"))
                {
                    //paths = localpath.Split('|');

                    //if (sessiongen.RemoteDirectoryExists(remotepath))
                    //    foreach (string lpath in paths)
                    //    {
                    //        if (Directory.Exists(lpath))
                    //        {
                    //            sessiongen.UploadMultiple(lpath, remotepath);
                    //            sessiongen.VerifyUpload(remotepath, lpath, ref listFilesNotUp, ref listFilesUp);
                    //        }
                    //        if (File.Exists(lpath) && !Directory.Exists(lpath))
                    //        {
                    //            sessiongen.UploadOne(remotepath, lpath);
                    //            string rpath = (remotepath == "") ? lpath.Substring(lpath.LastIndexOf(@"\") + 1) : remotepath + "/" + lpath.Substring(lpath.LastIndexOf(@"\") + 1);
                    //            if (sessiongen.RemoteFileExists(rpath))
                    //                listFilesUp.Add(new FileInfo(lpath));
                    //            else
                    //                listFilesNotUp.Add(new FileInfo(lpath));
                    //        }
                    //    }
                }
                else
                    if (sessiongen.RemoteDirectoryExists(remotepath))
                    {
                        if (Directory.Exists(localpath))
                        {
                            //sessiongen.UploadMultiple(localpath, remotepath);
                            //sessiongen.VerifyUpload(remotepath, localpath, ref listFilesNotUp, ref listFilesUp);
                        }

                        string dircurrent = Directory.GetCurrentDirectory();
                        if (!localpath.Contains(@":\"))
                        { 
                            if (localpath[0] == '\\')
                                localpath = dircurrent + localpath;
                            else
                                localpath = dircurrent + "\\" + localpath;
                        }

                        if (File.Exists(localpath) && !Directory.Exists(localpath))                            
                        {
                            sessiongen.UploadOne(remotepath, localpath);
                            string rpath = (remotepath == "") ? localpath.Substring(localpath.LastIndexOf(@"\") + 1) : remotepath + "/" + localpath.Substring(localpath.LastIndexOf(@"\") + 1);
                            if (sessiongen.RemoteFileExists(rpath))
                                listFilesUp.Add(new FileInfo(localpath));
                            else
                                listFilesNotUp.Add(new FileInfo(localpath));
                         }
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
