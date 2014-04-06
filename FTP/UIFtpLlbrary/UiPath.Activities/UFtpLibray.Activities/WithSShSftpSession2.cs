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

using FtpActivities.Design; //UiPath.Activities.Design

namespace FtpActivities
{
    [Designer(typeof(WithSSHSftpSessionDesigner2))]
	public class WithSSHSftpSession2 : BaseActivity
	{
		protected bool ScheduleBody;
        //[Category("Target")]
        //public InArgument<string> WorkPath
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<string> User
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<string> User_Pass
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<string> Host
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<int> Port
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<bool> Sftp
        //{
        //    get;
        //    set;
        //}
        //[Category("Logon")]
        //public InArgument<string> SKeyFiles
        //{
        //    get;
        //    set;
        //}
        [Category("Input")]
        public InArgument<FtpSessionGen> FtpSession 
        {
            get;
            set;
        }
        [Category("Output")]
        public OutArgument<FtpSessionGen> FtpSessionOut
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
            //metadata.AddArgument(new RuntimeArgument("WorkPath", typeof(string), ArgumentDirection.In)); //, true));
            //metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In));
            //metadata.AddArgument(new RuntimeArgument("User_Pass", typeof(string), ArgumentDirection.In));
            //metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In));
            //metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In));
            //metadata.AddArgument(new RuntimeArgument("Sftp", typeof(bool), ArgumentDirection.In));
            //metadata.AddArgument(new RuntimeArgument("SKeyFiles", typeof(string), ArgumentDirection.In));
          
          
            metadata.AddArgument(new RuntimeArgument("FtpSession", typeof(FtpSessionGen), ArgumentDirection.In, true));
            metadata.AddArgument(new RuntimeArgument("FtpSessionOut", typeof(FtpSessionGen), ArgumentDirection.Out));

            //metadata.AddVariable(FtpVarSession);

            metadata.AddChild(this.Body);
            

            base.CacheMetadata(metadata);
		}
        public WithSSHSftpSession2()
		{
			base.ContinueOnError = true;
            this.DisplayName = "With sftp/ftp session";
			this.Body = new Sequence
			{
				DisplayName = "Do"
			};
		}
		protected override void ExecuteAsync()
		{
            try
            {
                ftpSession = this.FtpSession.Get<FtpSessionGen>();

                //if (ftpSession == null)
                //{
                //    int modeSftp = 0;
                //    if (Sftp.Get<Boolean>() == false)
                //        modeSftp = 1;

                //    ftpSession = new FtpSessionGen(modeSftp, Host.Get<string>(), User.Get<string>(), User_Pass.Get<string>(), Port.Get<int>(), SKeyFiles.Get<string>());
                //}

                if (ftpSession != null)
                {
                    this.FtpSessionOut.Set(ftpSession);
                    //this.FtpSession.Get<FtpSessionGen>().Connect();
                    ftpSession.Connect();
                }
                
                this.ScheduleBody = true;
            }
            catch (System.Exception ex2)
            {
                base.HandleException(ex2);
            }
		}
		protected override void EndExecute(NativeActivityContext context, System.IAsyncResult result)
		{
			base.EndExecute(context, result);
			if (this.ScheduleBody)
			{
				context.ScheduleActivity(this.Body, new CompletionCallback(this.onCompletedCallback), new FaultCallback(this.onFaultedCallback));
			}
		}
		private void onCompletedCallback(NativeActivityContext context, ActivityInstance activityInstance)
		{
            //this.FtpSession.Get<FtpSessionGen>().Disconnect();
		}
		private void onFaultedCallback(NativeActivityFaultContext faultContext, System.Exception exception, ActivityInstance source)
		{
            //this.FtpSession.Get<FtpSessionGen>().Disconnect();
		}

        
	}
}
