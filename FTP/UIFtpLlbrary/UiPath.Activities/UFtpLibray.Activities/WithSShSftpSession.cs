using System;
using System.Activities;
using System.Activities.Statements;
using System.ComponentModel;
using System.Runtime.InteropServices;

using System.Collections;
using System.Collections.Generic;

using Renci.SshNet.Common;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Classes;
using Renci.SshNet.Sftp;

using FtpActivities.Design;

namespace FtpActivities
{
    [Designer(typeof(WithSSHSftpSessionDesigner))]
	public class WithSSHSftpSession : BaseActivity
	{
		protected bool ScheduleBody;
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
        [Category("Output")]
        public OutArgument<FtpSessionGen> FtpSession 
        {
            get;
            set;
        }
		[Browsable(false)]
		public Activity Body
		{
			get;
			set;
		}

        private FtpSessionGen ftpSession;

       
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("WorkPath", typeof(string), ArgumentDirection.In)); //, true));
            metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In, true));
            metadata.AddArgument(new RuntimeArgument("User_Pass", typeof(string), ArgumentDirection.In, true));
            metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In, true));
            metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In, true));
            metadata.AddArgument(new RuntimeArgument("Sftp", typeof(bool), ArgumentDirection.In));
            metadata.AddArgument(new RuntimeArgument("SKeyFiles", typeof(string), ArgumentDirection.In));
          
            metadata.AddArgument(new RuntimeArgument("FtpSession", typeof(FtpSessionGen), ArgumentDirection.Out));

            metadata.AddChild(this.Body);
            
            base.CacheMetadata(metadata);
		}
        public WithSSHSftpSession()
		{
			base.ContinueOnError = true;
            this.DisplayName = "Open sftp/ftp session";
			this.Body = new Sequence
			{
				DisplayName = "Do"
			};

         
		}
		protected override void ExecuteAsync()
		{
            try
            {
                int modeSftp = 0;
                if (Sftp.Get<Boolean>() == false)
                    modeSftp = 1;

                ftpSession = new FtpSessionGen(modeSftp, Host.Get<string>(), User.Get<string>(), User_Pass.Get<string>(), Port.Get<int>(), SKeyFiles.Get<string>());
                ftpSession.Connect(); 

                if (FtpSession != null)
                    this.FtpSession.Set(ftpSession);

                //System.Windows.Forms.MessageBox.Show("SessOut set");
                
                this.ScheduleBody = true;
                ////this.FtpSession.Get<FtpSessionGen>().Connect();
            }
            catch (System.Exception ex2)
            {
                base.HandleException(ex2);
            }
		}

        bool blnVariableSession = false;
		protected override void EndExecute(NativeActivityContext context, System.IAsyncResult result)
		{
            
            PropertyDescriptorCollection array = context.DataContext.GetProperties(); //base.GetType().GetProperties();
            foreach (PropertyDescriptor propertyInfo in array)
            {
                if (propertyInfo.PropertyType != null && propertyInfo.PropertyType.FullName.Contains("FtpSessionGen"))//("SftpClient"))
                {
                    //System.Windows.Forms.MessageBox.Show(propertyInfo.Name);
                    
                    //object obj = propertyInfo.GetValue(context.DataContext);
                    //FtpSessionGen sess = (FtpSessionGen)obj;
                    //if (sess != null)
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Var not null");
                    //    if (sess.IsConnected())
                    //{
                    //    blnVariableSession = true;
                    //    System.Windows.Forms.MessageBox.Show(propertyInfo.PropertyType.FullName);
                    //    break;
                    //}
                    //}
                }
            }
            context.Properties.Add("ftpSession", ftpSession);

			base.EndExecute(context, result);
			if (this.ScheduleBody)
			{
				context.ScheduleActivity(this.Body, new CompletionCallback(this.onCompletedCallback), new FaultCallback(this.onFaultedCallback));
			}
		}
		private void onCompletedCallback(NativeActivityContext context, ActivityInstance activityInstance)
		{
            if (blnVariableSession)
            {
                //System.Windows.Forms.MessageBox.Show("No Close");
                return;
            }
            this.FtpSession.Get<FtpSessionGen>().Disconnect();
		}
		private void onFaultedCallback(NativeActivityFaultContext faultContext, System.Exception exception, ActivityInstance source)
		{
            this.FtpSession.Get<FtpSessionGen>().Disconnect();
		}

	}
}
