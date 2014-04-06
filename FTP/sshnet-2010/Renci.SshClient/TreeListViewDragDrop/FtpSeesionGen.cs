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

//using UiPath.Library;

using System.Net;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Classes;
using Renci.SshNet.Sftp;

using System.Net.FtpClient;
using FtpClientFunc;


namespace TreeListViewDragDrop
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

        public void UploadOne(string remotepath, string localpath)
        {
            string namefile = localpath.Substring(localpath.LastIndexOf(@"\") + 1);
            string remotepathfile =  (remotepath == "" || remotepath == @"/" || remotepath == @"\") ? remotepath + namefile : remotepath + @"/" + namefile ;   
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
        public void DownloadMultiple(string localpath, string remotepath)
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

        public void UploadMultiple(string localpath, string remotepath)
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
        //public Workbook(string workbookPath, string password = null, bool createNew = true)
        //{
        //    //modif
        //    //Workbook.<>c__DisplayClass1 <>c__DisplayClass = new Workbook.<>c__DisplayClass1();
        //    //<>c__DisplayClass.workbookPath = workbookPath;
        //    //<>c__DisplayClass.password = password;
			
        //    try
        //    {
        //        if (this.ExcelApp == null)
        //        {
        //            this.ExcelApp = (Application)System.Activator.CreateInstance(System.Type.GetTypeFromCLSID(new System.Guid("00024500-0000-0000-C000-000000000046")));
        //            this.ExcelApp.DisplayAlerts = false;
        //            this.ExcelApp.SheetsInNewWorkbook = 1;
        //        }
        //    }
        //    catch
        //    {
        //        throw new System.Exception("Error openning workbook. Make sure Excel is installed");
        //    }
        //    Workbooks workbooks = this.ExcelApp.Workbooks;
        //    //<>c__DisplayClass.workbookPath = System.IO.Path.GetFullPath(<>c__DisplayClass.workbookPath);
        //    if (!System.IO.File.Exists(workbookPath))
        //    {
        //        if (!createNew)
        //        {
        //            throw new System.ArgumentException("The workbook does not exist");
        //        }
        //        this.CurrentWorkbook = workbooks.Add(System.Reflection.Missing.Value);
        //        this.CurrentWorkbook.SaveAs(workbookPath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, XlSaveAsAccessMode.xlNoChange, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
        //        this.CurrentWorkbook.Close(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
        //    }
        //    this.CurrentWorkbook = Retry.Do<Microsoft.Office.Interop.Excel.Workbook>(() => workbooks.Open(workbookPath, System.Type.Missing, System.Type.Missing, System.Type.Missing, password, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value), 50, 3);
        //}

        public void DeleteFile(string remotepath)
        {
            if (this.FtpMode == 0)
                this.sftpClient.DeleteFile(remotepath);
            if (this.FtpMode == 1)
                ftpClient.DeleteFile(remotepath);
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
                bIsDir = ftpClient.DirectoryExists(rpath);
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

    //public class ModelWithChildren
    //{

    //    public static FtpSessionGen ftpSessionGen;

    //    public static string LocalPathRoot;
    //    public string Now { get; set; } //{ return DateTime.Now.ToShortDateString(); } }//DateTime Now { get { return DateTime.Now; } }
    //    public string Update { get; set; } //{ return lastChildrenFetch; } }
    //    private DateTime lastChildrenFetch;

    //    public string Label { get; set; }
    //    public ModelWithChildren Parent { get; set; }
    //    public string ParentLabel
    //    {
    //        get { return this.Parent == null ? "none" : this.Parent.Label; }
    //    }

    //    public ModelWithChildren() { }
    //    public ModelWithChildren ModelWithChildrenClone(ModelWithChildren m)
    //    {
    //        ModelWithChildren model = new ModelWithChildren();
    //        model.Label = this.Label;
    //        model.Update = this.Update;
    //        model.LocalRemote = this.LocalRemote;
    //        model.RemotePath = this.RemotePath;
    //        model.LocalPath = this.LocalPath;
    //        model.ChildCount = this.ChildCount;
    //        model.Now = this.Now;
    //        //model.LocalPathRoot = LocalPathRoot;
    //        model.DataForChildren = new ArrayList { "" };
    //        return model;
    //    }
    //    public long ChildCount
    //    {
    //        get;
    //        set;
    //        //get
    //        //{
    //        //    return this.children == null ? 0 : this.children.Count;
    //        //}
    //        //set 
    //        //{
    //        //    ChildCount = value;
    //        //}
    //    }

    //    public string RemotePath
    //    {
    //        get;
    //        set;
    //    }

    //    public string LocalPath
    //    {
    //        get;
    //        set;
    //    }

    //    public int LocalRemote
    //    {
    //        get;
    //        set;
    //    }
    //    public List<ModelWithChildren> Children
    //    {
    //        get
    //        {
    //            lastChildrenFetch = DateTime.Now;
    //            if (this.children == null)
    //                children = CreateModels(this, this.DataForChildren, ftpSessionGen, LocalPathRoot, LocalRemote);
    //            return this.children;
    //        }
    //        set { this.children = value; }
    //    }
    //    private List<ModelWithChildren> children;

    //    public ArrayList DataForChildren { get; set; }

    //    public bool IsAncestor(ModelWithChildren model)
    //    {
    //        if (this == model)
    //            return true;
    //        if (this.Parent == null)
    //            return false;
    //        return this.Parent.IsAncestor(model);
    //    }

    //    //public override string ToString()
    //    //{
    //    //    //return String.Format("{0}, parent: {1}", Label ?? "[]", ParentLabel);
    //    //    return String.Format(Label ?? "");
    //    //}
    //    public static List<ModelWithChildren> CreateModels(ModelWithChildren parent, ArrayList data, FtpSessionGen sftp, string localPathRoot, int localRemote = 0)
    //    {
    //        List<ModelWithChildren> models = new List<ModelWithChildren>();
    //        //foreach (object x in data) {
    //        //    models.Add(new ModelWithChildren {
    //        //        Label = (parent == null ? x.ToString() : parent.Label + "-" + x.ToString()),
    //        //        Parent = parent,
    //        //        DataForChildren = data
    //        //    });
    //        //}

    //        //string host = "192.168.0.104"; // TODO: Initialize to an appropriate value
    //        //string username = "one"; // TODO: Initialize to an appropriate value
    //        ////PrivateKeyFile[] keyFiles = null; // TODO: Initialize to an appropriate value
    //        //string password = "one";

    //        //sftpClient = new Renci.SshNet.SftpClient(host, 22, username, password);
    //        //sftpClient.Connect();

    //        LocalPathRoot = localPathRoot;
    //        ftpSessionGen = sftp;
    //        if (parent == null)
    //        {
    //            //var files = sftpClient.ListDirectory("\\");
    //            //data.Clear();
    //            //foreach (Renci.SshNet.Sftp.SftpFile f in files)
    //            //    data.Add(f.Name);

    //            foreach (object x in data)
    //            {
    //                models.Add(new ModelWithChildren
    //                {
    //                    ////Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
    //                    //Label = (parent == null ? x.ToString() : x.ToString()),
    //                    Label = (localRemote == 0 ? x.ToString() : localPathRoot),
    //                    RemotePath = (parent == null ? x.ToString() : x.ToString()),
    //                    Parent = parent,
    //                    LocalRemote = localRemote,
    //                    LocalPath = localPathRoot,
    //                    DataForChildren = new ArrayList { x.ToString() } //data
    //                });
    //            }
    //        }
    //        else
    //        {
    //            if (parent.Update == "file") return null;

    //            string pathremote = "";
    //            if (parent.LocalRemote == 0)
    //            {
    //                if (parent.Label.ToString() == "root dir")
    //                    pathremote = "\\";
    //                else
    //                    //pathremote = parent.Label.ToString();
    //                    pathremote = parent.RemotePath.ToString();
    //            }
    //            string localpath = "";
    //            if (parent.LocalRemote == 1)
    //            {
    //                if (parent.Label.ToString() == "")
    //                    localpath = localPathRoot; //"G:\\BAK";
    //                else
    //                    //pathremote = parent.Label.ToString();
    //                    localpath = parent.LocalPath.ToString();
    //            }
    //            //sftpClient.ChangeDirectory(pathremote);



    //            if (parent.LocalRemote == 0 && ftpSessionGen.IsConnected())
    //            {
    //                var files = ftpSessionGen.GetFiles(pathremote); //ListDirectory(pathremote);


    //                data.Clear();
    //                //foreach (Renci.SshNet.Sftp.SftpFile f in files)
    //                foreach (FtpFileClass f in files)
    //                    if (f.IsDirectory && f.Name != "." && f.Name != "..")
    //                    {
    //                        string path = "";
    //                        if (parent.Label == "")
    //                            path = f.Name;
    //                        else
    //                            path = parent.RemotePath + @"/" + f.Name;

    //                        models.Add(new ModelWithChildren
    //                        {
    //                            //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
    //                            Label = (parent == null ? f.Name : f.Name), //path
    //                            RemotePath = path,
    //                            Parent = parent,
    //                            Now = f.LastWriteTime.ToShortDateString(),
    //                            //ChildCount = (Children == null ? 0 : Children.Count),
    //                            LocalRemote = 0,
    //                            Update = "dir",
    //                            DataForChildren = new ArrayList { f.Name } //data
    //                        });
    //                    }

    //                //foreach (object x in data)
    //                //{
    //                //    string path = "";
    //                //    if (parent.Label == "")
    //                //        path = x.ToString();
    //                //    else
    //                //        path = parent.RemotePath + @"/" + x.ToString();

    //                //    models.Add(new ModelWithChildren
    //                //    {
    //                //        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
    //                //        Label = (parent == null ? x.ToString() : x.ToString()), //path
    //                //        RemotePath = path,
    //                //        Parent = parent,
    //                //        LocalRemote = 0,
    //                //        Update = "dir",
    //                //        DataForChildren = new ArrayList { x.ToString() } //data
    //                //    });
    //                //}


    //                //files
    //                data.Clear();

    //                //foreach (Renci.SshNet.Sftp.SftpFile f in files)
    //                foreach (FtpFileClass f in files)
    //                    if (f.IsRegularFile && !f.IsDirectory && f.Name != "." && f.Name != "..")
    //                    //data.Add(f.Name);
    //                    //foreach (object x in data)
    //                    {
    //                        string path = "";
    //                        if (parent.Label == "")
    //                            path = f.Name;
    //                        else
    //                            path = parent.RemotePath + @"/" + f.Name;

    //                        models.Add(new ModelWithChildren
    //                        {
    //                            //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
    //                            Label = (parent == null ? f.Name : f.Name), //x.ToString() : x.ToString()), //path,
    //                            RemotePath = path,
    //                            Parent = parent,
    //                            Update = "file",
    //                            Now = f.LastWriteTime.ToShortDateString(),
    //                            ChildCount = f.Length,
    //                            LocalRemote = 0,
    //                            Children = null,
    //                            DataForChildren = null //data
    //                        });
    //                    }
    //                //foreach (Renci.SshNet.Sftp.SftpFile f in files)
    //                //    if (f.IsRegularFile && f.Name != "." && f.Name != "..")
    //                //        data.Add(f.Name);

    //                //foreach (object x in data)
    //                //{
    //                //    string path = "";
    //                //    if (parent.Label == "")
    //                //        path = x.ToString();
    //                //    else
    //                //        path = parent.RemotePath + @"/" + x.ToString();

    //                //    models.Add(new ModelWithChildren
    //                //    {
    //                //        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
    //                //        Label = (parent == null ? x.ToString() : x.ToString()), //path,
    //                //        RemotePath = path,
    //                //        Parent = parent,
    //                //        Update = "file",
    //                //        //Now = ,
    //                //        LocalRemote = 0,
    //                //        Children = null,
    //                //        DataForChildren = null //data
    //                //    });
    //                //}

    //            }


    //            if (parent.LocalRemote == 1)
    //            {
    //                string[] dirs = Directory.GetDirectories(localpath);
    //                //LocalPathRoot = localpath;

    //                data.Clear();
    //                //foreach (string s in dirs)
    //                //        data.Add(s);

    //                foreach (string dir in dirs)
    //                {
    //                    string path = "";
    //                    string folderpath = "";
    //                    //if (parent.Label == "")
    //                    //    path = x.ToString();
    //                    //else
    //                    path = dir; // x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
    //                    if (path.LastIndexOf("\\") > 1)
    //                        folderpath = path.Substring(path.LastIndexOf("\\") + 1);
    //                    else
    //                        folderpath = path;

    //                    DateTime datetimedir = Directory.GetLastWriteTime(dir);

    //                    models.Add(new ModelWithChildren
    //                    {
    //                        Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
    //                        LocalPath = path,
    //                        Parent = parent,
    //                        LocalRemote = 1,
    //                        Update = "dir",
    //                        Now = datetimedir.ToShortDateString(),
    //                        DataForChildren = new ArrayList { dir } //data
    //                    });
    //                }

    //                data.Clear();
    //                if (parent.LocalPath != null)
    //                {
    //                    string[] filesindir = Directory.GetFiles(parent.LocalPath);
    //                    //foreach (string sfi in filesindir)
    //                    // data.Add(sfi);

    //                    foreach (string x in filesindir)
    //                    {
    //                        string path = "";
    //                        string folderpath = "";
    //                        //if (parent.Label == "")
    //                        //    path = x.ToString();
    //                        //else
    //                        path = x; // x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
    //                        if (path.LastIndexOf("\\") > 1)
    //                            folderpath = path.Substring(path.LastIndexOf("\\") + 1);
    //                        else
    //                            folderpath = path;
    //                        DateTime datetimefile = File.GetCreationTime(path);
    //                        //long sizefile = 
    //                        FileInfo fileinfo = new FileInfo(path);
    //                        models.Add(new ModelWithChildren
    //                        {
    //                            Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
    //                            LocalPath = path,
    //                            Parent = parent,
    //                            LocalRemote = 1,
    //                            Update = "file",
    //                            Now = datetimefile.ToShortDateString(),
    //                            ChildCount = fileinfo.Length,
    //                            DataForChildren = new ArrayList { x } //data
    //                        });
    //                    }


    //                    //foreach (object x in data)
    //                    //{
    //                    //    string path = "";
    //                    //    string folderpath = "";
    //                    //    //if (parent.Label == "")
    //                    //    //    path = x.ToString();
    //                    //    //else
    //                    //    path = x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
    //                    //    if (path.LastIndexOf("\\") > 1)
    //                    //        folderpath = path.Substring(path.LastIndexOf("\\") + 1);
    //                    //    else
    //                    //        folderpath = path;

    //                    //    models.Add(new ModelWithChildren
    //                    //    {
    //                    //        Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
    //                    //        LocalPath = path,
    //                    //        Parent = parent,
    //                    //        LocalRemote = 1,
    //                    //        Update = "dir",
    //                    //        DataForChildren = new ArrayList { x.ToString() } //data
    //                    //    });
    //                    //}

    //                    //data.Clear();
    //                    //if (parent.LocalPath != null)
    //                    //{
    //                    //    var filesindir = Directory.GetFiles(parent.LocalPath); 
    //                    //    foreach (string sfi in filesindir)
    //                    //     data.Add(sfi);

    //                    //    foreach (object x in data)
    //                    //    {
    //                    //        string path = "";
    //                    //        string folderpath = "";
    //                    //        //if (parent.Label == "")
    //                    //        //    path = x.ToString();
    //                    //        //else
    //                    //        path = x.ToString(); //parent.LocalPath + @"\\" + x.ToString();
    //                    //        if (path.LastIndexOf("\\") > 1)
    //                    //            folderpath = path.Substring(path.LastIndexOf("\\") + 1);
    //                    //        else
    //                    //            folderpath = path;

    //                    //        models.Add(new ModelWithChildren
    //                    //        {
    //                    //            Label = folderpath, // (parent == null ? x.ToString() : x.ToString()), //path
    //                    //            LocalPath = path,
    //                    //            Parent = parent,
    //                    //            LocalRemote = 1,
    //                    //            Update = "file",
    //                    //            DataForChildren = new ArrayList { x.ToString() } //data
    //                    //        });
    //                    //    }


    //                    //    //foreach (object x in data)
    //                    //    //{
    //                    //    //    string path = "";
    //                    //    //    if (parent.Label == "")
    //                    //    //        path = x.ToString();
    //                    //    //    else
    //                    //    //        path = parent.RemotePath + @"/" + x.ToString();

    //                    //    //    models.Add(new ModelWithChildren
    //                    //    //    {
    //                    //    //        //Label = (parent == null ? x.ToString() : parent.Label + "/" + x.ToString()),
    //                    //    //        Label = (parent == null ? x.ToString() : x.ToString()), //path,
    //                    //    //        RemotePath = path,
    //                    //    //        Parent = parent,
    //                    //    //        Update = "file",
    //                    //    //        LocalRemote = 0,
    //                    //    //        Children = null,
    //                    //    //        DataForChildren = null //data
    //                    //    //    });
    //                    //    //}
    //                }
    //            }
    //        }


    //        return models;
    //    }
    //}

}
