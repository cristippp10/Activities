using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.SshNet.Tests.Common;
using Renci.SshNet.Tests.Properties;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Renci.SshNet.Tests.Classes
{
    /// <summary>
    /// Implementation of the SSH File Transfer Protocol (SFTP) over SSH.
    /// </summary>
    public partial class SftpClientTest : TestBase
    {
        [TestMethod]
        [TestCategory("Sftp")]
        public void Test_Sftp_SynchronizeDirectories()
        {
            using (var sftp = new SftpClient(Resources.HOST, Resources.USERNAME, Resources.PASSWORD))
            {
                sftp.Connect();

                string uploadedFileName = Path.GetTempFileName();

                string sourceDir = Path.GetDirectoryName(uploadedFileName);
                string destDir = "/";
                string searchPattern = Path.GetFileName(uploadedFileName);
                var upLoadedFiles = sftp.SynchronizeDirectories(sourceDir, destDir, searchPattern);

                Assert.IsTrue(upLoadedFiles.Count() > 0);

                foreach (var file in upLoadedFiles)
                {
                    Debug.WriteLine(file.FullName);
                }

                sftp.Disconnect();
            }
        }

        [TestMethod]
        [TestCategory("Sftp")]
        public void Test_Sftp_BeginSynchronizeDirectories()
        {
            using (var sftp = new SftpClient(Resources.HOST, Resources.USERNAME, Resources.PASSWORD))
            {
                sftp.Connect();

                string uploadedFileName = Path.GetTempFileName();

                string sourceDir = Path.GetDirectoryName(uploadedFileName);
                string destDir = "/";
                string searchPattern = Path.GetFileName(uploadedFileName);

                var asyncResult = sftp.BeginSynchronizeDirectories(sourceDir,
                    destDir,
                    searchPattern,
                    null,
                    null
                );

                // Wait for the WaitHandle to become signaled.
                asyncResult.AsyncWaitHandle.WaitOne(1000);

                var upLoadedFiles = sftp.EndSynchronizeDirectories(asyncResult);

                Assert.IsTrue(upLoadedFiles.Count() > 0);

                foreach (var file in upLoadedFiles)
                {
                    Debug.WriteLine(file.FullName);
                }

                // Close the wait handle.
                asyncResult.AsyncWaitHandle.Close();

                sftp.Disconnect();
            }
        }

        [TestMethod]
        [TestCategory("Sftp")]
        public void Sftp_BeginSynchronizeDirectories(SftpClient sftp, string localPath, string remotePath, string pattern)
        {
            //using (var sftp = new SftpClient(Resources.HOST, Resources.USERNAME, Resources.PASSWORD))
            //{
                //sftp.Connect();

                //string uploadedFileName = Path.GetTempFileName();

                string sourceDir = localPath; //Path.GetDirectoryName(localPath);// (uploadedFileName);
                string destDir = remotePath; //"Documents";
                string searchPattern = pattern; //Path.GetFileName(localPath); //(uploadedFileName);

                var dirs = Directory.EnumerateDirectories(localPath);
                try
                {
                    sftp.CreateDirectory(destDir);
                }
                catch { }

                foreach (string dir in dirs)
                {
                    string newRemoteDir = "";
                    newRemoteDir = remotePath + "/" + dir.Substring(localPath.Length + 0);

                    //if (!sftp.Exists(newRemoteDir))
                    try
                    {
                        sftp.CreateDirectory(newRemoteDir);
                    }
                    catch { }

                    Sftp_BeginSynchronizeDirectories(sftp, dir, newRemoteDir, pattern);
                }

                var asyncResult = sftp.BeginSynchronizeDirectories(sourceDir,
                    destDir,
                    searchPattern,
                    null,
                    null
                );

                // Wait for the WaitHandle to become signaled.
                asyncResult.AsyncWaitHandle.WaitOne(1000);

                var upLoadedFiles = sftp.EndSynchronizeDirectories(asyncResult);

                //Assert.IsTrue(upLoadedFiles.Count() > 0);

                //foreach (var file in upLoadedFiles)
                //{
                //    Debug.WriteLine(file.FullName);
                //}

                // Close the wait handle.
                asyncResult.AsyncWaitHandle.Close();

                //sftp.Disconnect();
            //}
        }

    }
}