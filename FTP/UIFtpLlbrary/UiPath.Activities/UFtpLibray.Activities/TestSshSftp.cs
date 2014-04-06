using S22.Imap;
using S22.Pop3;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using FtpActivities.Utilities;
using UiPath.Library;

using Renci.SshNet.Common;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Classes;
using Renci.SshNet.Sftp;

namespace FtpActivities
{
	[Permission(Permissions.GetEmail)]
	public class TestSshSftp : BaseActivity
	{
		[Category("Logon")]
		public InArgument<string> User
		{
			get;
			set;
		}
		[Category("Logon")]
		public InArgument<string> Password
		{
			get;
			set;
		}
		[Category("Host")]
		public InArgument<string> Host
		{
			get;
			set;
		}
		[Category("Host")]
		public InArgument<int> Port
		{
			get;
			set;
		}
		[Category("Host")]
		public bool EnableSSL
		{
			get;
			set;
		}
        //[Category("Input")]
        //public bool DeleteMessages
        //{
        //    get;
        //    set;
        //}
        //[Category("Input")]
        //public bool OnlyUnreadMessages
        //{
        //    get;
        //    set;
        //}
		[Category("Input")]
		public InArgument<int> Top
		{
			get;
			set;
		}
        //[Category("Input")]
        //public bool MarkAsRead
        //{
        //    get;
        //    set;
        //}
        //[Category("Host")]
        //public EmailProtocol Protocol
        //{
        //    get;
        //    set;
        //}
		[Category("Output")]
		public OutArgument<System.Collections.Generic.List<SftpFile>> Messages
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("User", typeof(string), ArgumentDirection.In, true));
			metadata.AddArgument(new RuntimeArgument("Password", typeof(string), ArgumentDirection.In, true));
			metadata.AddArgument(new RuntimeArgument("Host", typeof(string), ArgumentDirection.In, true));
			metadata.AddArgument(new RuntimeArgument("Port", typeof(int), ArgumentDirection.In, true));
			metadata.AddArgument(new RuntimeArgument("Top", typeof(int), ArgumentDirection.In, false));
			metadata.AddArgument(new RuntimeArgument("Messages", typeof(System.Collections.Generic.List<SftpFile>), ArgumentDirection.Out));
			base.CacheMetadata(metadata);
		}
		public TestSshSftp()
		{
            //this.Protocol = EmailProtocol.IMAP;
			this.EnableSSL = true;
		}
		protected override void ExecuteAsync()
		{
			try
			{

                Renci.SshNet.SftpClient sftpClient = new Renci.SshNet.SftpClient(Host.Get<string>(), Port.Get<int>(), User.Get<string>(), Password.Get<string>());
                sftpClient.Connect();
                Renci.SshNet.ConnectionInfo coinfo = sftpClient.ConnectionInfo;
                IEnumerable<SftpFile> ief = sftpClient.ListDirectory("\\");

                System.Collections.Generic.List<SftpFile> list = new System.Collections.Generic.List<SftpFile>();
                list = ief as System.Collections.Generic.List<SftpFile>;
                this.Messages.Set(list);
                //SftpFile sf = sftpClient.Get("\\Test1.txt");
                //string[] strLines = sftpClient.ReadAllLines(sf.FullName);

                sftpClient.Disconnect();

                //if (this.Protocol == EmailProtocol.IMAP)
                //{
                    //ImapClient imapClient = new ImapClient(this.Server.Get<string>(), this.Port.Get<int>(), this.Email.Get<string>(), this.Password.Get<string>(), S22.Imap.AuthMethod.Login, this.EnableSSL, null);
                    //uint[] array;
                    ////if (this.OnlyUnreadMessages)
                    //{
                    //    array = imapClient.Search(SearchCondition.Unseen(), null);
                    //}
                    ////else
                    //{
                    //    array = imapClient.Search(SearchCondition.All(), null);
                    //}
                    //int num = 0;
                    //if (this.Top != null && this.Top.Expression != null && array.Count<uint>() > this.Top.Get<int>())
                    //{
                    //    num = array.Count<uint>() - this.Top.Get<int>();
                    //}
                //    System.Collections.Generic.List<SftpFile> list = new System.Collections.Generic.List<SftpFile>();
                //    for (int i = num; i < array.Count<uint>(); i++)
                //    {
                //        //list.Add(imapClient.GetMessage(array[i], this.MarkAsRead, null));
                //        //if (this.DeleteMessages)
                //        {
                //            imapClient.DeleteMessage(array[i], null);
                //        }
                //    }
                //    imapClient.Expunge(null);
                //    imapClient.Logout();
                //    this.Messages.Set(list);
                //}
                //else
            //    {
            //        //if (this.Protocol == EmailProtocol.POP3)
            //        {
            //            Pop3Client pop3Client = new Pop3Client(this.Server.Get<string>(), this.Port.Get<int>(), this.Email.Get<string>(), this.Password.Get<string>(), S22.Pop3.AuthMethod.Login, this.EnableSSL, null);
            //            uint[] messageNumbers = pop3Client.GetMessageNumbers();
            //            int num2 = 0;
            //            if (this.Top != null && this.Top.Expression != null && messageNumbers.Count<uint>() > this.Top.Get<int>())
            //            {
            //                num2 = messageNumbers.Count<uint>() - this.Top.Get<int>();
            //            }
            //            System.Collections.Generic.List<MailMessage> list2 = new System.Collections.Generic.List<MailMessage>();
            //            for (int j = num2; j < messageNumbers.Count<uint>(); j++)
            //            {
            //                list2.Add(pop3Client.GetMessage(messageNumbers[j], S22.Pop3.FetchOptions.Normal, this.DeleteMessages));
            //            }
            //            pop3Client.Logout();
            //            this.Messages.Set(list2.ToList<MailMessage>());
            //        }
            //    }
            }
			catch (System.Exception ex)
			{
				base.HandleException(ex);
			}
		}
	}
}
