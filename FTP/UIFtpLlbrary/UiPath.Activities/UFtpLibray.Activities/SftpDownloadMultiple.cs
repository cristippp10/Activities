
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

//using System.Windows.Forms;

namespace FtpActivities
{
	[Permission(Permissions.Automation)]
    [Designer(typeof(SftpDownloadMultipleDesigner))]
	public class SftpDownloadMultiple : BaseActivity
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
        public InArgument<FtpSessionGen> FtpSession 
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

        [Category("Output")]
        public OutArgument<System.Collections.Generic.List<FtpFileClass>> FilesListNotDownloaded
        {
            get;
            set;
        }
        [Category("Output")]
        public OutArgument<System.Collections.Generic.List<FtpFileClass>> FilesListDownloaded
        {
            get;
            set;
        }

        [Category("Output")]
        public OutArgument<System.Data.DataTable> DataTableNotDownloaded
        {
            get;
            set;
        }
        [Category("Output")]
        public OutArgument<System.Data.DataTable> DataTableDownloaded
        {
            get;
            set;
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
            metadata.AddArgument(new RuntimeArgument("FtpSession", typeof(FtpSessionGen), ArgumentDirection.In ));
            metadata.AddArgument(new RuntimeArgument("LocalPath", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("RemotePath", typeof(string), ArgumentDirection.In, true));

            metadata.AddArgument(new RuntimeArgument("WorkPath", typeof(string), ArgumentDirection.In)); //, true));
            metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("User_Pass", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("Sftp", typeof(bool), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("SKeyFiles", typeof(string), ArgumentDirection.In));
          
            metadata.AddArgument(new RuntimeArgument("FilesListNotDownloaded", typeof(System.Collections.Generic.List<FtpFileClass>), ArgumentDirection.Out));
            metadata.AddArgument(new RuntimeArgument("FilesListDownloaded", typeof(System.Collections.Generic.List<FtpFileClass>), ArgumentDirection.Out));

            metadata.AddArgument(new RuntimeArgument("DataTableNotDownloaded", typeof(System.Data.DataTable), ArgumentDirection.Out));
            metadata.AddArgument(new RuntimeArgument("DataTableDownloaded", typeof(System.Data.DataTable), ArgumentDirection.Out));
            base.CacheMetadata(metadata);
		}
        public SftpDownloadMultiple()
		{   
            this.DisplayName = "Multiple Download";
		}
        
        FtpSessionGen sessiongen = null;

        protected override void ExecuteAsync()
		{
			try
            {
                //System.Windows.Forms.MessageBox.Show("EnterExecute");
                
                sessiongen = FtpSession.Get<FtpSessionGen>();
                if (sessiongen == null)
                    if (SessionFromContainer != null)
                    {
                        sessiongen = SessionFromContainer;
                        //System.Windows.Forms.MessageBox.Show("Session From Container");
                    }
                bool autonomy = false;
                if (sessiongen == null)
                {
                    int modeSftp = 0;
                    if (Sftp.Get<Boolean>() == false)
                        modeSftp = 1;
                    sessiongen = new FtpSessionGen(modeSftp, Host.Get<string>(), User.Get<string>(), User_Pass.Get<string>(), Port.Get<int>(), SKeyFiles.Get<string>());
                    autonomy = true;
                    //System.Windows.Forms.MessageBox.Show("Session Autonomy");
                }

                //this.FtpSession.Set(ftpSession);
                //this.FtpSession.Get<FtpSessionGen>().Connect();
                ////System.Windows.Forms.MessageBox.Show("Enter2Execute");

                string localpath = LocalPath.Get<string>(); 
                string remotepath = RemotePath.Get<string>();
                string[] paths = null;

                //System.Windows.Forms.MessageBox.Show("Enter3Execute");

                ArrayList listFilesNotDown = new ArrayList();
                ArrayList listFilesDown = new ArrayList();

                if (remotepath.Contains("|"))
                {
                    
                    paths = remotepath.Split('|');
                
                    foreach (string rpath in paths)
                    {
                        if (this.IsActivityCanceled)
                        {
                            //System.Windows.Forms.MessageBox.Show("Canceled");
                            return;
                        }

                        if (rpath.Length > 1)
                        {
                            //System.Windows.Forms.MessageBox.Show("Enter4.1Execute");
                            if (sessiongen.RemoteDirectoryExists(rpath))
                            {
                                //System.Windows.Forms.MessageBox.Show("Enter5Execute");

                                sessiongen.DownloadMultiple(localpath, rpath);
                                sessiongen.VerifyDownload(rpath, localpath, ref listFilesNotDown, ref listFilesDown);
                            }
                            if (sessiongen.RemoteFileExists(rpath))
                            {
                                FtpFileClass ftpFile = sessiongen.GetFileAttributes(rpath);
                                sessiongen.DownloadOne(rpath, localpath);
                                if (! File.Exists(localpath + @"\" + ftpFile.Name))
                                    listFilesNotDown.Add(ftpFile);
                                else
                                    listFilesDown.Add(ftpFile);
                            }
                        }
                    }
                }
                else
                {
                    if (sessiongen.RemoteDirectoryExists(remotepath))
                    {   
                        sessiongen.DownloadMultiple(localpath, remotepath);
                        sessiongen.VerifyDownload(remotepath, localpath, ref listFilesNotDown, ref listFilesDown);
                    }
                    if (sessiongen.RemoteFileExists(remotepath))
                    {
                        FtpFileClass ftpFile = sessiongen.GetFileAttributes(remotepath);
                        sessiongen.DownloadOne(remotepath, localpath);
                        if (!File.Exists(localpath + @"\" + ftpFile.Name))
                            listFilesNotDown.Add(ftpFile);
                        else
                            listFilesDown.Add(ftpFile);
                    }       
                }

                System.Collections.Generic.List<FtpFileClass> listNotDown = new System.Collections.Generic.List<FtpFileClass>();
                foreach (FtpFileClass f in listFilesNotDown) listNotDown.Add(f);
                this.FilesListNotDownloaded.Set(listNotDown);

                System.Data.DataTable datable = sessiongen.DataTableFromFileList(listFilesNotDown);
                if (DataTableNotDownloaded != null)
                    this.DataTableNotDownloaded.Set(datable);
                
                listFilesNotDown.Clear();
                
                
                System.Collections.Generic.List<FtpFileClass> listDown = new System.Collections.Generic.List<FtpFileClass>();
                foreach (FtpFileClass f in listFilesDown) listDown.Add(f);
                this.FilesListDownloaded.Set(listDown);

                System.Data.DataTable datable2 = sessiongen.DataTableFromFileList(listFilesDown);
                if (DataTableDownloaded != null)
                    this.DataTableDownloaded.Set(datable2);

                listFilesDown.Clear();

                if (autonomy)
                    if (sessiongen.IsConnected())
                    {
                        sessiongen.Disconnect();
                    }
            }
            catch (System.Exception ex)
            {
                base.HandleException(ex);
            }
        }

        
        protected override System.IAsyncResult BeginExecute(NativeActivityContext context, System.AsyncCallback callback, object state)
        {
            NativeActivityContext cx = context;

            SessionFromContainer  = (FtpSessionGen)context.Properties.Find("ftpSession");

            //PropertyDescriptorCollection array = cx.DataContext.GetProperties(); //base.GetType().GetProperties();
            //foreach (PropertyDescriptor propertyInfo in array)
            //{
            //   if (propertyInfo.PropertyType != null && propertyInfo.PropertyType.FullName.Contains("FtpSessionGen") )//("SftpClient"))
            //    {   
            //        object obj = propertyInfo.GetValue(cx.DataContext);
                   
            //        FtpSessionGen sess = (FtpSessionGen)obj;
            //        if (sess != null)
            //            if (sess.IsConnected())
            //            {
            //                SessionFromContainer = (FtpSessionGen)obj;
            //                this.FtpSession.Set((FtpSessionGen)obj);
            //                break;
            //            }
            //    }
            //}
            
            return base.BeginExecute(context, callback, state);
        }

        protected override void Cancel(NativeActivityContext context)
        {
            this.IsActivityCanceled = true;
        }

	}
}
