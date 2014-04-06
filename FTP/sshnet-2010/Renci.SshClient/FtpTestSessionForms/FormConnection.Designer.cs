namespace TreeListViewDragDrop
{
    partial class FormConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkFtp = new System.Windows.Forms.CheckBox();
            this.chkSftp = new System.Windows.Forms.CheckBox();
            this.groupBoxKeys = new System.Windows.Forms.GroupBox();
            this.btnDelKey = new System.Windows.Forms.Button();
            this.btnAddKey = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSelFile = new System.Windows.Forms.Button();
            this.txtPassKey = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colKeytype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKeyPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnTest = new System.Windows.Forms.Button();
            this.lblConnectionState = new System.Windows.Forms.Label();
            this.txtConnectionInfo = new System.Windows.Forms.TextBox();
            this.chkKeys = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxKeys.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(18, 382);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Apply";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(123, 382);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.lblPass);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtHost);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 307);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(133, 117);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(109, 22);
            this.txtPassword.TabIndex = 9;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(63, 117);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(69, 17);
            this.lblPass.TabIndex = 8;
            this.lblPass.Text = "Password";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(133, 84);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(109, 22);
            this.txtUser.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "User";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(133, 50);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(33, 22);
            this.txtPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(133, 17);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(109, 22);
            this.txtHost.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkFtp);
            this.groupBox2.Controls.Add(this.chkSftp);
            this.groupBox2.Location = new System.Drawing.Point(12, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 40);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // chkFtp
            // 
            this.chkFtp.AutoSize = true;
            this.chkFtp.Location = new System.Drawing.Point(186, 12);
            this.chkFtp.Name = "chkFtp";
            this.chkFtp.Size = new System.Drawing.Size(56, 21);
            this.chkFtp.TabIndex = 1;
            this.chkFtp.Text = "FTP";
            this.chkFtp.UseVisualStyleBackColor = true;
            this.chkFtp.CheckedChanged += new System.EventHandler(this.chkFtp_CheckedChanged);
            // 
            // chkSftp
            // 
            this.chkSftp.AutoSize = true;
            this.chkSftp.Location = new System.Drawing.Point(66, 13);
            this.chkSftp.Name = "chkSftp";
            this.chkSftp.Size = new System.Drawing.Size(65, 21);
            this.chkSftp.TabIndex = 0;
            this.chkSftp.Text = "SFTP";
            this.chkSftp.UseVisualStyleBackColor = true;
            this.chkSftp.CheckedChanged += new System.EventHandler(this.chkSftp_CheckedChanged);
            // 
            // groupBoxKeys
            // 
            this.groupBoxKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKeys.Controls.Add(this.btnDelKey);
            this.groupBoxKeys.Controls.Add(this.btnAddKey);
            this.groupBoxKeys.Controls.Add(this.groupBox4);
            this.groupBoxKeys.Controls.Add(this.listView1);
            this.groupBoxKeys.Location = new System.Drawing.Point(364, 101);
            this.groupBoxKeys.Name = "groupBoxKeys";
            this.groupBoxKeys.Size = new System.Drawing.Size(400, 262);
            this.groupBoxKeys.TabIndex = 9;
            this.groupBoxKeys.TabStop = false;
            this.groupBoxKeys.Text = "Keys (DSA, RSA)";
            this.groupBoxKeys.Visible = false;
            // 
            // btnDelKey
            // 
            this.btnDelKey.Location = new System.Drawing.Point(100, 113);
            this.btnDelKey.Name = "btnDelKey";
            this.btnDelKey.Size = new System.Drawing.Size(94, 23);
            this.btnDelKey.TabIndex = 5;
            this.btnDelKey.Text = "Delete Key";
            this.btnDelKey.UseVisualStyleBackColor = true;
            this.btnDelKey.Click += new System.EventHandler(this.btnDelKey_Click);
            // 
            // btnAddKey
            // 
            this.btnAddKey.Location = new System.Drawing.Point(6, 113);
            this.btnAddKey.Name = "btnAddKey";
            this.btnAddKey.Size = new System.Drawing.Size(88, 23);
            this.btnAddKey.TabIndex = 4;
            this.btnAddKey.Text = "Add Key";
            this.btnAddKey.UseVisualStyleBackColor = true;
            this.btnAddKey.Click += new System.EventHandler(this.btnAddKey_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSelFile);
            this.groupBox4.Controls.Add(this.txtPassKey);
            this.groupBox4.Controls.Add(this.txtFile);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.lblFile);
            this.groupBox4.Location = new System.Drawing.Point(3, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(394, 86);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "File Key/ Key password";
            // 
            // btnSelFile
            // 
            this.btnSelFile.Location = new System.Drawing.Point(358, 21);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(30, 23);
            this.btnSelFile.TabIndex = 4;
            this.btnSelFile.Text = "..";
            this.btnSelFile.UseVisualStyleBackColor = true;
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // txtPassKey
            // 
            this.txtPassKey.Location = new System.Drawing.Point(73, 50);
            this.txtPassKey.Name = "txtPassKey";
            this.txtPassKey.Size = new System.Drawing.Size(90, 22);
            this.txtPassKey.TabIndex = 3;
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(73, 22);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(279, 22);
            this.txtFile.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(7, 22);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(30, 17);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "File";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colKeytype,
            this.colFile,
            this.colKeyPassword,
            this.colKey});
            this.listView1.Location = new System.Drawing.Point(0, 142);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(394, 124);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // colKeytype
            // 
            this.colKeytype.Text = "Key Type";
            this.colKeytype.Width = 100;
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            this.colFile.Width = 200;
            // 
            // colKeyPassword
            // 
            this.colKeyPassword.Text = "KeyPass";
            this.colKeyPassword.Width = 80;
            // 
            // colKey
            // 
            this.colKey.Text = "Key";
            this.colKey.Width = 150;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(232, 382);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblConnectionState
            // 
            this.lblConnectionState.AutoSize = true;
            this.lblConnectionState.Location = new System.Drawing.Point(367, 15);
            this.lblConnectionState.Name = "lblConnectionState";
            this.lblConnectionState.Size = new System.Drawing.Size(120, 17);
            this.lblConnectionState.TabIndex = 11;
            this.lblConnectionState.Text = "Connection State:";
            // 
            // txtConnectionInfo
            // 
            this.txtConnectionInfo.Location = new System.Drawing.Point(370, 36);
            this.txtConnectionInfo.Name = "txtConnectionInfo";
            this.txtConnectionInfo.Size = new System.Drawing.Size(382, 22);
            this.txtConnectionInfo.TabIndex = 12;
            this.txtConnectionInfo.Visible = false;
            // 
            // chkKeys
            // 
            this.chkKeys.AutoSize = true;
            this.chkKeys.Location = new System.Drawing.Point(374, 73);
            this.chkKeys.Name = "chkKeys";
            this.chkKeys.Size = new System.Drawing.Size(61, 21);
            this.chkKeys.TabIndex = 13;
            this.chkKeys.Text = "Keys";
            this.chkKeys.UseVisualStyleBackColor = true;
            this.chkKeys.CheckedChanged += new System.EventHandler(this.chkKeys_CheckedChanged_1);
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 423);
            this.Controls.Add(this.chkKeys);
            this.Controls.Add(this.txtConnectionInfo);
            this.Controls.Add(this.lblConnectionState);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBoxKeys);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FormConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxKeys.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkFtp;
        private System.Windows.Forms.CheckBox chkSftp;
        private System.Windows.Forms.GroupBox groupBoxKeys;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colKeytype;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.Button btnDelKey;
        private System.Windows.Forms.Button btnAddKey;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSelFile;
        private System.Windows.Forms.TextBox txtPassKey;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ColumnHeader colKeyPassword;
        private System.Windows.Forms.ColumnHeader colKey;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblConnectionState;
        private System.Windows.Forms.TextBox txtConnectionInfo;
        private System.Windows.Forms.CheckBox chkKeys;
    }
}