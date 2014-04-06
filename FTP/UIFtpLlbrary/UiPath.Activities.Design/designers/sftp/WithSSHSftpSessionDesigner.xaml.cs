using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Statements;

using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

//using UiPath.Activities;

using System.Windows.Forms;

using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Collections;
using System.IO;

//using BrightIdeasSoftware;

using TreeListViewDragDrop;

namespace FtpActivities.Design
{
	public partial class WithSSHSftpSessionDesigner : ActivityDesigner, IComponentConnector
	{
		//private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        //private TreeListViewDragDrop.Form1 frmSftpSession; //= new TreeListViewDragDrop.Form1();
        
        //internal LinkPropertyControl WorkbookSetter;
        //private bool _contentLoaded;
        public WithSSHSftpSessionDesigner()
		{
			this.InitializeComponent();
            //this.openFileDialog1.Title = "Select a file";
            //this.openFileDialog1.Filter = "Files|*.*";
            //this.openFileDialog1.FileOk += new CancelEventHandler(this.openFileDialog1_FileOk);

		}
		private void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			//this.openFileDialog1.ShowDialog();
            try
            {
                TreeListViewDragDrop.Form1 frmSftpSession = null;
                frmSftpSession = new TreeListViewDragDrop.Form1();
                //frmSftpSession.UsedForDownload = true;

                int modeSftp = 0;
                if (base.ModelItem.Properties["Sftp"].Value != null)
                {
                    InArgument<bool> Sftp = (InArgument<bool>)base.ModelItem.Properties["Sftp"].Value.GetCurrentValue();
                    bool mode = Convert.ToBoolean(Sftp.Expression.ToString());
                    if (mode == false)
                        modeSftp = 1;
                }
                frmSftpSession.FtpMode = modeSftp;
                frmSftpSession.sKeyFiles = ""; // @"G:\Bak\DSA key.txt|G:\Bak\DSA key pass.txt<tester>|G:\Bak\RSA key.txt|G:\Bak\RSA key pass.txt<tester>";
                if (base.ModelItem.Properties["Host"].Value != null)
                {
                    InArgument<string> Host = (InArgument<string>)base.ModelItem.Properties["Host"].Value.GetCurrentValue();
                    frmSftpSession.host = Host.Expression.ToString();
                }

                if (base.ModelItem.Properties["User"].Value != null)
                {
                    InArgument<string> User = (InArgument<string>)base.ModelItem.Properties["User"].Value.GetCurrentValue();
                    frmSftpSession.username = User.Expression.ToString();
                }

                if (base.ModelItem.Properties["User_Pass"].Value != null)
                {
                    InArgument<string> User_Pass = (InArgument<string>)base.ModelItem.Properties["User_Pass"].Value.GetCurrentValue();
                    frmSftpSession.password = User_Pass.Expression.ToString();
                }
                if (base.ModelItem.Properties["Port"].Value != null)
                {
                    InArgument<int> Port = (InArgument<int>)base.ModelItem.Properties["Port"].Value.GetCurrentValue();
                    frmSftpSession.port = Convert.ToInt32(Port.Expression.ToString());
                }
                frmSftpSession.LocalPathRoot = @"C:\";
                if (base.ModelItem.Properties["WorkPath"].Value != null)
                {
                    InArgument<string> WorkPath = (InArgument<string>)base.ModelItem.Properties["WorkPath"].Value.GetCurrentValue();
                    string dirpath = WorkPath.Expression.ToString();
                    if (Directory.Exists(dirpath))
                        frmSftpSession.LocalPathRoot = dirpath; // "G:\\Bak";//WorkPath.Expression.ToString();
                }

                frmSftpSession.sKeyFiles = "";
                if (base.ModelItem.Properties["SKeyFiles"].Value != null)
                {
                    InArgument<string> SKeyFiles = (InArgument<string>)base.ModelItem.Properties["SKeyFiles"].Value.GetCurrentValue();
                    frmSftpSession.sKeyFiles = SKeyFiles.Expression.ToString();
                }

                DialogResult dlgResult = frmSftpSession.ShowDialog();
                if (dlgResult == DialogResult.OK)
                {
                    base.ModelItem.Properties["WorkPath"].SetValue(new InArgument<string>(frmSftpSession.LocalPathRoot));
                    ArrayList alocal = frmSftpSession.SelectedLocalPaths as ArrayList;

                    base.ModelItem.Properties["Host"].SetValue(new InArgument<string>(frmSftpSession.host));
                    base.ModelItem.Properties["Port"].SetValue(new InArgument<int>(frmSftpSession.port));
                    base.ModelItem.Properties["User"].SetValue(new InArgument<string>(frmSftpSession.username));
                    base.ModelItem.Properties["User_Pass"].SetValue(new InArgument<string>(frmSftpSession.password));
                    base.ModelItem.Properties["SKeyFiles"].SetValue(new InArgument<string>(frmSftpSession.sKeyFiles));

                    if (frmSftpSession.FtpMode == 0)  
                        base.ModelItem.Properties["Sftp"].SetValue(new InArgument<bool>(true));
                    else
                        base.ModelItem.Properties["Sftp"].SetValue(new InArgument<bool>(false));

                    //this.PathC.HintText = "Host: " + frmSftpSession.host + " Port: " + frmSftpSession.port.ToString() + " User: " + frmSftpSession.username;
                    //this.tbxConnection.Text = "Host: " + frmSftpSession.host + " Port: " + frmSftpSession.port.ToString() + " User: " + frmSftpSession.username;
                    
                   
                    ArrayList aremote = frmSftpSession.SelectedRemotePaths as ArrayList;
                    string strRemoteFiles = "";
                    foreach (object mo in aremote)
                    {
                        TreeListViewDragDrop.Form1.ModelWithChildren mod = (TreeListViewDragDrop.Form1.ModelWithChildren)mo;
                        strRemoteFiles = strRemoteFiles + mod.RemotePath + ";";
                    }
                  
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
		}
        //private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        //{
        //    base.ModelItem.Properties["WorkPath"].SetValue(new InArgument<string>(this.openFileDialog1.FileName));
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/excel/WithSSHSftpSessionDesigner.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //internal Delegate _CreateDelegate(Type delegateType, string handler)
        //{
        //    return Delegate.CreateDelegate(delegateType, this, handler);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    if (connectionId == 1)
        //    {
        //        this.WorkbookSetter = (LinkPropertyControl)target;
        //        return;
        //    }
        //    this._contentLoaded = true;
        //}
	}
}
