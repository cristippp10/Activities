using System;
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
        private void Form1_Load(object sender, EventArgs e) {

            //string host = "192.168.0.104"; // TODO: Initialize to an appropriate value
            //string username = "one"; // TODO: Initialize to an appropriate value
            ////PrivateKeyFile[] keyFiles = null; // TODO: Initialize to an appropriate value
            //string password = "one";

            //sftpClient = new Renci.SshNet.SftpClient(host, port, username, password);
            //sftpClient.Connect();
            //sftpClient = new Renci.SshNet.SftpClient(host, port, username, password);
            ftpSessionGen = new FtpSessionGen(FtpMode, host, username, password, port);
            ftpSessionGen.Connect();

            // Remember: TreeListViews must have a small image list assigned to them.
            // If they do not, the hit testing will be wildly off
            olvColumn1.ImageGetter = delegate(object x) 
            {
                ModelWithChildren y = (ModelWithChildren)x; 
                if (y.Update == "file") 
                    return "File_image"; 
                else return "folder"; 
            }; //{ return "folder"; };
            //olvColumn3.ImageGetter = delegate(object x) { return "folder"; };
            olvColumn3.ImageGetter = delegate(object x)
            {
                ModelWithChildren y = (ModelWithChildren)x;
                if (y.Update == "file")
                    return "File_image";
                else return "folder";
            }; //{

            // Allow all models to be expanded and each model will show Children as its sub-branches
            treeListView1.CanExpandGetter = delegate(object x) 
            {
                ModelWithChildren y = (ModelWithChildren)x;
                if (y.Update == "file")
                    return false;
                else 
                    return true; 
            };
            treeListView1.ChildrenGetter = delegate(object x) { return ((ModelWithChildren)x).Children; };
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
            sink1.AcceptExternal = true;
            sink1.CanDropBetween = true;
            sink1.CanDropOnBackground = true;

            SimpleDropSink sink2 = (SimpleDropSink)treeListView2.DropSink;
            sink2.AcceptExternal = true;
            sink2.CanDropBetween = true;
            sink2.CanDropOnBackground = true;

            

            // Give each tree its top level objects to get things going
            //treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { 0, 1, 2, 3, 4, 5 });
            //ModelWithChildren mc1 = new ModelWithChildren();
            //ModelWithChildren mc2 = new ModelWithChildren();
            //LocalPathRoot = @"G:\Bak";
            treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
            treeListView2.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot, 1);

            txtLocalPathRoot.Text = LocalPathRoot;

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
                    if (sourceModels.Any(x => target.IsAncestor(x)))
                        e.InfoMessage = "Cannot drop on descendant (think of the temporal paradoxes!)";
                    else
                        e.Effect = DragDropEffects.Move;
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
                y.Parent = target;
                target.Children.Add(y);

                if (target.LocalRemote == 1 && y.LocalRemote == 0)
                {
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
                        //Renci.SshNet.Tests.Classes.SftpClientTest sftpTest = new Renci.SshNet.Tests.Classes.SftpClientTest();
                        //sftpTest.Sftp_Multiple_Async_Download_Files(sftpClient, target.LocalPath + "\\" + y.Label, y.RemotePath, "*.*");
                        ftpSessionGen.DownloadMultiple(target.LocalPath + "\\" + y.Label, y.RemotePath);
                        ////sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, @"G:\Bak\Sdcard\new_patch.zip", "*.*");
                    }
                    y.LocalRemote = 1;
                    y.LocalPath = target.LocalPath + "\\" + y.Label;
                }


                if (target.LocalRemote == 0 && y.LocalRemote == 1)
                {
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
                        ftpSessionGen.UploadMultiple(y.LocalPath, (target.RemotePath == "") ? target.RemotePath + y.Label : target.RemotePath + @"/" + y.Label);
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
            this.treeListView1.RebuildAll(true);
        }

        private void button2_Click(object sender, EventArgs e) {
            if (treeListView1.SelectedObjects != null)
                this.treeListView1.RefreshObjects(this.treeListView1.SelectedObjects);
           
            LocalPathRoot = txtLocalPathRoot.Text;
            treeListView1.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot);
            treeListView2.Roots = ModelWithChildren.CreateModels(null, new ArrayList { "" }, ftpSessionGen, LocalPathRoot, 1);
            ModelWithChildren model1 = (ModelWithChildren)(treeListView1.Roots as IList)[0];
            ModelWithChildren model2 = (ModelWithChildren)(treeListView2.Roots as IList)[0];
            //model2.LocalPath = LocalPathRoot;
            treeListView1.Expand(model1);
            treeListView2.Expand(model2);
        }

        private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            ftpSessionGen.Disconnect();
            Close();
        }

        int nOnshut = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (nOnshut == 0)
            {
                button2_Click(null, null);
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
            if (openFileBrowswer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtLocalPathRoot.Text = openFileBrowswer.SelectedPath;
            } 
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

    
    public class ModelWithChildren {

        public static FtpSessionGen ftpSessionGen;

        public static string LocalPathRoot;
        public string Now { get; set;  } //{ return DateTime.Now.ToShortDateString(); } }//DateTime Now { get { return DateTime.Now; } }
        public string Update { get; set; } //{ return lastChildrenFetch; } }
        private DateTime lastChildrenFetch;

        public string Label { get; set; }
        public ModelWithChildren Parent { get; set; }
        public string ParentLabel {
            get { return this.Parent == null ? "none" : this.Parent.Label; }
        }

        public ModelWithChildren() {}
        public ModelWithChildren ModelWithChildrenClone (ModelWithChildren m) {
            ModelWithChildren model = new ModelWithChildren();
            model.Label = this.Label;
            model.Update = this.Update;
            model.LocalRemote = this.LocalRemote;
            model.RemotePath = this.RemotePath;
            model.LocalPath = this.LocalPath;
            //model.LocalPathRoot = LocalPathRoot;
            model.DataForChildren = new ArrayList { "" }; 
            return model;       
        }
        public long ChildCount
        {
            get;
            set;
            //get
            //{
            //    return this.children == null ? 0 : this.children.Count;
            //}
            //set 
            //{
            //    ChildCount = value;
            //}
        }

        public string RemotePath
        {
            get;
            set;
        }
    
        public string LocalPath
        {
            get;
            set;
        }

        public int LocalRemote
        {
            get;
            set;
        }
        public List<ModelWithChildren> Children {
            get {
                lastChildrenFetch = DateTime.Now;
                if (this.children == null)
                    children = CreateModels(this, this.DataForChildren, ftpSessionGen, LocalPathRoot, LocalRemote );
                return this.children;
            }
            set { this.children = value; }
        }
        private List<ModelWithChildren> children;

        public ArrayList DataForChildren { get; set; }

        public bool IsAncestor(ModelWithChildren model) {
            if (this == model)
                return true;
            if (this.Parent == null)
                return false;
            return this.Parent.IsAncestor(model);
        }

        //public override string ToString()
        //{
        //    //return String.Format("{0}, parent: {1}", Label ?? "[]", ParentLabel);
        //    return String.Format(Label ?? "");
        //}
        public static List<ModelWithChildren> CreateModels(ModelWithChildren parent, ArrayList data, FtpSessionGen sftp, string localPathRoot, int localRemote = 0)
        {
            List<ModelWithChildren> models = new List<ModelWithChildren>();
            //foreach (object x in data) {
            //    models.Add(new ModelWithChildren {
            //        Label = (parent == null ? x.ToString() : parent.Label + "-" + x.ToString()),
            //        Parent = parent,
            //        DataForChildren = data
            //    });
            //}

            //string host = "192.168.0.104"; // TODO: Initialize to an appropriate value
            //string username = "one"; // TODO: Initialize to an appropriate value
            ////PrivateKeyFile[] keyFiles = null; // TODO: Initialize to an appropriate value
            //string password = "one";

            //sftpClient = new Renci.SshNet.SftpClient(host, 22, username, password);
            //sftpClient.Connect();
           
            LocalPathRoot = localPathRoot;
            ftpSessionGen = sftp;
            if (parent == null)
            {
                //var files = sftpClient.ListDirectory("\\");
                //data.Clear();
                //foreach (Renci.SshNet.Sftp.SftpFile f in files)
                //    data.Add(f.Name);

                foreach (object x in data)
                {
                    models.Add(new ModelWithChildren
                    {
                        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
                        Label = (parent == null ? x.ToString() : x.ToString()),
                        RemotePath = (parent == null ? x.ToString() : x.ToString()),
                        Parent = parent,
                        LocalRemote = localRemote,
                        LocalPath = localPathRoot,
                        DataForChildren = new ArrayList { x.ToString() } //data
                    });
                }
            }
            else
            {
                if (parent.Update == "file") return null;

                string pathremote = "";
                if (parent.LocalRemote == 0)
                {
                    if (parent.Label.ToString() == "root dir")
                        pathremote = "\\";
                    else
                        //pathremote = parent.Label.ToString();
                        pathremote = parent.RemotePath.ToString();
                }   
                string localpath = "";
                if (parent.LocalRemote == 1)
                {
                    if (parent.Label.ToString() == "")
                        localpath = localPathRoot; //"G:\\BAK";
                    else
                        //pathremote = parent.Label.ToString();
                        localpath = parent.LocalPath.ToString();
                }
                //sftpClient.ChangeDirectory(pathremote);
                
               

                if (parent.LocalRemote == 0)
                {
                    var files = ftpSessionGen.GetFiles(pathremote); //ListDirectory(pathremote);
                

                    data.Clear();
                    //foreach (Renci.SshNet.Sftp.SftpFile f in files)
                    foreach (FtpFileClass f in files)
                        if (f.IsDirectory && f.Name != "." && f.Name != "..")
                        {
                            string path = "";
                            if (parent.Label == "")
                                path = f.Name;
                            else
                                path = parent.RemotePath + @"/" + f.Name;

                            models.Add(new ModelWithChildren
                            {
                                //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
                                Label = (parent == null ? f.Name : f.Name), //path
                                RemotePath = path,
                                Parent = parent,
                                Now = f.LastWriteTime.ToShortDateString(),
                                //ChildCount = (Children == null ? 0 : Children.Count),
                                LocalRemote = 0,
                                Update = "dir",
                                DataForChildren = new ArrayList { f.Name } //data
                            });
                        }
                    
                    //foreach (object x in data)
                    //{
                    //    string path = "";
                    //    if (parent.Label == "")
                    //        path = x.ToString();
                    //    else
                    //        path = parent.RemotePath + @"/" + x.ToString();

                    //    models.Add(new ModelWithChildren
                    //    {
                    //        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
                    //        Label = (parent == null ? x.ToString() : x.ToString()), //path
                    //        RemotePath = path,
                    //        Parent = parent,
                    //        LocalRemote = 0,
                    //        Update = "dir",
                    //        DataForChildren = new ArrayList { x.ToString() } //data
                    //    });
                    //}


                    //files
                    data.Clear();

                    //foreach (Renci.SshNet.Sftp.SftpFile f in files)
                    foreach (FtpFileClass f in files)
                        if (f.IsRegularFile && !f.IsDirectory && f.Name != "." && f.Name != "..")
                        //data.Add(f.Name);
                        //foreach (object x in data)
                        {
                            string path = "";
                            if (parent.Label == "")
                                path = f.Name;
                            else
                                path = parent.RemotePath + @"/" + f.Name;

                            models.Add(new ModelWithChildren
                            {
                                //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
                                Label = (parent == null ? f.Name : f.Name) , //x.ToString() : x.ToString()), //path,
                                RemotePath = path,
                                Parent = parent,
                                Update = "file",
                                Now = f.LastWriteTime.ToShortDateString(),
                                ChildCount = f.Length,
                                LocalRemote = 0,
                                Children = null,
                                DataForChildren = null //data
                            });
                        }
                    //foreach (Renci.SshNet.Sftp.SftpFile f in files)
                    //    if (f.IsRegularFile && f.Name != "." && f.Name != "..")
                    //        data.Add(f.Name);

                    //foreach (object x in data)
                    //{
                    //    string path = "";
                    //    if (parent.Label == "")
                    //        path = x.ToString();
                    //    else
                    //        path = parent.RemotePath + @"/" + x.ToString();

                    //    models.Add(new ModelWithChildren
                    //    {
                    //        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
                    //        Label = (parent == null ? x.ToString() : x.ToString()), //path,
                    //        RemotePath = path,
                    //        Parent = parent,
                    //        Update = "file",
                    //        //Now = ,
                    //        LocalRemote = 0,
                    //        Children = null,
                    //        DataForChildren = null //data
                    //    });
                    //}

                }


                if (parent.LocalRemote == 1)
                {
                    string[] dirs = Directory.GetDirectories(localpath);
                    //LocalPathRoot = localpath;

                    data.Clear();
                    //foreach (string s in dirs)
                    //        data.Add(s);

                    foreach (string dir in dirs)
                    {
                        string path = "";
                        string folderpath = "";
                        //if (parent.Label == "")
                        //    path = x.ToString();
                        //else
                        path = dir; // x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
                        if (path.LastIndexOf("\\") > 1)
                            folderpath = path.Substring(path.LastIndexOf("\\") + 1);
                        else
                            folderpath = path;

                        DateTime datetimedir = Directory.GetLastWriteTime(dir);

                        models.Add(new ModelWithChildren
                        {
                            Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
                            LocalPath = path,
                            Parent = parent,
                            LocalRemote = 1,
                            Update = "dir",
                            Now = datetimedir.ToShortDateString(),
                            DataForChildren = new ArrayList { dir } //data
                        });
                    }

                    data.Clear();
                    if (parent.LocalPath != null)
                    {
                        string[] filesindir = Directory.GetFiles(parent.LocalPath); 
                        //foreach (string sfi in filesindir)
                        // data.Add(sfi);

                        foreach (string x in filesindir)
                        {
                            string path = "";
                            string folderpath = "";
                            //if (parent.Label == "")
                            //    path = x.ToString();
                            //else
                            path = x; // x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
                            if (path.LastIndexOf("\\") > 1)
                                folderpath = path.Substring(path.LastIndexOf("\\") + 1);
                            else
                                folderpath = path;
                            DateTime datetimefile = File.GetCreationTime(path);
                            //long sizefile = 
                            FileInfo fileinfo = new FileInfo(path); 
                            models.Add(new ModelWithChildren
                            {
                                Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
                                LocalPath = path,
                                Parent = parent,
                                LocalRemote = 1,
                                Update = "file",
                                Now = datetimefile.ToShortDateString(),
                                ChildCount = fileinfo.Length,
                                DataForChildren = new ArrayList { x } //data
                            });
                        }

                        
                    //foreach (object x in data)
                    //{
                    //    string path = "";
                    //    string folderpath = "";
                    //    //if (parent.Label == "")
                    //    //    path = x.ToString();
                    //    //else
                    //    path = x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
                    //    if (path.LastIndexOf("\\") > 1)
                    //        folderpath = path.Substring(path.LastIndexOf("\\") + 1);
                    //    else
                    //        folderpath = path;

                    //    models.Add(new ModelWithChildren
                    //    {
                    //        Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
                    //        LocalPath = path,
                    //        Parent = parent,
                    //        LocalRemote = 1,
                    //        Update = "dir",
                    //        DataForChildren = new ArrayList { x.ToString() } //data
                    //    });
                    //}

                    //data.Clear();
                    //if (parent.LocalPath != null)
                    //{
                    //    var filesindir = Directory.GetFiles(parent.LocalPath); 
                    //    foreach (string sfi in filesindir)
                    //     data.Add(sfi);

                    //    foreach (object x in data)
                    //    {
                    //        string path = "";
                    //        string folderpath = "";
                    //        //if (parent.Label == "")
                    //        //    path = x.ToString();
                    //        //else
                    //        path = x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
                    //        if (path.LastIndexOf("\\") > 1)
                    //            folderpath = path.Substring(path.LastIndexOf("\\") + 1);
                    //        else
                    //            folderpath = path;

                    //        models.Add(new ModelWithChildren
                    //        {
                    //            Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
                    //            LocalPath = path,
                    //            Parent = parent,
                    //            LocalRemote = 1,
                    //            Update = "file",
                    //            DataForChildren = new ArrayList { x.ToString() } //data
                    //        });
                    //    }


                    //    //foreach (object x in data)
                    //    //{
                    //    //    string path = "";
                    //    //    if (parent.Label == "")
                    //    //        path = x.ToString();
                    //    //    else
                    //    //        path = parent.RemotePath + @"/" + x.ToString();

                    //    //    models.Add(new ModelWithChildren
                    //    //    {
                    //    //        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
                    //    //        Label = (parent == null ? x.ToString() : x.ToString()), //path,
                    //    //        RemotePath = path,
                    //    //        Parent = parent,
                    //    //        Update = "file",
                    //    //        LocalRemote = 0,
                    //    //        Children = null,
                    //    //        DataForChildren = null //data
                    //    //    });
                    //    //}
                    }
                }
            }
            

            

            return models;
        }
    }
}
