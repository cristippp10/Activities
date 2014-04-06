using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Presentation;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

using System.Collections;
using System.IO;
using System.Windows.Forms;

using TreeListViewDragDrop;



namespace FtpActivities.Design
{
    public partial class SftpCreateDirDesigner : ActivityDesigner, IComponentConnector
	{
		//private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private System.Windows.Forms.FolderBrowserDialog openFileBrowswer = new System.Windows.Forms.FolderBrowserDialog();
        //internal LinkPropertyControl WorkbookSetter;
        //private bool _contentLoaded;


        public SftpCreateDirDesigner()
		{
			this.InitializeComponent();
            
            //this.openFileDialog1.Title = "Select a file";
            //this.openFileDialog1.Filter = "All Files|*.*";
            //this.openFileDialog1.FileOk += new CancelEventHandler(this.openFileDialog1_FileOk);
		}
		private void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			//this.openFileDialog1.ShowDialog();

            System.Activities.Presentation.Model.ModelItem modelitem = base.ModelItem;
            
            System.Activities.Presentation.Model.ModelItem modelitemparent = modelitem.Parent;
            
            System.Activities.Presentation.Model.ModelItem modelitemparentup = null; 

            while (modelitemparent != null)
            {
                if (modelitemparent.Properties != null)
                {
                    if (modelitemparent.Properties["Host"] != null && modelitemparent.Properties["Port"] != null)
                        break;
                }
                modelitemparentup = modelitemparent;
                modelitemparent = modelitemparent.Parent;
            }

            if (modelitemparentup.Parent != null)
            if (modelitemparent.Parent != null)
            if (modelitemparent.Properties != null)  
            {
                TreeListViewDragDrop.Form1 frmSftpSession = null;
                frmSftpSession = new TreeListViewDragDrop.Form1();
                frmSftpSession.UsedForUpload = true;
                frmSftpSession.UsedForDownload = true;

                int modeSftp = 0;
                if (modelitemparent.Properties["Sftp"].Value != null)
                {
                    InArgument<bool> Sftp = (InArgument<bool>)modelitemparent.Properties["Sftp"].Value.GetCurrentValue();
                    bool mode = Convert.ToBoolean(Sftp.Expression.ToString());
                    if (mode == false)
                        modeSftp = 1;
                }

                if (modelitemparent.Properties["Host"].Value != null)
                {
                    frmSftpSession.FtpMode = modeSftp;
                    InArgument<string> Host = (InArgument<string>)modelitemparent.Properties["Host"].Value.GetCurrentValue();
                    frmSftpSession.host = Host.Expression.ToString();
                }
                if (modelitemparent.Properties["User"].Value != null)
                {
                    InArgument<string> User = (InArgument<string>)modelitemparent.Properties["User"].Value.GetCurrentValue();
                    frmSftpSession.username = User.Expression.ToString();
                }
                if (modelitemparent.Properties["User_Pass"].Value != null)
                {
                    InArgument<string> User_Pass = (InArgument<string>)modelitemparent.Properties["User_Pass"].Value.GetCurrentValue();
                    frmSftpSession.password = User_Pass.Expression.ToString();
                }
                if (modelitemparent.Properties["Port"].Value != null)
                {
                    InArgument<int> Port = (InArgument<int>)modelitemparent.Properties["Port"].Value.GetCurrentValue();
                    frmSftpSession.port = Convert.ToInt32(Port.Expression.ToString());
                }
                frmSftpSession.sKeyFiles = "";
                if (modelitemparent.Properties["SKeyFiles"].Value != null)
                {
                    InArgument<string> SKeyFiles = (InArgument<string>)modelitemparent.Properties["SKeyFiles"].Value.GetCurrentValue();
                    frmSftpSession.sKeyFiles = SKeyFiles.Expression.ToString();
                }
                if (modelitemparent.Properties["WorkPath"].Value != null)
                {
                    InArgument<string> WorkPath = (InArgument<string>)modelitemparent.Properties["WorkPath"].Value.GetCurrentValue();
                    frmSftpSession.LocalPathRoot = WorkPath.Expression.ToString();
                }
                else
                    frmSftpSession.LocalPathRoot = @"C:\";
                
                DialogResult dlgResult = frmSftpSession.ShowDialog();
                if (dlgResult == DialogResult.OK)
                {
                    //base.ModelItem.Properties["FilePath"].SetValue(new InArgument<string>(frmSftpSession.LocalPathRoot));
                    ArrayList alocal = frmSftpSession.SelectedLocalPaths as ArrayList;
                    string strLocalFiles = "";

                    base.ModelItem.Properties["WorkPath"].SetValue(new InArgument<string>(frmSftpSession.LocalPathRoot));

                    base.ModelItem.Properties["Host"].SetValue(new InArgument<string>(frmSftpSession.host));
                    base.ModelItem.Properties["Port"].SetValue(new InArgument<int>(frmSftpSession.port));
                    base.ModelItem.Properties["User"].SetValue(new InArgument<string>(frmSftpSession.username));
                    base.ModelItem.Properties["User_Pass"].SetValue(new InArgument<string>(frmSftpSession.password));
                    base.ModelItem.Properties["SKeyFiles"].SetValue(new InArgument<string>(frmSftpSession.sKeyFiles));
                    if (frmSftpSession.FtpMode == 0)
                        base.ModelItem.Properties["Sftp"].SetValue(new InArgument<bool>(true));
                    else
                        base.ModelItem.Properties["Sftp"].SetValue(new InArgument<bool>(false));

                    foreach (object mo in alocal)
                    {
                        TreeListViewDragDrop.Form1.ModelWithChildren mod = (TreeListViewDragDrop.Form1.ModelWithChildren)mo;
                        strLocalFiles = strLocalFiles + mod.LocalPath + "|";
                    }
                    if (strLocalFiles == "" || strLocalFiles == "|")
                    {
                        //System.Windows.Forms.MessageBox.Show("local path not selected!");
                        //return;
                    }
                    else
                        if (strLocalFiles[strLocalFiles.Length - 1] == '|') strLocalFiles = strLocalFiles.Substring(0, strLocalFiles.Length - 1);
                    
                    ArrayList aremote = frmSftpSession.SelectedRemotePaths as ArrayList;
                    string strRemoteFiles = "";
                    if (aremote != null)
                        if (aremote.Count == 1)
                        {
                            string pathupload = ((TreeListViewDragDrop.Form1.ModelWithChildren)aremote[0]).RemotePath;
                            base.ModelItem.Properties["RemotePath"].SetValue(new InArgument<string>(pathupload));
                            strRemoteFiles = pathupload;
                        }
                  
                    base.ModelItem.Properties["RemotePath"].SetValue(new InArgument<string>(strRemoteFiles));
                    //base.ModelItem.Properties["NewDirName"].SetValue(new InArgument<string>(strLocalFiles));
                    this.tbxConnection.Text = "Host: " + frmSftpSession.host + " Port: " + frmSftpSession.port.ToString() + " User: " + frmSftpSession.username;
                }
            }

            //---
            if (modelitemparentup.Parent == null)
            {
                //System.Windows.Forms.MessageBox.Show("NotContainer");

                TreeListViewDragDrop.Form1 frmSftpSession = null;
                frmSftpSession = new TreeListViewDragDrop.Form1();
                frmSftpSession.UsedForUpload = true;
                frmSftpSession.UsedForDownload = true;

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
                    //ArrayList alocal = frmSftpSession.SelectedLocalPaths as ArrayList;

                    base.ModelItem.Properties["Host"].SetValue(new InArgument<string>(frmSftpSession.host));
                    base.ModelItem.Properties["Port"].SetValue(new InArgument<int>(frmSftpSession.port));
                    base.ModelItem.Properties["User"].SetValue(new InArgument<string>(frmSftpSession.username));
                    base.ModelItem.Properties["User_Pass"].SetValue(new InArgument<string>(frmSftpSession.password));
                    base.ModelItem.Properties["SKeyFiles"].SetValue(new InArgument<string>(frmSftpSession.sKeyFiles));

                    if (frmSftpSession.FtpMode == 0)
                        base.ModelItem.Properties["Sftp"].SetValue(new InArgument<bool>(true));
                    else
                        base.ModelItem.Properties["Sftp"].SetValue(new InArgument<bool>(false));

                    ArrayList alocal = frmSftpSession.SelectedLocalPaths as ArrayList;
                    string strLocalFiles = "";

                    base.ModelItem.Properties["WorkPath"].SetValue(new InArgument<string>(frmSftpSession.LocalPathRoot));

                    base.ModelItem.Properties["Host"].SetValue(new InArgument<string>(frmSftpSession.host));
                    base.ModelItem.Properties["Port"].SetValue(new InArgument<int>(frmSftpSession.port));
                    base.ModelItem.Properties["User"].SetValue(new InArgument<string>(frmSftpSession.username));
                    base.ModelItem.Properties["User_Pass"].SetValue(new InArgument<string>(frmSftpSession.password));
                    base.ModelItem.Properties["SKeyFiles"].SetValue(new InArgument<string>(frmSftpSession.sKeyFiles));

                    foreach (object mo in alocal)
                    {
                        TreeListViewDragDrop.Form1.ModelWithChildren mod = (TreeListViewDragDrop.Form1.ModelWithChildren)mo;
                        strLocalFiles = strLocalFiles + mod.LocalPath + "|";
                    }
                    if (strLocalFiles == "" || strLocalFiles == "|")
                    {
                        //System.Windows.Forms.MessageBox.Show("local path not selected!");
                        //return;
                    }
                    else
                        if (strLocalFiles[strLocalFiles.Length - 1] == '|') strLocalFiles = strLocalFiles.Substring(0, strLocalFiles.Length - 1);

                    ArrayList aremote = frmSftpSession.SelectedRemotePaths as ArrayList;
                    string strRemoteFiles = "";
                    if (aremote != null)
                        if (aremote.Count == 1)
                        {
                            string pathupload = ((TreeListViewDragDrop.Form1.ModelWithChildren)aremote[0]).RemotePath;
                            base.ModelItem.Properties["RemotePath"].SetValue(new InArgument<string>(pathupload));
                            strRemoteFiles = pathupload;
                        }

                    base.ModelItem.Properties["RemotePath"].SetValue(new InArgument<string>(strRemoteFiles));
                    //base.ModelItem.Properties["LocalPath"].SetValue(new InArgument<string>(strLocalFiles));
                    this.tbxConnection.Text = "Host: " + frmSftpSession.host + " Port: " + frmSftpSession.port.ToString() + " User: " + frmSftpSession.username;
                }
            }

            //---
            //if (openFileBrowswer.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            //{ 
            //    base.ModelItem.Properties["FilePath"].SetValue(new InArgument<string>(this.openFileBrowswer.SelectedPath)) ; 
            //} 
		}
        //private void openFileDialog1_FileOk(openFileBrowswer  sender, CancelEventArgs e)
        //{
        //    base.ModelItem.Properties["FilePath"].SetValue(new InArgument<string>(this.openFileDialog1.FileName));
        //    //base.ModelItem.Properties["ExistingWorkbook"].SetValue(null);
        //}

        ////[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        ////public void InitializeComponent()
        ////{
        ////    if (this._contentLoaded)
        ////    {
        ////        return;
        ////    }
        ////    this._contentLoaded = true;
        ////    Uri resourceLocator = new Uri("/FtpActivities.Design;component/designers/excel/readrangeworkbookdesigner.xaml", UriKind.Relative);
        ////    Application.LoadComponent(this, resourceLocator);
        ////}
        ////[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        ////internal Delegate _CreateDelegate(Type delegateType, string handler)
        ////{
        ////    return Delegate.CreateDelegate(delegateType, this, handler);
        ////}
        ////[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        ////void IComponentConnector.Connect(int connectionId, object target)
        ////{
        ////    if (connectionId == 1)
        ////    {
        ////        this.WorkbookSetter = (LinkPropertyControl)target;
        ////        return;
        ////    }
        ////    this._contentLoaded = true;
        ////}
	}
}
