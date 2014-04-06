using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Collections;

using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Sftp;

using System.Net.FtpClient;
using FtpClientFunc;

namespace TreeListViewDragDrop
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        public Form1 Parent;

        public string host; // TODO: Initialize to an appropriate value
        public string username; // TODO: Initialize to an appropriate value
        //PrivateKeyFile[] keyFiles = null; // TODO: Initialize to an appropriate value
        public string password;
        public int port;
        public string LocalPathRoot;
        public int FtpMode;

        public bool ConnectionIsOk;

        public PrivateKeyFile[] keyFiles = null;
        public ArrayList aKeyFiles = new ArrayList();
        public string sKeyFiles = @"G:\Bak\DSA key.txt|G:\Bak\DSA key pass.txt<tester>|G:\Bak\RSA key.txt|G:\Bak\RSA key pass.txt<tester>" ;
        private void FormConnection_Load(object sender, EventArgs e)
        {
            host = txtHost.Text = Parent.host;
            port = Parent.port; 
            txtPort.Text = port.ToString();
            username = txtUser.Text = Parent.username;
            password = txtPassword.Text = Parent.password;
            FtpMode = Parent.FtpMode;
           
            if (sKeyFiles != null && sKeyFiles != "")
                sKeyFiles = Parent.sKeyFiles;

            if (FtpMode == 0)
            {
                chkSftp.Checked = true;
                chkFtp.Checked = false;
            }
            else
            {
                chkSftp.Checked = false;
                chkFtp.Checked = true;
            }
            
            listView1.View = System.Windows.Forms.View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            string[] aFiles = sKeyFiles.Split('|');
            foreach (string f in aFiles)
            {
                string filename = f;
                string pass = "";
                if ((f.IndexOf("<") > 0) && (f.IndexOf(">") == f.Length - 1))
                {
                    pass = f.Substring(f.IndexOf("<") + 1, f.IndexOf(">") - f.IndexOf("<") -1);
                    filename = f.Substring(0, f.IndexOf("<"));
                }
                
                //if (!File.Exists(filename))
                //{
                //    MessageBox.Show("Invalid File name");
                //    return;
                //}
                string hostkey = File.ReadAllText(filename);
                string passkey = pass;
                string typeRSADSA = "";

                if (hostkey.Contains("DSA")) typeRSADSA = "DSA";
                if (hostkey.Contains("RSA")) typeRSADSA = "RSA";

                PrivateKeyFile pk = null;
                try
                {
                    if (hostkey.Contains("ENCRYPTED"))
                    {
                        pk = new PrivateKeyFile(new MemoryStream(
                         Encoding.ASCII.GetBytes(hostkey)), passkey);
                        typeRSADSA = typeRSADSA + " with pass";
                    }
                    else
                    {
                        pk = new PrivateKeyFile(new MemoryStream(
                       Encoding.ASCII.GetBytes(hostkey)));
                        passkey = "";
                    }
                }
                catch (Exception ex)
                {
                    string mess = ex.Message;
                }

                if (pk != null)
                {
                    aKeyFiles.Add(pk);

                    ListViewItem li = new ListViewItem();
                    li.Text = typeRSADSA;
                    li.SubItems.Add(filename);
                    li.SubItems.Add(passkey);
                    li.SubItems.Add(hostkey);

                    listView1.Items.Add(li);

                }
                //else
                //    MessageBox.Show("Invalid File Key");
            }

            Array a = aKeyFiles.ToArray(typeof(PrivateKeyFile));
            keyFiles = a as PrivateKeyFile[];

            if (keyFiles != null) { chkKeys.Checked = true; groupBoxKeys.Enabled = true; }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkKeys_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void btnSelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select a file";
            openFileDialog1.Filter = "Files|*.*";

            openFileDialog1.ShowDialog();
            txtFile.Text = openFileDialog1.FileName;

            //ListViewItem li = new ListViewItem();
            //li.Text = "111";
            
            //li.SubItems.Add("222");
            //li.SubItems.Add("333");

            //listView1.Items.Add(li);

        }

        private void btnAddKey_Click(object sender, EventArgs e)
        {
            string filename = txtFile.Text;
            if (!File.Exists(filename) ) 
            {
                MessageBox.Show("Invalid File name");
                return;
            }
            string hostkey = File.ReadAllText(filename);
            string passkey = txtPassKey.Text;
            string typeRSADSA = "";
            string mess = "";

            if (hostkey.Contains("DSA")) typeRSADSA = "DSA";
            if (hostkey.Contains("RSA")) typeRSADSA = "RSA";
            
            PrivateKeyFile pk = null;
            try
            {
                if (hostkey.Contains("ENCRYPTED"))
                {
                    pk = new PrivateKeyFile(new MemoryStream(
                     Encoding.ASCII.GetBytes(hostkey)), passkey);
                    typeRSADSA = typeRSADSA + " with pass";
                }
                else
                {
                    pk = new PrivateKeyFile(new MemoryStream(
                   Encoding.ASCII.GetBytes(hostkey)));
                    passkey = "";
                }
            }
            catch (Exception ex)
            {
                mess = ex.Message;
            }

            if (pk != null)
            {
                aKeyFiles.Add(pk);

                ListViewItem li = new ListViewItem();
                li.Text = typeRSADSA;
                li.SubItems.Add(filename);
                li.SubItems.Add(passkey);
                li.SubItems.Add(hostkey);

                listView1.Items.Add(li);

            }
            else
                MessageBox.Show(mess); //("Invalid File Key");

              
            Array a = aKeyFiles.ToArray(typeof(PrivateKeyFile));
            
            sKeyFiles = "";
            int nKeys = listView1.Items.Count;
            for (int i = 0; i < nKeys; i++)
            {
                string psw = listView1.Items[i].SubItems[2].Text.Trim();
                if (sKeyFiles != "") sKeyFiles = sKeyFiles + "|"; 
                sKeyFiles = sKeyFiles + listView1.Items[i].SubItems[1].Text + ((psw != "") ? "<" + psw + ">" : "");
            }
 
            
            keyFiles = a as PrivateKeyFile[];

            txtFile.Text = "";
        }

        private void btnDelKey_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems == null) return;
            if (listView1.SelectedIndices == null) return;
            if (listView1.SelectedIndices.Count < 1) return;

            int nIndex = listView1.SelectedIndices[0];
            ListViewItem li = listView1.SelectedItems[0];
            string filename = li.SubItems[1].Text;
            li.Remove();
            aKeyFiles.RemoveAt(nIndex);
            Array a = aKeyFiles.ToArray(typeof(PrivateKeyFile));
            keyFiles = a as PrivateKeyFile[];

            sKeyFiles = "";
            int nKeys = listView1.Items.Count;
            for (int i = 0; i < nKeys; i++)
            {
                string psw = listView1.Items[i].SubItems[2].Text.Trim();
                if (sKeyFiles != "") sKeyFiles = sKeyFiles + "|";
                sKeyFiles = sKeyFiles + listView1.Items[i].SubItems[1].Text + ((psw != "") ? "<" + psw + ">" : "");
            }
        }

        void VerifyConnection()
        {
            ConnectionIsOk = false;
            SftpClient sftpClientTest = null;
            string sExMess = "";

            this.txtConnectionInfo.Visible = true;

            try
            {
                int port = Convert.ToInt32(txtPort.Text);
                host = txtHost.Text;
                username = txtUser.Text;
                password = txtPassword.Text;
                if ((keyFiles != null && aKeyFiles.Count > 0) && this.chkKeys.Checked)
                {
                    sftpClientTest = new SftpClient(this.txtHost.Text, port, this.txtUser.Text, keyFiles);
                }
                else
                    sftpClientTest = new SftpClient(this.txtHost.Text, port, this.txtUser.Text, this.txtPassword.Text);

                sftpClientTest.Connect();
                this.txtConnectionInfo.Text = "Connection is OK";
                ConnectionIsOk = true;

                if (ConnectionIsOk) sftpClientTest.Disconnect();
            }
            catch (Exception ex)
            {
                sExMess = ex.Message;
                this.txtConnectionInfo.Text = sExMess;
                ConnectionIsOk = false;
            }
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            VerifyConnection();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            VerifyConnection();
            if (!this.ConnectionIsOk)
            {
                MessageBox.Show("Connection is not ok");
            }
            else
            {
                if (chkSftp.Checked)
                    FtpMode = 0;
                else
                    FtpMode = 1;
                //btnOk.DialogResult = DialogResult.OK;
            }
        }

        private void chkSftp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSftp.Checked)
            { chkFtp.Checked = false; if (chkKeys.Checked) {groupBoxKeys.Enabled = true;} chkKeys.Enabled = true; }
            else
                { chkFtp.Checked = true; groupBoxKeys.Enabled = false; chkKeys.Enabled = false; }
        }

        private void chkFtp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFtp.Checked)
                { chkSftp.Checked = false; groupBoxKeys.Enabled = false; chkKeys.Enabled = false; }
            else
                { chkSftp.Checked = true; if (chkKeys.Checked) { groupBoxKeys.Enabled = true; } chkKeys.Enabled = true; }
        }

        private void chkKeys_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkKeys.Checked) 
                groupBoxKeys.Enabled = true; 
            else
                groupBoxKeys.Enabled = false; 

        }

    }
}
