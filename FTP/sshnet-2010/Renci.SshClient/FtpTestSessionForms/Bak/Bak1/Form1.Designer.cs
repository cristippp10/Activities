namespace TreeListViewDragDrop {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtBoxDirNew = new System.Windows.Forms.TextBox();
            this.btnCreateDirectory = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSelDir = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtLocalPathRoot = new System.Windows.Forms.TextBox();
            this.lblLocalPathRoot = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.treeListView2 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn20 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtBoxDirNew);
            this.splitContainer1.Panel1.Controls.Add(this.btnCreateDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.treeListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSelDir);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.txtLocalPathRoot);
            this.splitContainer1.Panel2.Controls.Add(this.lblLocalPathRoot);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.treeListView2);
            this.splitContainer1.Size = new System.Drawing.Size(948, 485);
            this.splitContainer1.SplitterDistance = 528;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtBoxDirNew
            // 
            this.txtBoxDirNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtBoxDirNew.Location = new System.Drawing.Point(271, 445);
            this.txtBoxDirNew.Name = "txtBoxDirNew";
            this.txtBoxDirNew.Size = new System.Drawing.Size(116, 22);
            this.txtBoxDirNew.TabIndex = 4;
            this.txtBoxDirNew.Visible = false;
            // 
            // btnCreateDirectory
            // 
            this.btnCreateDirectory.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCreateDirectory.Location = new System.Drawing.Point(186, 445);
            this.btnCreateDirectory.Name = "btnCreateDirectory";
            this.btnCreateDirectory.Size = new System.Drawing.Size(79, 23);
            this.btnCreateDirectory.TabIndex = 3;
            this.btnCreateDirectory.Text = "CreateDir";
            this.btnCreateDirectory.UseVisualStyleBackColor = true;
            this.btnCreateDirectory.Click += new System.EventHandler(this.btnCreateDirectory_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(440, 447);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(96, 442);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "Refresh Selected";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(17, 442);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Rebuild All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.AllColumns.Add(this.olvColumn7);
            this.treeListView1.AllColumns.Add(this.olvColumn8);
            this.treeListView1.AllColumns.Add(this.olvColumn2);
            this.treeListView1.AllColumns.Add(this.olvColumn5);
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn2,
            this.olvColumn5});
            this.treeListView1.EmptyListMsg = " connection not ok";
            this.treeListView1.HideSelection = false;
            this.treeListView1.IsSimpleDragSource = true;
            this.treeListView1.IsSimpleDropSink = true;
            this.treeListView1.Location = new System.Drawing.Point(-2, 3);
            this.treeListView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(527, 419);
            this.treeListView1.SmallImageList = this.imageList1;
            this.treeListView1.TabIndex = 0;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            this.treeListView1.Expanded += new System.EventHandler<BrightIdeasSoftware.TreeBranchExpandedEventArgs>(this.treeListView1_Expanded);
            this.treeListView1.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.HandleCanDrop);
            this.treeListView1.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.HandleDropped);
            this.treeListView1.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.HandleModelCanDrop);
            this.treeListView1.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.HandleModelDropped);
            this.treeListView1.SelectedIndexChanged += new System.EventHandler(this.treeListView1_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Label";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Text = "Server File/Folder";
            this.olvColumn1.Width = 275;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Now";
            this.olvColumn7.AspectToStringFormat = "";
            this.olvColumn7.CellPadding = null;
            this.olvColumn7.Text = "Date";
            this.olvColumn7.Width = 87;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "Update";
            this.olvColumn8.CellPadding = null;
            this.olvColumn8.Text = "Type";
            this.olvColumn8.Width = 92;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ChildCount";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Text = "Size";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn2.Width = 90;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "ParentLabel";
            this.olvColumn5.CellPadding = null;
            this.olvColumn5.Text = "Parent";
            this.olvColumn5.Width = 58;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder");
            this.imageList1.Images.SetKeyName(1, "File_image");
            // 
            // btnSelDir
            // 
            this.btnSelDir.Location = new System.Drawing.Point(360, 7);
            this.btnSelDir.Name = "btnSelDir";
            this.btnSelDir.Size = new System.Drawing.Size(30, 23);
            this.btnSelDir.TabIndex = 6;
            this.btnSelDir.Text = "..";
            this.btnSelDir.UseVisualStyleBackColor = true;
            this.btnSelDir.Click += new System.EventHandler(this.btnSelDir_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(146, 442);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtLocalPathRoot
            // 
            this.txtLocalPathRoot.Enabled = false;
            this.txtLocalPathRoot.Location = new System.Drawing.Point(122, 7);
            this.txtLocalPathRoot.Name = "txtLocalPathRoot";
            this.txtLocalPathRoot.Size = new System.Drawing.Size(230, 22);
            this.txtLocalPathRoot.TabIndex = 3;
            // 
            // lblLocalPathRoot
            // 
            this.lblLocalPathRoot.AutoSize = true;
            this.lblLocalPathRoot.Location = new System.Drawing.Point(12, 9);
            this.lblLocalPathRoot.Name = "lblLocalPathRoot";
            this.lblLocalPathRoot.Size = new System.Drawing.Size(109, 17);
            this.lblLocalPathRoot.TabIndex = 2;
            this.lblLocalPathRoot.Text = "Local Path Root";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.EnableAutoDragDrop = true;
            this.richTextBox1.Location = new System.Drawing.Point(0, 372);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ShowSelectionMargin = true;
            this.richTextBox1.Size = new System.Drawing.Size(382, 50);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Drag ";
            this.richTextBox1.Visible = false;
            // 
            // treeListView2
            // 
            this.treeListView2.AllColumns.Add(this.olvColumn3);
            this.treeListView2.AllColumns.Add(this.olvColumn20);
            this.treeListView2.AllColumns.Add(this.olvColumn4);
            this.treeListView2.AllColumns.Add(this.olvColumn6);
            this.treeListView2.AllColumns.Add(this.olvColumn9);
            this.treeListView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumn20,
            this.olvColumn4,
            this.olvColumn6,
            this.olvColumn9});
            this.treeListView2.EmptyListMsg = "path not ok";
            this.treeListView2.HideSelection = false;
            this.treeListView2.IsSimpleDragSource = true;
            this.treeListView2.IsSimpleDropSink = true;
            this.treeListView2.Location = new System.Drawing.Point(0, 35);
            this.treeListView2.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView2.Name = "treeListView2";
            this.treeListView2.OwnerDraw = true;
            this.treeListView2.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.treeListView2.ShowGroups = false;
            this.treeListView2.Size = new System.Drawing.Size(387, 310);
            this.treeListView2.SmallImageList = this.imageList1;
            this.treeListView2.TabIndex = 0;
            this.treeListView2.UseCompatibleStateImageBehavior = false;
            this.treeListView2.View = System.Windows.Forms.View.Details;
            this.treeListView2.VirtualMode = true;
            this.treeListView2.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.HandleCanDrop);
            this.treeListView2.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.HandleDropped);
            this.treeListView2.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.HandleModelCanDrop);
            this.treeListView2.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.HandleModelDropped);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Label";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.Text = "Local File/Folder";
            this.olvColumn3.Width = 240;
            // 
            // olvColumn20
            // 
            this.olvColumn20.AspectName = "Now";
            this.olvColumn20.CellPadding = null;
            this.olvColumn20.Text = "Date";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Update";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.Text = "Type";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "ChildCount";
            this.olvColumn6.CellPadding = null;
            this.olvColumn6.Text = "Size";
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "ParentLabel";
            this.olvColumn9.CellPadding = null;
            this.olvColumn9.Text = "ParentLabel";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 485);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Sftp SSh Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.TreeListView treeListView2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.Button button2;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtLocalPathRoot;
        private System.Windows.Forms.Label lblLocalPathRoot;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timer1;
        private BrightIdeasSoftware.OLVColumn olvColumn20;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCreateDirectory;
        private System.Windows.Forms.TextBox txtBoxDirNew;
        private System.Windows.Forms.Button btnSelDir;

    }
}

