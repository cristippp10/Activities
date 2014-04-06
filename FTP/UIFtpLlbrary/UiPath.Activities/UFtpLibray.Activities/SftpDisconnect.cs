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
    public class SftpDisconnect : BaseActivity
	{
		protected bool ScheduleBody;
        
        [Category("Input")]
        public InArgument<FtpSessionGen> FtpSession 
        {
            get;
            set;
        }
        private FtpSessionGen ftpSession;
        
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
           
            metadata.AddArgument(new RuntimeArgument("FtpSession", typeof(FtpSessionGen), ArgumentDirection.In, true));
            
            base.CacheMetadata(metadata);
		}
        public SftpDisconnect()
		{
			base.ContinueOnError = true;
            this.DisplayName = "Disconnect";
			
		}
		protected override void ExecuteAsync()
		{
            try
            {
                ftpSession = this.FtpSession.Get<FtpSessionGen>();

                if (ftpSession != null)
                {
                   if (ftpSession.IsConnected())
                    ftpSession.Disconnect();
                }
                
            }
            catch (System.Exception ex2)
            {
                base.HandleException(ex2);
            }
		}
	
        
	}
}
