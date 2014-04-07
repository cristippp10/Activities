using Microsoft.CSharp.RuntimeBinder;
//using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

using System.Linq;
using System.Linq.Expressions;

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using System.Text;
using System.Data;
//using UiPath.Library;

using System.Net;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Classes;
using Renci.SshNet.Sftp;

using System.Net.FtpClient;
using FtpClientFunc;


namespace FtpActivities
{
	public class FtpSessionGen : System.IDisposable
	{
        public string Host
        {
            get;
            set;
        }
        public string User
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public int Port
        {
            get;
            set;
        }
        public int FtpMode
        {
            get;
            set;
        }
        public SftpClient sftpClient;
        public FtpClient ftpClient;

        public ArrayList aKeyFiles = new ArrayList();
        public string SKeyFiles;
        public PrivateKeyFile[] keyFiles;

        public bool IsActivityCanceled = false;

        static void cl_ValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            e.Accept = true;
        }

		public FtpSessionGen()
		{
		}
        public FtpSessionGen(int ftpMode, string host, string user, string password, int port, string sKeyFiles = "")
        {
            FtpMode = ftpMode; 
            Host = host; 
            User = user;
            Password = password;
            Port = port;
            SKeyFiles = sKeyFiles; 

            if (ftpMode == 0)
            {
                if (SKeyFiles == "")
                    sftpClient = new SftpClient(Host, Port, User, Password);
                else
                {
                    string[] aFiles = sKeyFiles.Split('|');
                    foreach (string f in aFiles)
                    {
                        string filename = f;
                        string pass = "";
                        if ((f.IndexOf("<") > 0) && (f.IndexOf(">") == f.Length - 1))
                        {
                            pass = f.Substring(f.IndexOf("<") + 1, f.IndexOf(">") - f.IndexOf("<") - 1);
                            filename = f.Substring(0, f.IndexOf("<"));
                        }

                        if (File.Exists(filename))
                        {
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
                            }

                        }
                    }

                    Array a = aKeyFiles.ToArray(typeof(PrivateKeyFile));
                    keyFiles = a as PrivateKeyFile[];

                    string strMessError = "";
                    try
                    {
                        sftpClient = new SftpClient(Host, Port, User, keyFiles);

                    }
                    catch (Exception ex)
                    {
                        strMessError = ex.Message;
                    }
                }
            }
            if (ftpMode == 1)
            {
                ftpClient = new FtpClient();
                ftpClient.Credentials = new NetworkCredential(User, Password);
                ftpClient.Host = Host;
                ftpClient.EncryptionMode = FtpEncryptionMode.None;
                ftpClient.ValidateCertificate += new FtpSslValidation(cl_ValidateCertificate);
                ftpClient.DataConnectionType = FtpDataConnectionType.EPSV; //(FtpDataConnectionType)(int);                       
            }
        }

        public void Connect()
        {
            if (FtpMode == 0) 
                sftpClient.Connect();
            if (FtpMode == 1)
                ftpClient.Connect();
        }

        public bool IsConnected()
        {
            if (FtpMode == 0)
            {
                if (sftpClient != null) 
                    return sftpClient.IsConnected;
            }

            if (FtpMode == 1)
            {
                if (ftpClient != null)
                    return ftpClient.IsConnected;
            }
            return false;
        }

        public void Disconnect()
        {
            if (FtpMode == 0)
                sftpClient.Disconnect();
            if (FtpMode == 1)
                ftpClient.Disconnect();
        }

        //public IEnumerable<FtpFileClass> GetFiles(string remotepath)
        public ArrayList GetFiles(string remotepath)
        {
            //FtpFileClass ftpFile = null;
            ArrayList ftpFileClassColection = new ArrayList();
            
            if (this.FtpMode == 0)
                foreach (SftpFile f in sftpClient.ListDirectory(remotepath)) //@"" 
                {
                    FtpFileClass ftpFile = new FtpFileClass();

                    ftpFile.Fullname = f.FullName;
                    ftpFile.Name = f.Name;
                    ftpFile.Length = f.Length;

                    ftpFile.LastWriteTime = f.LastWriteTime;
                    ftpFile.LastAccessTime = f.LastAccessTime;

                    ftpFile.IsDirectory = f.IsDirectory;
                    ftpFile.IsRegularFile = f.IsRegularFile;
                    ftpFile.IsSymbolicLink = f.IsSymbolicLink;

                    ftpFile.UserId = f.UserId;
                    ftpFile.GroupId = f.GroupId;

                    ftpFile.OwnerCanRead = f.OwnerCanRead;
                    ftpFile.OwnerCanExecute = f.OwnerCanExecute;
                    ftpFile.OwnerCanWrite = f.OwnerCanWrite;
                    ftpFile.OthersCanRead = f.OthersCanRead;
                    ftpFile.OthersCanExecute = f.OthersCanExecute;
                    ftpFile.OthersCanWrite = f.OthersCanWrite;
                    ftpFile.GroupCanRead = f.GroupCanRead;
                    ftpFile.GroupCanExecute = f.GroupCanExecute;
                    ftpFile.GroupCanWrite = f.GroupCanWrite;
                    if (ftpFile.Name != "." && ftpFile.Name != "..")  
                        ftpFileClassColection.Add(ftpFile);
                 }

            if (this.FtpMode == 1)
                foreach (FtpListItem f in ftpClient.GetListing(remotepath)) //@""
                {
                    FtpFileClass ftpFile = new FtpFileClass();

                    ftpFile.Fullname = f.FullName;
                    ftpFile.Name = f.Name;
                    ftpFile.Length = f.Size;

                    ftpFile.IsDirectory = f.Type.HasFlag(FtpFileSystemObjectType.Directory);
                    ftpFile.IsRegularFile = f.Type.HasFlag(FtpFileSystemObjectType.File);
                    ftpFile.IsSymbolicLink = f.Type.HasFlag(FtpFileSystemObjectType.Link);

                    ftpFile.LastWriteTime = f.Modified; 
                    ftpFile.LastAccessTime = f.Created;

                    ftpFile.OwnerCanRead = f.OwnerPermissions.HasFlag(FtpPermission.Read);
                    ftpFile.OwnerCanExecute = f.OwnerPermissions.HasFlag(FtpPermission.Execute);
                    ftpFile.OwnerCanWrite = f.OwnerPermissions.HasFlag(FtpPermission.Write);
                    ftpFile.OthersCanRead = f.OthersPermissions.HasFlag(FtpPermission.Read);
                    ftpFile.OthersCanExecute = f.OthersPermissions.HasFlag(FtpPermission.Execute);
                    ftpFile.OthersCanWrite = f.OthersPermissions.HasFlag(FtpPermission.Write);
                    ftpFile.GroupCanRead = f.GroupPermissions.HasFlag(FtpPermission.Read);
                    ftpFile.GroupCanExecute = f.GroupPermissions.HasFlag(FtpPermission.Execute);
                    ftpFile.GroupCanWrite = f.GroupPermissions.HasFlag(FtpPermission.Write);

                    if (ftpFile.Name != "." && ftpFile.Name != "..")
                        ftpFileClassColection.Add(ftpFile);
                }

            return ftpFileClassColection;//as IEnumerable<FtpFileClass> ;//sftpClient.ListDirectory(@"");
            
        }

        public FtpFileClass GetFileAttributes(string rpath)
        {
           string remotepath = rpath.Substring(0, (rpath.LastIndexOf('/') < 0) ? 0 : rpath.LastIndexOf('/'));
            
            if (this.FtpMode == 0)
                foreach (SftpFile f in sftpClient.ListDirectory(remotepath)) 
                {
                    if (f.IsRegularFile)
                    {
                        if (f.FullName.Contains(rpath))
                        {
                            FtpFileClass ftpFile = new FtpFileClass();

                            ftpFile.Fullname = f.FullName;
                            ftpFile.Name = f.Name;
                            ftpFile.Length = f.Length;

                            ftpFile.LastWriteTime = f.LastWriteTime;
                            ftpFile.LastAccessTime = f.LastAccessTime;

                            ftpFile.IsDirectory = f.IsDirectory;
                            ftpFile.IsRegularFile = f.IsRegularFile;
                            ftpFile.IsSymbolicLink = f.IsSymbolicLink;

                            ftpFile.UserId = f.UserId;
                            ftpFile.GroupId = f.GroupId;

                            ftpFile.OwnerCanRead = f.OwnerCanRead;
                            ftpFile.OwnerCanExecute = f.OwnerCanExecute;
                            ftpFile.OwnerCanWrite = f.OwnerCanWrite;
                            ftpFile.OthersCanRead = f.OthersCanRead;
                            ftpFile.OthersCanExecute = f.OthersCanExecute;
                            ftpFile.OthersCanWrite = f.OthersCanWrite;
                            ftpFile.GroupCanRead = f.GroupCanRead;
                            ftpFile.GroupCanExecute = f.GroupCanExecute;
                            ftpFile.GroupCanWrite = f.GroupCanWrite;

                            return ftpFile;
                        }
                    }
                    
                }

            if (this.FtpMode == 1)
                foreach (FtpListItem f in ftpClient.GetListing(remotepath)) 
                {
                    if (f.FullName.Contains(rpath))
                    {

                        FtpFileClass ftpFile = new FtpFileClass();

                        ftpFile.Fullname = f.FullName;
                        ftpFile.Name = f.Name;
                        ftpFile.Length = f.Size;

                        ftpFile.IsDirectory = f.Type.HasFlag(FtpFileSystemObjectType.Directory);
                        ftpFile.IsRegularFile = f.Type.HasFlag(FtpFileSystemObjectType.File);
                        ftpFile.IsSymbolicLink = f.Type.HasFlag(FtpFileSystemObjectType.Link);

                        ftpFile.LastWriteTime = f.Modified;
                        ftpFile.LastAccessTime = f.Created;

                        ftpFile.OwnerCanRead = f.OwnerPermissions.HasFlag(FtpPermission.Read);
                        ftpFile.OwnerCanExecute = f.OwnerPermissions.HasFlag(FtpPermission.Execute);
                        ftpFile.OwnerCanWrite = f.OwnerPermissions.HasFlag(FtpPermission.Write);
                        ftpFile.OthersCanRead = f.OthersPermissions.HasFlag(FtpPermission.Read);
                        ftpFile.OthersCanExecute = f.OthersPermissions.HasFlag(FtpPermission.Execute);
                        ftpFile.OthersCanWrite = f.OthersPermissions.HasFlag(FtpPermission.Write);
                        ftpFile.GroupCanRead = f.GroupPermissions.HasFlag(FtpPermission.Read);
                        ftpFile.GroupCanExecute = f.GroupPermissions.HasFlag(FtpPermission.Execute);
                        ftpFile.GroupCanWrite = f.GroupPermissions.HasFlag(FtpPermission.Write);

                        return ftpFile;
                    }
                }

            return null;

        }

        public void GetFilesAllSubDir(string remotepath, ref ArrayList list)
        {
            
            ArrayList dirfiles = GetFiles(remotepath);

            foreach (FtpFileClass f in dirfiles)
            {
                if (f.IsDirectory && (f.Name != "." && f.Name != ".."))
                    GetFilesAllSubDir(f.Fullname, ref list);
                if (!list.Contains(f)) list.Add(f);
            } 
        }

        public void GetFilesDir(string remotepath, ref ArrayList list)
        {
            ArrayList dirfiles = GetFiles(remotepath);

            foreach (FtpFileClass f in dirfiles)
            {
                if (!list.Contains(f)) list.Add(f);
            }
        }

        public void GetFilesAllSubDir(string remotepath, ref Dictionary<string, FtpFileClass> dic)
        {

            ArrayList dirfiles = GetFiles(remotepath);
            foreach (FtpFileClass fi in dirfiles)
            {
                if (fi.IsDirectory && (fi.Name != "." && fi.Name != ".."))
                    GetFilesAllSubDir(fi.Fullname, ref dic);
                else
                {
                    if (!dic.ContainsKey(fi.Fullname))
                    {
                        if (remotepath == "")
                            dic.Add(fi.Name, fi);
                        else
                        {
                            string rpath = fi.Fullname.Substring(fi.Fullname.IndexOf(remotepath) + 1);

                            dic.Add(rpath, fi);
                        }
                    }
                }
            }
            dirfiles.Clear();

            foreach (FtpFileClass f in dirfiles)
            {

                if (f.IsDirectory)
                    GetFilesAllSubDir(f.Fullname, ref dic);
                //if (!list.Contains(f)) list.Add(f);
            }
        }

        public ArrayList GetFilesLocal(string path)
        {
            ArrayList fileColection = new ArrayList();

            //if (this.FtpMode == 0)
                foreach (string f in Directory.GetFiles(path))
                {
                    FileInfo fi = new FileInfo(f);
                    fileColection.Add(fi);
                }
            
            return fileColection;
        }

        public void GetFilesLocalAllSubDir(string path, ref Dictionary<string, FileInfo> dic)
        {
            ArrayList al_fi = GetFilesLocal(path);
            foreach (FileInfo fi in al_fi)
                if (!dic.ContainsKey(fi.FullName))
                    dic.Add(fi.FullName, fi);
            al_fi.Clear();

            string[] dirfiles = Directory.GetDirectories(path);

            foreach (string f in dirfiles)
            {
                GetFilesLocalAllSubDir(f, ref dic);
                //ArrayList alfi = GetFilesLocal(f);
                //foreach (FileInfo fi in alfi)
                //    if (!dic.ContainsKey(fi.FullName))
                //        dic.Add(fi.FullName, fi);
                //alfi.Clear();
            }
           
        }

        public void GetFilesLocalAllSubDir(string path, ref ArrayList list)
        {
            ArrayList al_fi = GetFilesLocal(path);
            foreach (FileInfo fi in al_fi)
                if (!list.Contains(fi))
                    list.Add( fi);
            al_fi.Clear();

            string[] dirfiles = Directory.GetDirectories(path);

            foreach (string f in dirfiles)
            {
                GetFilesLocalAllSubDir(f, ref list);
            }

        }

        public void VerifyDownload(string remotepath, string localpath, ref ArrayList listFilesNotDown, ref ArrayList listFilesDown)
        {

            ArrayList list = new ArrayList();
            GetFilesAllSubDir(remotepath, ref list);

            Dictionary<string, FileInfo> diclocalfiles = new Dictionary<string, FileInfo>();
            GetFilesLocalAllSubDir(localpath, ref diclocalfiles);

            foreach (FtpFileClass f in list)
            {
                if (f.IsDirectory) continue;
                string dirparent = (remotepath.Contains("/")) ? remotepath.Substring(remotepath.LastIndexOf("/") + 1) : remotepath;
                string partname = f.Fullname.Substring(f.Fullname.IndexOf(dirparent));
                string key = localpath + @"\" + partname.Replace("/", @"\");
                FileInfo fi = diclocalfiles.FirstOrDefault((x) => x.Key == key).Value;
                if (fi != null)
                {
                    if (fi.Length != f.Length)
                        listFilesNotDown.Add(f);
                    else
                        listFilesDown.Add(f);
                }
                else
                    listFilesNotDown.Add(f);
            }
        }

        public void VerifyUpload(string remotepath, string localpath, ref ArrayList listFilesNotUp, ref ArrayList listFilesUp)
        {

            Dictionary<string, FtpFileClass> dicremotefiles = new Dictionary<string, FtpFileClass>();
            ArrayList list = new ArrayList();
            GetFilesAllSubDir(remotepath, ref dicremotefiles);

            //Dictionary<string, FileInfo> diclocalfiles = new Dictionary<string, FileInfo>();
            GetFilesLocalAllSubDir(localpath, ref list);

            foreach (FileInfo f in list)
            {
                //if (f.IsDirectory) continue;
                string dirparent = (localpath.Contains(@"\")) ? localpath.Substring(localpath.LastIndexOf(@"\") + 1) : localpath;
                string partname = f.FullName.Substring(f.FullName.IndexOf(dirparent) + 0);
                string key = (remotepath != "") ? remotepath + @"/" + partname.Replace(@"\", @"/") : partname.Replace(@"\", @"/");
                FtpFileClass fi = dicremotefiles.FirstOrDefault((x) => x.Key == key).Value;
                if (fi != null)
                {
                    if (fi.Length != f.Length)
                        listFilesNotUp.Add(f);
                    else
                        listFilesUp.Add(f);
                }
                else
                    listFilesNotUp.Add(f);
            }
        }

        public void DownloadOne(string remotepath, string localpath)
        {
            try
            {
                if (FtpMode == 0)
                {
                    if (!sftpClient.IsConnected)
                        sftpClient.Connect();
                    string namefile = remotepath.Substring(remotepath.LastIndexOf(@"/") + 1);
                    if (localpath.Length > 0)
                        if (localpath[localpath.Length - 1] != '\\')
                            localpath = localpath + @"\";
                    sftpClient.DownloadFile(remotepath, File.Create(localpath + namefile));
                }
                if (FtpMode == 1)
                {
                    if (!ftpClient.IsConnected)
                        ftpClient.Connect();
                    FtpClientFuncClass.DoDownloadOneFile(ftpClient, remotepath, localpath);
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void UploadOne(string remotepath, string localpath)
        {
            try
            {
                string namefile = localpath.Substring(localpath.LastIndexOf(@"\") + 1);
                string remotepathfile = (remotepath == "" || remotepath == @"/" || remotepath == @"\") ? remotepath + namefile : remotepath + @"/" + namefile;
                if (FtpMode == 0)
                {
                    if (!sftpClient.IsConnected)
                        sftpClient.Connect();

                    sftpClient.UploadFile(File.OpenRead(localpath), remotepathfile);
                }
                if (FtpMode == 1)
                {
                    if (!ftpClient.IsConnected)
                        ftpClient.Connect();
                    FtpClientFuncClass.DoUploadOneFile(ftpClient, remotepath, localpath);
                }
            }
            catch (Exception ex) 
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        public System.Threading.ManualResetEvent mevent = new ManualResetEvent(false);
        public string localPath;
        public string remotePath;

        public void DoDownloadMultiple()
        {
            DownloadMultiple(localPath, remotePath);
            mevent.Set();
        }

        public void DoDownloadOne()
        {
            DownloadOne(remotePath, localPath);
            mevent.Set();
        }

        public void DownloadMultiple(string localpath, string remotepath)
        {
            try
            {
                if (localpath[localpath.Length - 1] != '\\')
                    localpath = localpath + @"\";

                if (FtpMode == 0)
                {

                    if (!sftpClient.IsConnected)
                        sftpClient.Connect();

                    string rootdirlocal = "";
                    if (remotepath.Length > 1)
                    {
                        if (remotepath[remotepath.Length - 1] != '/')
                        {
                            rootdirlocal = remotepath.Substring(remotepath.LastIndexOf("/") + 1);
                            //remotepath = remotepath + "/";
                        }
                        else
                        {
                            rootdirlocal = remotepath.Substring(0, remotepath.Length - 1);
                            rootdirlocal = rootdirlocal.Substring(rootdirlocal.LastIndexOf("/") + 1);
                        }
                    }

                    localpath = localpath + rootdirlocal;

                    Renci.SshNet.Tests.Classes.SftpClientTest sftpTest = new SftpClientTest();

                    sftpTest.Sftp_Multiple_Async_Download_Files(sftpClient, localpath, remotepath, "*.*");

                }

                if (FtpMode == 1)
                {
                    if (!ftpClient.IsConnected)
                        ftpClient.Connect();

                    List<Thread> threads = new List<Thread>();
                    //System.Windows.Forms.MessageBox.Show("Enter6Execute");
                    FtpClientFunc.FtpClientFuncClass.Download(threads, ftpClient, remotepath, localpath, remotepath); //remotePath = "/"

                    while (threads.Count > 0)
                    {
                        threads[0].Join();

                        lock (threads)
                        {
                            threads.RemoveAt(0);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void DoUploadMultiple()
        {
            UploadMultiple(localPath, remotePath);
            mevent.Set();
        }

        public void DoUploadOne()
        {
            UploadOne(remotePath, localPath);
            mevent.Set();
        }

        public void UploadMultiple(string localpath, string remotepath)
        {
            try
            {
                string rootdirremote = "";
                if (localpath.Length > 0)
                {
                    if (localpath[localpath.Length - 1] != '\\')
                    {
                        rootdirremote = localpath.Substring(localpath.LastIndexOf(@"\") + 1);
                        localpath = localpath + @"\";
                    }
                    else
                    {
                        rootdirremote = localpath.Substring(0, localpath.Length - 1);
                        rootdirremote = rootdirremote.Substring(rootdirremote.LastIndexOf(@"\") + 1);
                    }
                }

                remotepath = (remotepath == "" || remotepath == "/" || remotepath == @"\") ? remotepath + rootdirremote : remotepath + "/" + rootdirremote;

                if (FtpMode == 0)
                {
                    if (!sftpClient.IsConnected)
                        sftpClient.Connect();

                    Renci.SshNet.Tests.Classes.SftpClientTest sftpTest = new SftpClientTest();
                    sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, localpath, remotepath, "*.*");
                }

                if (FtpMode == 1)
                {
                    if (!ftpClient.IsConnected)
                        ftpClient.Connect();


                    FtpClientFunc.FtpClientFuncClass.Upload(ftpClient, localpath, remotepath); //remotePath = "/"
                }
            }
            catch (Exception ex) 
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
       

        public void DeleteFile(string remotepath)
        {
            if (this.FtpMode == 0)
                this.sftpClient.DeleteFile(remotepath);
            if (this.FtpMode == 1)
                ftpClient.DeleteFile(remotepath);
        }

        public void CreateDirectory(string newdir)
        {
            if (FtpMode == 0)
                sftpClient.CreateDirectory(newdir);

            if (FtpMode == 1)
                ftpClient.CreateDirectory(newdir);
        }

        public void DeleteDirectory(string remotepath)
        {
            if (this.FtpMode == 0)
            {
                //this.ftpSessionGen.sftpClient.DeleteDirectory(remotepath);
                RemoveDirectory(remotepath, this);
                sftpClient.DeleteDirectory(remotepath);
            }
            if (this.FtpMode == 1)
            {
                FtpClientFunc.FtpClientFuncClass.DeleteDirectory(ftpClient, remotepath);
                ftpClient.DeleteDirectory(remotepath);
            }
        }

        static void RemoveDirectory(string remotepath, FtpSessionGen ftpSessionGen)
        {
            var dirs = ftpSessionGen.sftpClient.ListDirectory(remotepath);
            foreach (SftpFile dir in dirs)
            {
                if (!dir.IsDirectory)
                {
                    ftpSessionGen.sftpClient.DeleteFile(dir.FullName);
                }
            }
            foreach (SftpFile dir in dirs)
            {
                if (dir.IsDirectory)
                {
                    if (dir.Name != "." && dir.Name != "..")
                    {
                        RemoveDirectory(dir.FullName, ftpSessionGen);

                        ftpSessionGen.sftpClient.DeleteDirectory(dir.FullName);
                    }
                }
            }
            //ftpSessionGen.sftpClient.DeleteDirectory(remotepath);
        }

        public bool RemoteDirectoryExists (string rpath)
        {
            bool bIsDir = false;

            if (this.FtpMode == 0)
            {
                if (rpath.Length > 0 || rpath == "")
                {
                    try
                    {
                        var filesindir = sftpClient.ListDirectory(rpath);
                        if (filesindir != null)
                            bIsDir = true;
                    }
                    catch
                    {
                        bIsDir = false;
                    }
                }

            }

            if (this.FtpMode == 1)
            {
                //System.Windows.Forms.MessageBox.Show("Enter4.2Execute");
                bIsDir = ftpClient.DirectoryExists(rpath);
                //System.Windows.Forms.MessageBox.Show("Enter4.3Execute");
            }

            return bIsDir;
        }

        public bool RemoteFileExists(string rpath)
        {
            bool bIsFile = false;

            if (this.FtpMode == 0)
            {
                try
                {
                    var filesinparentdir = sftpClient.ListDirectory(rpath.Substring(0, (rpath.LastIndexOf('/') < 0) ? 0 : rpath.LastIndexOf('/')));
                    foreach (Renci.SshNet.Sftp.SftpFile f in filesinparentdir)
                        if (f.IsRegularFile)
                        {
                            if (f.FullName.Contains(rpath))
                            {
                                bIsFile = true;
                            }
                        }
                }
                catch
                {
                    bIsFile = false;
                }
            }

            if (this.FtpMode == 1)
            {
                bIsFile = ftpClient.FileExists(rpath);
            }

            return bIsFile;
        }

        public DataTable DataTableFromFileList(ArrayList listFiles)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Length", typeof(long));

            dataTable.Columns.Add("LastWriteTime", typeof(DateTime));
            dataTable.Columns.Add("LastAccessTime", typeof(DateTime));

            dataTable.Columns.Add("IsDirectory", typeof(bool));
            dataTable.Columns.Add("IsRegularFile", typeof(bool));
            dataTable.Columns.Add("IsSymbolicLink", typeof(bool));

            dataTable.Columns.Add("OwnerCanRead", typeof(bool));
            dataTable.Columns.Add("OwnerCanExecute", typeof(bool));
            dataTable.Columns.Add("OwnerCanWrite", typeof(bool));
            dataTable.Columns.Add("OthersCanRead", typeof(bool));
            dataTable.Columns.Add("OthersCanExecute", typeof(bool));
            dataTable.Columns.Add("OthersCanWrite", typeof(bool));
            dataTable.Columns.Add("GroupCanRead", typeof(bool));
            dataTable.Columns.Add("GroupCanExecute", typeof(bool));
            dataTable.Columns.Add("GroupCanWrite", typeof(bool));

            //int i = 1; 
            foreach (FtpFileClass f in listFiles)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow[0] = f.Fullname;
                dataRow[1] = f.Name;
                dataRow[2] = f.Length;

                dataRow[3] = f.LastWriteTime;
                dataRow[4] = f.LastAccessTime;

                dataRow[5] = f.IsDirectory;
                dataRow[6] = f.IsRegularFile;
                dataRow[7] = f.IsSymbolicLink;

                dataRow[8] = f.OwnerCanRead;
                dataRow[9] = f.OwnerCanExecute;
                dataRow[10] = f.OwnerCanWrite;
                dataRow[11] = f.OthersCanRead;
                dataRow[12] = f.OthersCanExecute;
                dataRow[13] = f.OthersCanWrite;
                dataRow[14] = f.GroupCanRead;
                dataRow[15] = f.GroupCanExecute;
                dataRow[16] = f.GroupCanWrite;

                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        public DataTable DataTableFromFileInfoList(ArrayList listFiles)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Length", typeof(long));

            dataTable.Columns.Add("LastWriteTime", typeof(DateTime));
            dataTable.Columns.Add("LastAccessTime", typeof(DateTime));

            dataTable.Columns.Add("IsDirectory", typeof(bool));
            dataTable.Columns.Add("IsRegularFile", typeof(bool));
            dataTable.Columns.Add("IsSymbolicLink", typeof(bool));

            dataTable.Columns.Add("OwnerCanRead", typeof(bool));
            dataTable.Columns.Add("OwnerCanExecute", typeof(bool));
            dataTable.Columns.Add("OwnerCanWrite", typeof(bool));
            dataTable.Columns.Add("OthersCanRead", typeof(bool));
            dataTable.Columns.Add("OthersCanExecute", typeof(bool));
            dataTable.Columns.Add("OthersCanWrite", typeof(bool));
            dataTable.Columns.Add("GroupCanRead", typeof(bool));
            dataTable.Columns.Add("GroupCanExecute", typeof(bool));
            dataTable.Columns.Add("GroupCanWrite", typeof(bool));

            //int i = 1; 
            foreach (FileInfo f in listFiles)
            {
                DataRow dataRow = dataTable.NewRow();
                FileAttributes fa = File.GetAttributes(f.FullName);
                //System.Security.AccessControl.FileSecurity fs = File.GetAccessControl(f.FullName);
                
                dataRow[0] = f.FullName;
                dataRow[1] = f.Name;
                dataRow[2] = f.Length;

                dataRow[3] = f.LastWriteTime;
                dataRow[4] = f.LastAccessTime;

                dataRow[5] = fa.HasFlag(FileAttributes.Directory);  //f.IsDirectory;
                dataRow[6] = true ;// fa.HasFlag(FileAttributes.Normal); //f.IsRegularFile;
                dataRow[7] = false;  //f.IsSymbolicLink;

                //System.Security.AccessControl.FileSecurity  fs = f.GetAccessControl();
                //System.Security.AccessControl.AuthorizationRuleCollection arc = fs.GetAccessRules(true,true,null);
                //System.Security.AccessControl.AuthorizationRule ar = arc[0];

                dataRow[8] = true;//f.OwnerCanRead;
                dataRow[9] = false;// f.OwnerCanExecute;
                dataRow[10] = true; // f.OwnerCanWrite;
                dataRow[11] = false; // f.OthersCanRead;
                dataRow[12] = false;// f.OthersCanExecute;
                dataRow[13] = false; //f.OthersCanWrite;
                dataRow[14] = false;// f.GroupCanRead;
                dataRow[15] = false;//f.GroupCanExecute;
                dataRow[16] = false;// f.GroupCanWrite;

                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        public void CancelActivity()
        {
            this.IsActivityCanceled = true;

            try
            {
                if (this.FtpMode == 0)
                {
                    
                    lock (sftpClient)
                    {
                        //uint bs = sftpClient.BufferSize;
                        //this.sftpClient.OperationTimeout = new TimeSpan(0, 0, 0);
                        this.sftpClient.Disconnect();
                    }
                }

                if (this.FtpMode == 1)
                {

                    lock (ftpClient)
                    {
                        //uint bs = sftpClient.BufferSize;
                        //this.sftpClient.OperationTimeout = new TimeSpan(0, 0, 0);
                        this.ftpClient.Disconnect();
                    }
                }

            }
            catch (Exception ex)
            { 
            }

        }



		~FtpSessionGen()
		{
			this.Dispose();
		}
		public void Dispose()
		{
			//this.CloseWorkbook();
		}
	}

    public class FtpFileClass
    {
        public string Fullname ;
        public string Name;
        public long Length;

        public DateTime LastWriteTime;
        public DateTime LastAccessTime;

        public bool IsDirectory;
        public bool IsRegularFile;
        public bool IsSymbolicLink;

        public int UserId;
        public int GroupId;

        public bool OwnerCanRead;
        public bool OwnerCanExecute;
        public bool OwnerCanWrite;
        public bool OthersCanRead;
        public bool OthersCanExecute;
        public bool OthersCanWrite;
        public bool GroupCanRead;
        public bool GroupCanExecute;
        public bool GroupCanWrite;

        public FtpFileClass()
        { }
    }

  
}
