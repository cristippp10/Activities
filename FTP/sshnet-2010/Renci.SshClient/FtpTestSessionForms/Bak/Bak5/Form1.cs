﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using BrightIdeasSoftware;

using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Sftp;

using System.Net.FtpClient;
using FtpClientFunc;

namespace TreeListViewDragDrop {
    public partial class Form1 : Form {
        public string host ; // TODO: Initialize to an appropriate value
        public string username ; // TODO: Initialize to an appropriate value
        //PrivateKeyFile[] keyFiles = null; // TODO: Initialize to an appropriate value
        public string password ;
        public int port ;
        public string LocalPathRoot;
        public int FtpMode;
        public Form1()
        {
           
            InitializeComponent();
       
        }
        public FtpSessionGen ftpSessionGen;
        public bool IsConnected = false;
        public bool UsedForDownload = false;
        public bool UsedForUpload = false;
        private void Form1_Load(object sender, EventArgs e) {

            //string host = "192.168.0.104"; // TODO: Initialize to an appropriate value
            //string username = "one"; // TODO: Initialize to an appropriate value
            ////PrivateKeyFile[] keyFiles = null; // TODO: Initialize to an appropriate value
            //string password = "one";

            //sftpClient = new Renci.SshNet.SftpClient(host, port, username, password);
            //sftpClient.Connect();
            //sftpClient = new Renci.SshNet.SftpClient(host, port, username, password);
            try
            {
                ftpSessionGen = new FtpSessionGen(FtpMode, host, username, password, port);
                ftpSessionGen.Connect();
            }
            catch { }

            IsConnected = ftpSessionGen.IsConnected();

            // Remember: TreeListViews must have a small image list assigned to them.
            // If they do not, the hit testing will be wildly off
            if (IsConnected)
            {
                olvColumn1.ImageGetter = delegate(object x)
                {
                    ModelWithChildren y = (ModelWithChildren)x;
                    if (y.Update == "file")
                        return "File_image";
                    else return "folder";
                }; //{ return "folder"; };
            }
            //olvColumn3.ImageGetter = delegate(object x) { return "folder"; };
            olvColumn3.ImageGetter = delegate(object x)
            {
                ModelWithChildren y = (ModelWithChildren)x;
                if (y.Update == "file")
                    return "File_image";
                else return "folder";
            }; //{

            // Allow all models to be expanded and each model will show Children as its sub-branches
            if (IsConnected)
            {
                treeListView1.CanExpandGetter = delegate(object x)
                {
                    ModelWithChildren y = (ModelWithChildren)x;
                    if (y.Update == "file")
                        return false;
                    else
                        return true;
                };
            }
            else 
                treeListView1.CanExpandGetter = null;

            if (IsConnected)
                treeListView1.ChildrenGetter = delegate(object x) { return ((ModelWithChildren)x).Children; };
            else
                treeListView1.ChildrenGetter = null;
            //treeListView2.CanExpandGetter = delegate(object x) { return true; };
            treeListView2.CanExpandGetter = delegate(object x)
            {
                ModelWithChildren y = (ModelWithChildren)x;
                if (y.Update == "file")
                    return false;
                else
                    return true;
            };
            treeListView2.ChildrenGetter = delegate(object x) { return ((ModelWithChildren)x).Children; };

            // In the Designer, set IsSimpleDropSink to true for both tree list views.
            // That creates an appropriately configured SimpleDropSink. 
            // Here, we are just configuring it a little more
            SimpleDropSink sink1 = (SimpleDropSink)treeListView1.DropSink;
            if (IsConnected)
            {
                sink1.AcceptExternal = true;
                sink1.CanDropBetween = true;
                sink1.CanDropOnBackground = true;
            }
            SimpleDropSink sink2 = (SimpleDropSink)treeListView2.DropSink;
            sink2.AcceptExternal = true;
            sink2.CanDropBetween = true;
            sink2.CanDropOnBackground = true;

            

            // Give each tree its top level objects to get things going
            //treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { 0, 1, 2, 3, 4, 5 });
            //ModelWithChildren mc1 = new ModelWithChildren();
            //ModelWithChildren mc2 = new ModelWithChildren();
            //LocalPathRoot = @"G:\Bak";
            if (IsConnected == true)
            {
                treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
                
            }
            else
            {
                //treeListView1.Enabled = false;
                treeListView1.Roots = null;
                treeListView1.EmptyListMsg = "Host: " + host + " - connection failed";
            }
            treeListView2.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot, 1);

            txtLocalPathRoot.Text = LocalPathRoot;

            if (UsedForDownload) treeListView2.MultiSelect = false;
            if (UsedForUpload) treeListView1.MultiSelect = false;

            timer1.Interval = 50;timer1.Start();
        }

        
        private void HandleModelCanDrop(object sender, BrightIdeasSoftware.ModelDropEventArgs e) {
            e.Handled = true;
            e.Effect = DragDropEffects.None;
            if (e.SourceModels.Contains(e.TargetModel))
                e.InfoMessage = "Cannot drop on self";
            else {
                IEnumerable<ModelWithChildren> sourceModels = e.SourceModels.Cast<ModelWithChildren>();
                if (e.DropTargetLocation == DropTargetLocation.Background) {
                    if (e.SourceListView == e.ListView && sourceModels.All(x => x.Parent == null))
                        e.InfoMessage = "Dragged objects are already roots";
                    else {
                        e.Effect = DragDropEffects.Move;
                        e.InfoMessage = "Drop on background to promote to roots";
                    }
                } else {
                    ModelWithChildren target = (ModelWithChildren)e.TargetModel;
                    if (target != null)
                    {
                        if (sourceModels.Any(x => target.IsAncestor(x)))
                            e.InfoMessage = "Cannot drop on descendant (think of the temporal paradoxes!)";
                        else
                            e.Effect = DragDropEffects.Move;
                    }
                }
            }
        }
    
        private void HandleModelDropped(object sender, BrightIdeasSoftware.ModelDropEventArgs e) {
            e.Handled = true;
            switch (e.DropTargetLocation)
            {
                case DropTargetLocation.AboveItem:
                    MoveObjectsToSibling(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView, 
                        (ModelWithChildren)e.TargetModel, 
                        e.SourceModels, 
                        0);
                    break;
                case DropTargetLocation.BelowItem:
                    MoveObjectsToSibling(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView, 
                        (ModelWithChildren)e.TargetModel, 
                        e.SourceModels, 
                        1);
                    break;
                case DropTargetLocation.Background:
                    MoveObjectsToRoots(
                        e.ListView as TreeListView, 
                        e.SourceListView as TreeListView, 
                        e.SourceModels);
                    break;
                case DropTargetLocation.Item:
                    MoveObjectsToChildren(
                        e.ListView as TreeListView, 
                        e.SourceListView as TreeListView, 
                        (ModelWithChildren)e.TargetModel, 
                        e.SourceModels);
                    break;
                default:
                    return;
            }

            e.RefreshObjects();
        }

        private void HandleCanDrop(object sender, OlvDropEventArgs e)
        {
            // This will only be triggered if HandleModelCanDrop doesn't set Handled to true.
            // In practice, this will only be called when the source of the drag is not an ObjectListView

            IDataObject data = e.DataObject as IDataObject;
            if (data == null || !data.GetDataPresent(DataFormats.UnicodeText))
                return;

            string str = data.GetData(DataFormats.UnicodeText) as string;
            e.Effect = String.IsNullOrEmpty(str) ? DragDropEffects.None : DragDropEffects.Copy;

            switch (e.DropTargetLocation)
            {
                case DropTargetLocation.AboveItem:
                case DropTargetLocation.BelowItem:
                    e.InfoMessage = "Cannot drop between items -- because I haven't written the logic :)";
                    break;
                case DropTargetLocation.Background:
                    e.InfoMessage = "Drop here to create a new root item called '" + str + "'";
                    break;
                case DropTargetLocation.Item:
                    e.InfoMessage = "Drop here to create a new child item called '" + str + "'";
                    break;
                default:
                    return;
            }
        }

        private void HandleDropped(object sender, OlvDropEventArgs e)
        {
            // This will only be triggered if HandleModelDropped doesn't set Handled to true.
            // In practice, this will only be called when the source of the drag is not an ObjectListView

            DataObject data = e.DataObject as DataObject;
            if (data == null || String.IsNullOrEmpty(data.GetText()))
                return;

            TreeListView treeListView = e.ListView as TreeListView;
            if (treeListView == null)
                return;

            ModelWithChildren newModel = new ModelWithChildren {
                Label = data.GetText(),
                DataForChildren = new ArrayList {"A", "B ", "C", "D", "E"}
            };

            switch (e.DropTargetLocation)
            {
                case DropTargetLocation.AboveItem:
                    break;
                case DropTargetLocation.BelowItem:
                    break;
                case DropTargetLocation.Background:
                    treeListView.AddObject(newModel);
                    break;
                case DropTargetLocation.Item:
                    ModelWithChildren targetModel = e.DropTargetItem.RowObject as ModelWithChildren;
                    if (targetModel != null)
                    {
                        newModel.Parent = targetModel;
                        targetModel.Children.Add(newModel);
                        treeListView.RefreshObject(targetModel);
                    }
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Move the given collection of model objects so that they become roots of the target tree
        /// </summary>
        /// <param name="targetTree"></param>
        /// <param name="sourceTree"></param>
        /// <param name="toMove"></param>
        private void MoveObjectsToRoots(TreeListView targetTree, TreeListView sourceTree, IList toMove) {
            
             //ModelWithChildren modeltarget = (ModelWithChildren)(targetTree.Roots as IList)[0];

             //MoveObjectsToChildren (targetTree, sourceTree, modeltarget, toMove ) ;
            //if (sourceTree == targetTree) {
            //    foreach (ModelWithChildren x in toMove) {
            //        if (x.Parent != null) {
            //            x.Parent.Children.Remove(x);
            //            x.Parent = null;
            //            sourceTree.AddObject(x);
            //        }
            //    }
            //} else {
            //    foreach (ModelWithChildren x in toMove) {
            //        if (x.Parent == null) {
            //            sourceTree.RemoveObject(x);
            //        } else {
            //            x.Parent.Children.Remove(x);
            //        }
            //        x.Parent = null;
            //        targetTree.AddObject(x);
            //    }
            //}
        }

        /// <summary>
        /// Move the given collection of model so that they become children of the target
        /// </summary>
        /// <param name="targetTree"></param>
        /// <param name="sourceTree"></param>
        /// <param name="target"></param>
        /// <param name="toMove"></param>
        private void MoveObjectsToChildren(TreeListView targetTree, TreeListView sourceTree, ModelWithChildren target, IList toMove) {
            foreach (ModelWithChildren x in toMove)
            {
            //    if (x.Parent == null) 
            //        sourceTree.RemoveObject(x);
            //    else
            //        x.Parent.Children.Remove(x);
                ModelWithChildren y = x.ModelWithChildrenClone(x);
                //x.Parent = target;
                //target.Children.Add(x);
               

                if (target.LocalRemote == 1 && y.LocalRemote == 0)
                {
                    y.Parent = target;
                    target.Children.Add(y);

                    if (y.Update == "file")
                        ////sftpClient.DownloadFile(y.RemotePath, File.Create(target.LocalPath + "\\" + y.Label));
                        //ftpSessionGen.DownloadFile(y.RemotePath, File.Create(target.LocalPath + "\\" + y.Label));
                        ftpSessionGen.DownloadOne(y.RemotePath, target.LocalPath);
                        //if (ftpSessionGen.FtpMode == 0)
                        //    ftpSessionGen.sftpClient.DownloadFile(y.RemotePath, File.Create(target.LocalPath + "\\" + y.Label));
                        //else
                         
                        //    FtpClientFunc.FtpClientFuncClass.DoDownloadOneFile(ftpSessionGen.ftpClient,  @"/" + y.RemotePath , target.LocalPath);

                    if (y.Update == "dir")
                    {
                        ////Renci.SshNet.Tests.Classes.SftpClientTest sftpTest = new Renci.SshNet.Tests.Classes.SftpClientTest();
                        ////sftpTest.Sftp_Multiple_Async_Download_Files(sftpClient, target.LocalPath + "\\" + y.Label, y.RemotePath, "*.*");
                        ftpSessionGen.DownloadMultiple(target.LocalPath, y.RemotePath);
                        //ftpSessionGen.DownloadMultiple(target.LocalPath + "\\" + y.Label, y.RemotePath);
                        ////sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, @"G:\Bak\Sdcard\new_patch.zip", "*.*");
                    }
                    y.LocalRemote = 1;
                    y.LocalPath = target.LocalPath + "\\" + y.Label;
                }


                if (target.LocalRemote == 0 && y.LocalRemote == 1)
                {
                    y.Parent = target;
                    target.Children.Add(y);

                    if (y.Update == "file")
                        //if (ftpSessionGen.FtpMode == 0)
                        //    ftpSessionGen.sftpClient.UploadFile(File.OpenRead(y.LocalPath), (target.RemotePath == "") ? target.RemotePath + y.Label : target.RemotePath + @"/" + y.Label);
                        //else
                        //    FtpClientFunc.FtpClientFuncClass.DoUploadOneFile(ftpSessionGen.ftpClient, (target.RemotePath == "") ? target.RemotePath  : target.RemotePath + @"/" , y.LocalPath);
                        ftpSessionGen.UploadOne(target.RemotePath, y.LocalPath);
                    if (y.Update == "dir")
                    {
                        //Renci.SshNet.Tests.Classes.SftpClientTest sftpTest = new Renci.SshNet.Tests.Classes.SftpClientTest();
                        ////sftpTest.Sftp_Multiple_Async_Download_Files(sftpClient, @"G:\TestDownload", "Documents", "*.*");
                        //sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, y.LocalPath, (target.RemotePath == "") ? target.RemotePath + y.Label : target.RemotePath + @"/" + y.Label, "*.*");
                        //ftpSessionGen.UploadMultiple(y.LocalPath, (target.RemotePath == "") ? target.RemotePath + y.Label : target.RemotePath + @"/" + y.Label);
                        ftpSessionGen.UploadMultiple(y.LocalPath, (target.RemotePath == "") ? target.RemotePath  : target.RemotePath );
                    }

                    y.LocalRemote = 0;
                    y.RemotePath = target.RemotePath + @"/" + y.Label;
                }

 
            }
        }
 
        /// <summary>
        /// Move the given collection of model objects so that they become the siblings of the target.
        /// </summary>
        /// <param name="targetTree"></param>
        /// <param name="sourceTree"></param>
        /// <param name="target"></param>
        /// <param name="toMove"></param>
        /// <param name="siblingOffset">0 indicates that the siblings should appear before the target,
        /// 1 indicates that the siblings should appear after the target</param>
        private void MoveObjectsToSibling(TreeListView targetTree, TreeListView sourceTree, ModelWithChildren target, IList toMove, int siblingOffset) {

            // There are lots of things to get right here:
            // - sourceTree and targetTree may be the same
            // - target may be a root (which means that all moved objects will also become roots)
            // - one or more moved objects may be roots (which means the roots of the sourceTree will change)
            return; 

            ArrayList sourceRoots = sourceTree.Roots as ArrayList;
            ArrayList targetRoots = targetTree == sourceTree ? sourceRoots : targetTree.Roots as ArrayList;
            bool sourceRootsChanged = false;
            bool targetRootsChanged = false;

            // We want to make the moved objects to be siblings of the target. So, we have to 
            // remove the moved objects from their old parent and give them the same parent as the target.
            // If the target is a root, then the moved objects have to become roots too.
            foreach (ModelWithChildren x in toMove) {
                if (x.Parent == null) {
                    sourceRootsChanged = true;
                    sourceRoots.Remove(x);
                } else
                    x.Parent.Children.Remove(x);
                x.Parent = target.Parent;
            }

            // Now add to the moved objects to children of their parent (or to the roots collection
            // if the target is a root)
            if (target.Parent == null) {
                targetRootsChanged = true;
                targetRoots.InsertRange(targetRoots.IndexOf(target) + siblingOffset, toMove);
            } else {
                target.Parent.Children.InsertRange(target.Parent.Children.IndexOf(target) + siblingOffset, toMove.Cast<ModelWithChildren>());
            }
            if (targetTree == sourceTree) {
                if (sourceRootsChanged || targetRootsChanged)
                    sourceTree.Roots = sourceRoots;
            } else {
                if (sourceRootsChanged)
                    sourceTree.Roots = sourceRoots;
                if (targetRootsChanged)
                    targetTree.Roots = targetRoots;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            //this.treeListView1.RebuildAll(true);
            
            LocalPathRoot = txtLocalPathRoot.Text;

            if (IsConnected)
            {
                treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
                ModelWithChildren model1 = (ModelWithChildren)(treeListView1.Roots as IList)[0];
                treeListView1.Expand(model1);
            }
            
            if (nOnshut == 0)
            {
                treeListView2.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot, 1);
                ModelWithChildren model2 = (ModelWithChildren)(treeListView2.Roots as IList)[0];
                treeListView2.Expand(model2);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (treeListView1.SelectedObjects != null)
                this.treeListView1.RefreshObjects(this.treeListView1.SelectedObjects);
           
            //LocalPathRoot = txtLocalPathRoot.Text;

            //if (IsConnected)
            //{
            //    treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
            //    ModelWithChildren model1 = (ModelWithChildren)(treeListView1.Roots as IList)[0];
            //    treeListView1.Expand(model1); 
            //}

            
            ////treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
            //if (nOnshut == 0)
            //{
            //    treeListView2.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot, 1);
            //    //ModelWithChildren model1 = (ModelWithChildren)(treeListView1.Roots as IList)[0];
            //    ModelWithChildren model2 = (ModelWithChildren)(treeListView2.Roots as IList)[0];
            //    ////model2.LocalPath = LocalPathRoot;
            //    //treeListView1.Expand(model1);
            //    treeListView2.Expand(model2);
            //}
        }

        private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public IList SelectedRemotePaths;
        public IList SelectedLocalPaths;
        private void btnOK_Click(object sender, EventArgs e)
        {
            //foreach (ModelWithChildren mo  in treeListView1.SelectedObjects)
            SelectedRemotePaths = treeListView1.SelectedObjects;
            SelectedLocalPaths = treeListView2.SelectedObjects;

            ftpSessionGen.Disconnect();
            Close();
        }

        int nOnshut = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (nOnshut == 0)
            {
                button1_Click(null, null);
                nOnshut = 1;

                //for (int i = 0 ; i < this.treeListView1.Items.Count; i++)
                //    if (this.treeListView1.Items[i].SubItems[2].Text != "")
                //        this.treeListView1.Items[i].ForeColor= Color.Blue;
                
                
            }
            //for (int i = 0; i < this.treeListView1.Items.Count; i++)
            //{
            //    OLVListItem it = this.treeListView1.GetItem(i);
            //    if (it.SubItems[2].Text != "dir")
            //    {
            //        it.ForeColor = Color.Blue;
            //        it.ImageIndex = 1; // ImageSelector = "File_image";
            //        it.SubItems[0].ForeColor = Color.Blue;
            //        this.treeListView1.RefreshItem(it);
            //    }
            //}

            //ModelWithChildren model1 = (ModelWithChildren)(treeListView1.Roots as IList)[0];
            //foreach (ModelWithChildren model in model1.Children)
            //{
            //    if (model.Update == "file")
            //    {
            //        treeListView1.Expand(model);
            //    }    
                    
            //}
        }

        private void treeListView1_Expanded(object sender, TreeBranchExpandedEventArgs e)
        {
            //ModelWithChildren modelexpanded = (ModelWithChildren)e.Model;
            //if (modelexpanded.Children != null)
            //foreach ( ModelWithChildren model in modelexpanded.Children)
            //{
            //    if (model.Update == "file") treeListView1.Expand(model);
            //}

            //for (int i = 0; i < this.treeListView1.Items.Count; i++)
            //    if (this.treeListView1.Items[i].SubItems[2].Text == "file")
            //    {
            //        //this.treeListView1.Expand()
            //        this.treeListView1.Items[i].ForeColor = Color.Blue;
            //    }
                    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsConnected == false) return;

            ListView.SelectedListViewItemCollection lv = this.treeListView1.SelectedItems;
            OLVListItem oi = this.treeListView1.SelectedItem;
            object osel = this.treeListView1.SelectedObject;
            IList ilosel = this.treeListView1.SelectedObjects;
            ModelWithChildren model1 = null;
            if (ilosel != null)
                model1 = (ModelWithChildren)ilosel[0];
            ModelWithChildren model0 = (ModelWithChildren)osel;

            this.treeListView1.SelectedObject = model1.Parent;

            model1.Parent.Children.Remove(model1);

            if (model1 != null)
            {
                string remotepath = model1.RemotePath;
                bool isdir = (model1.Update == "dir") ;
                if (!isdir)
                {
                    //if (this.FtpMode == 0)
                    //    this.ftpSessionGen.sftpClient.DeleteFile(remotepath);
                    //if (this.FtpMode == 1)
                    //    ftpSessionGen.ftpClient.DeleteFile(remotepath);
                    ftpSessionGen.DeleteFile(remotepath);
                }
                else
                {
                    ftpSessionGen.DeleteDirectory(remotepath);
                    //if (this.FtpMode == 0)
                    //{
                    //    //this.ftpSessionGen.sftpClient.DeleteDirectory(remotepath);
                    //    RemoveDirectory(remotepath, ftpSessionGen);
                    //    ftpSessionGen.sftpClient.DeleteDirectory(remotepath);
                    //}
                    //if (this.FtpMode == 1)
                    //{
                    //    FtpClientFunc.FtpClientFuncClass.DeleteDirectory(ftpSessionGen.ftpClient, remotepath);
                    //    ftpSessionGen.ftpClient.DeleteDirectory(remotepath);
                    //}
                }
                //model1.Parent.Children.Remove(model1);
                ////oi.Remove();
                //treeListView1.Collapse(model1.Parent);
               
                //model1.Parent.Parent.Children.Clear();
                //(treeListView1.Roots as IList)[0] =  ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
                this.treeListView1.RefreshObjects(this.treeListView1.SelectedObjects);
                //treeListView1.Expand(model1.Parent);
            }

        }

        private void btnCreateDirectory_Click(object sender, EventArgs e)
        {
            if (IsConnected == false) return;

            if (!txtBoxDirNew.Visible)
            {
                txtBoxDirNew.Visible = true;
                return;
            }

            ListView.SelectedListViewItemCollection lv = this.treeListView1.SelectedItems;
            OLVListItem oi = this.treeListView1.SelectedItem;
            object osel = this.treeListView1.SelectedObject;
            IList ilosel = this.treeListView1.SelectedObjects;
            ModelWithChildren model1 = null;
            
               

            if (ilosel == null) return;
            if (ilosel.Count == 0) return;

            model1 = (ModelWithChildren)ilosel[0];
            ModelWithChildren model0 = (ModelWithChildren)osel;

            string newdir = "";
            string dirame = txtBoxDirNew.Text;

            newdir = (model1.RemotePath == "" || model1.RemotePath == @"\" || model1.RemotePath == @"/") ? dirame : model1.RemotePath + "/" + dirame;

            if (FtpMode == 0)
                ftpSessionGen.sftpClient.CreateDirectory(newdir);

            if (FtpMode == 1)
                ftpSessionGen.ftpClient.CreateDirectory(newdir);

            txtBoxDirNew.Visible = false;
        }

        private void btnSelDir_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileBrowswer = new System.Windows.Forms.FolderBrowserDialog();
            openFileBrowswer.SelectedPath = LocalPathRoot;
            if (openFileBrowswer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.LocalPathRoot = this.txtLocalPathRoot.Text = openFileBrowswer.SelectedPath;

                treeListView2.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot, 1);
                ModelWithChildren model2 = (ModelWithChildren)(treeListView2.Roots as IList)[0];
                ////model2.LocalPath = LocalPathRoot;
                //treeListView1.Expand(model1);
                treeListView2.Expand(model2);

            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ftpSessionGen.Disconnect();
            Close();
        }

        //static void RemoveDirectory(string remotepath, FtpSessionGen ftpSessionGen)
        //{
        //    var dirs = ftpSessionGen.sftpClient.ListDirectory(remotepath);
        //    foreach (SftpFile dir in dirs)
        //    {
        //        if (! dir.IsDirectory)
        //        {
        //            ftpSessionGen.sftpClient.DeleteFile(dir.FullName);  
        //        }
        //    }
        //    foreach (SftpFile dir in dirs)
        //    {
        //        if (dir.IsDirectory)
        //        {
        //            if (dir.Name != "." && dir.Name != "..")
        //            {
        //                RemoveDirectory(dir.FullName, ftpSessionGen);

        //                ftpSessionGen.sftpClient.DeleteDirectory(dir.FullName);
        //            }
        //        }
        //    }
        //    //ftpSessionGen.sftpClient.DeleteDirectory(remotepath);
        //}


    }

    
    
}
