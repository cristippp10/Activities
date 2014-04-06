using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Tests.Common;
//using Renci.SshNet.Tests.Properties;

using Renci.SshNet.Tests.Classes;

using Renci.SshNet.Sftp;
using Renci.SshNet;

using Renci.SshNet.Tests.Properties;

namespace TestWin
{
    public partial class TestFtpSsh : Form
    {
        public TestFtpSsh()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //TestBase testBase = new SftpClientTest();
            //Renci.SshNet.Tests.Classes.SftpClientTest testSftpClent = new Renci.SshNet.Tests.Classes.SftpClientTest();
            //testSftpClent.ExistsTest();

////            string dsahostkey = @"

            string dsahostkey = @"-----BEGIN DSA PRIVATE KEY-----
MIIBvAIBAAKBgQDdQrrwGvknwD+pb9Gpv/vDxj8yqFUg0cUuC/tkjm3u+lQj86Rw
fA8RJOV+OP4Gtvs9zOSsxiuginoB/uEiT+6jbHvyvJeJp2fsDWLx/tVRXxpi8Nwe
bfb455R0wVVZdqnFKsAZLrQAT589EUtLgyVAFQbUP5Fz6px8H8AG0qlybQIVALn7
UOxnbFR7fDfZkq2Pc7ZCVegpAoGBANRKf9vezuz1aGUnUGWILHO09SibHK255fkr
u4//zvCbdawWSZOoU+vMnplLUhNaVMoSyYE/TYOZvvbG9UtPvr8wZGdNPipvIXIB
xlpr+FH0mnSx0qlaRy3PEoDRAmw6msRCzVYV3vZ8ZTEUdt+phuru+pn0W9EugzCM
HikVVhJMAoGASHB5nNlWhpqvungn+otYyGKoVoATkRdRfSiUo3fb0mJLTmxrEhEH
4UOFA/UFdQ0TYXXW0wRX/amynT4iTEXAx0FjxUNK5ryxloeoXiXEH4FK7D8RtJO1
1UsaRrN+nqWiSTVAehl6tzoMCPargGcWeFlAZZnPGN76OD9J1GiKZj8CFQCl8SkF
GWfS+mH8xhLsxh1nI7rfJw==
-----END DSA PRIVATE KEY-----";

            string dsahostkeypass = @"-----BEGIN DSA PRIVATE KEY-----
Proc-Type: 4,ENCRYPTED
DEK-Info: DES-EDE3-CBC,053105DCFC6132E7

nVjIhJr3Eeqk0aBfyYK38B6cF0g35U2acgq5t3zG8fCM5JLSnFcmkkYvohbmCLva
swNHMfmwumoX8Ga94cxGu6vW1qf+IMvgEU4U53DtJqRoeICMwIre1yUq2cCrV+gI
qQ6MAVMDgfMs7HrPs5999m+KoDh7oYsA1l5q+axU/rqM4g3lySr/1oT6oAQx5Qp2
2DYkZEwnX5+NTw6aoMXl2qgIHBVxa7wZRMp9L0yAlEFk8T7fMuPrLSAEUBghcIaP
iBY1OY+M9MgDTTU56ZjLl+DfT3XfKzsZ3fmn1+bLqTRreiuS4/WF6xIa/DQu1sQd
nnjYgnKYcTWSvWWK9AcIVSpoiW2y6FcPkMAIw54ABrzBp4Rz0//Ykwv2Ga7AZxxm
P+lkxKf2RWnh406FxBvBZzwB3rQeeM7QTg2IcFqGnlf005FIikp6SlyhZ/3M/Nl3
FW235vuO37jLCL8qosGt4NOWAstXaxDujfIb/Q+IYxUpWZrdiH5tM/mUXARK0Sjf
D8DHbFwAT2mUv1QxRXYJO1y4pENboEzT6LUqxJgE+ae/F/29g2RD9DhtwqKqWjhM
7jB0kNVZrz3qUKnoJHIozA==
-----END DSA PRIVATE KEY-----";

            string rsahostkey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEogIBAAKCAQEA8ZsD8jgH7ySXnd4dprEFFkJ+zs1ne81E8febjR8hekiKrc+D
9GjeEykchy0PsrsBrf2F8J3GtLNISVerkl+EnVh1E0pi9Vllc1vcpQHvBeNPG/jl
9Dnum/DcaaMKLot+ARXjBuMX/xJVfnlgkvfpBvoq4QzQ6E27rOMbcPlvrN7KeYba
orsTpDMUraHX5u99P1evvw7sOuNl3lc9YpIgmjHT6dWUGTqBx9T2SEKXBRVk2NNb
d2UctQkr5BJn1gGehA+1KYS30FMiCSN8F8ZOlpYj1+K6xzs0srq1z30LailIyy7Q
mYp739BJlOF3cVywdcFXGre35H2cJylcNV2e+QIBIwKCAQAbnK/+bM207j02nQqz
9vlEX17zECkVdouJXiBn5kz4CEpdAcXwC/wfcmmpDHbSmQeBmVEi0DP3ZPJRhlzA
RW493jlECIBWjd+1aZWPM2vGKJtTp2q802u6DOX9cbgFUcVB899ugFqDjREVxLqU
dBqhtjDJQ0sTPBVy2CBkrsrvLy7AZd4LlaqxpdTdpkD/auUxg4zdThHT/XeQ0V0G
3BS5vGehfqgkDOQpQSm7HexM+9GDJnnzMdNXjmWBPxZU49QAlxujZeZ1M2IgKwMq
eNkWAOnZJidfsfwYluSCi8OHPN2i1s/b7pgn6ffb50S/k2mmhTHxNChDdGTlW91G
6CFLAoGBAPlGA4qzZCG4SLBQicGhhXZlwaKKfTvWNi4xNPZeoJezC0G+yZZT7IIV
zCj83t3dteaRRw15e+7NuIXZx3zl2hANfEpBZwnS4sOekvbS9/S7cEVzOEk53jRU
TOtHRsvNxS2xK3RywqoaqzcPaK2Df917yzbqvEajMRudRPrsTvYdAoGBAPggB/kJ
+VxZf13JqV2KgrFoVvykJpRlw2F5+lkH69ON9gdl976J3TNJDqAmHeWFxBUL+6Lt
9TSpMq3fYueJXg9xaTkSYg177sPRGCmRLO5aneA4nJkIB6KHRXsR71C9D4fJK8Fi
YB5n5dnBUTBhkvaI6KsxcxDzEg2zAJEcn4WNAoGBAOsHf66pI+VHWnpakINdxvqa
dL3TCFz9K8UnFK3G7y/x9Kuz4qOuNsPLaLjua7s+wXL+ASn2MwW6pqoWekKPkxZz
HWqVb8dvEFIKipDyzIepadsU3UxbIfbTI/PG3FnCAw3S7nUbvtrl8eN07arpsxKn
63zr81iLPO4mkX7ezhs/AoGADi23UA8y0hO+Ip3PKeqoYei0g3cYJbysFDLbGwfE
VTtP4ypl7aF6WrO3sWFDiXVbqW5mJpFBNjWN3gzD0rdkdek5NnYUO0jpEoD6EQD7
QNH6ZJWFSpK+m5Kzg2AcTcGpHbO5W4H23SqCKbNGd8sPtlD3Wj1XCEhnbn9B3GgJ
HZMCgYEAjn9BH4fcobMaM15AV0s6sCEyr/vzn3QBfoIBqqqlipMqOtPDv8oyvGO4
5tSpMrTKf4e3YAMPA4TXRAiVLjeRYlhdj3He0LkAuZZabrHeRmWqjDruMWd1TcKZ
3o8DLgYbSH7eGXUJ+euM4spKr5OLkBEkmH7Of6Qxss2njvjVNTE=
-----END RSA PRIVATE KEY-----";

            string rsahostkeypass = @"-----BEGIN RSA PRIVATE KEY-----
Proc-Type: 4,ENCRYPTED
DEK-Info: DES-EDE3-CBC,B3475536EDD1B442

Xa6Y7FYTfz19CMzPcVbCpBHEh8x3tnA9PutKEDNMwKbR+NVUvBx5fN1QjrLpltCr
uX7yD3vLCeyihg4oaK3nTBZBcsB/1TZx+MlLgpvMfV1JKmbKCmebBZ3lUpsypHlG
FpCY41miFwdHmBe6tuwL9XA3vz26eJwSgJGMkVN9EBvNbiOHinEPhSW0whzBfbv+
OfseG73gvHc4jZS6Sw8h5VDBAmlldJEfkP/s/1/iTbCXFQ22xRb4Z6NilEyKiWpB
nQviXmaucTWCEuNF5QDA7oV7Ugwm5cAXuBqFIs9ZGaKV4/XpfX1tClOLfB3Lguh+
bbkwjPb0ztlhKa4gkwXiMs0S/lhoueXBae4QStM0qJBXHtFhhRYIn4JeIZ8CJ0k6
SMP7QVfPf5aJIaa8t+SlpvtIFTIkEhTViOCl+udT04670DGwmJUgrJAV0r+/Ytf7
Mi+m3DagN7gGmCvYo+7r7EBl1G6e3hCSYm0rFxGOBesmCWriRoeRpxirWnkrns57
D57pEC1hg90IdycCGpiwqubGDKFljuMLiVd2w0onVhudShPszP+nJAaq18wUB+rE
mtBv+GlpqCITREB4lG2noP4r9P9lgrOTqmKvWjvUTQjfS3u0XO/1aQllKlwe97C7
mfOxcZQWy5F7+9CiWpDpomW7Eso89ja6uyupw4Q4gsm7EUacqOOaVxHrm3MVhYjE
Bfk9I5agKFqeHdjBUUP0DQ6X7JUEtb/Ri8ZrFnyT8sBG7JYnMTXfjPQqdR493cp2
hWI5reZLi4CCUqt4Pcmhm2vtwJz5HXChARPYq2C3DhdJHcdhxUr97rfTGE1w8mPY
JcwSFnNN47UBcDg6nvSfY3SJKV0gmmqz9fEw1pBoCrudJKw9U0vQrNoCEJOpEETG
4XojbAMsTr0Ps5fI2X1VbVYWtU1uyZxqF8KaTCTN1Paapmqaq4N+qIFrrXA+PTH+
dyaMLmYJ263Gy5eNkCZMeWLDFZ9WHX/Zx2ERMXfI6fyGImXkb6E0Dia+bB087BZP
9C5gHAvZIjv+FosZrViFqDfrV5hDXL6bO3+V3zieemRxRCTvMtk+RXUJDd50qIOW
gKNcSbevLPOyQH7eQbR+fU4KtJDUigbTFunSn2MZkDl2GDDlKI35wUAVr5yGsJbE
yiIQe5DgLGZcMiEpqbhuqSfuOw0cUlFVyKeNZ/Hr701HWngLt677IY8ExyuNbBfT
PRaes+hcjJ1QmJoRHZx9rQ3w0IpezCpRkRLRKJzzuQZOuwd95whKFXroFsdeaHxO
hS4PqlLbuSMLiSIaPSZM6Huc4kb5lqCaxg/SBlXTCX17Z/8TFoqV/wCJz17XnkH6
9WtKAC2TwKxiLZ2Qzwr2XV48lASugIOZkSW2qxM9ui+b1T9ICFKRGLn/UB//pOiG
270hNJDLB/BKRExjS+RXeOpdAIJB5XsAEp8h56ub9emhhf9tCEXOn7PN7HbMCnQh
7k8EpAG0h5StLUhY1HHvynVz2/qyMvZa/bIaaudL2565Z6nDU+iBxed7O1qrbRAH
Vakr7Sa3K5niCyH5kxdyO1t29l1ksBqpDUrj+vViFuLkd3XIiui8IA==
-----END RSA PRIVATE KEY-----";


            Renci.SshNet.PrivateKeyFile pkdsa = new Renci.SshNet.PrivateKeyFile(new MemoryStream(
                Encoding.ASCII.GetBytes(dsahostkey))); //, Resources.PASSWORD)

            Renci.SshNet.PrivateKeyFile pkdsapass = new Renci.SshNet.PrivateKeyFile(new MemoryStream(
                Encoding.ASCII.GetBytes(dsahostkeypass)), "tester");

            Renci.SshNet.PrivateKeyFile pkrsa = new Renci.SshNet.PrivateKeyFile(new MemoryStream(
                Encoding.ASCII.GetBytes(rsahostkey)));

            Renci.SshNet.PrivateKeyFile pkrsapass = new Renci.SshNet.PrivateKeyFile(new MemoryStream(
                Encoding.ASCII.GetBytes(rsahostkeypass)), "tester");
              
            string host = "192.168.0.102"; // TODO: Initialize to an appropriate value
            string username = "one"; // TODO: Initialize to an appropriate value
            Renci.SshNet.PrivateKeyFile[] keyFiles = new Renci.SshNet.PrivateKeyFile[] 
            {
                //////new PrivateKeyFile(new MemoryStream(Encoding.ASCII.GetBytes(Resources.INVALID_KEY))),
                //////new PrivateKeyFile(new MemoryStream(Encoding.ASCII.GetBytes(Resources.DSA_KEY_WITH_PASS)), Resources.PASSWORD),
                //////new PrivateKeyFile(new MemoryStream(Encoding.ASCII.GetBytes(Resources.RSA_KEY_WITH_PASS)), Resources.PASSWORD),
                //////new PrivateKeyFile(new MemoryStream(Encoding.ASCII.GetBytes(Resources.RSA_KEY_WITHOUT_PASS))),
                ////new PrivateKeyFile(new MemoryStream(Encoding.ASCII.GetBytes(Resources.DSA_KEY_WITHOUT_PASS)))
                
                //pkdsa,
                //pkdsapass,
                //pkrsapass,
                pkrsa
            }
            ; // TODO: Initialize to an appropriate value
            
            string password = "one";

            Renci.SshNet.SftpClient sftpClient = new Renci.SshNet.SftpClient(host, 49495, username, password);
            //Renci.SshNet.SftpClient sftpClient = new Renci.SshNet.SftpClient(host, 22, username, keyFiles);
            //Renci.SshNet.ConnectionInfo co_info = //new Renci.SshNet.ConnectionInfo(host, username);
            //new Renci.SshNet.PasswordConnectionInfo(host, 22, username, password);
            //Renci.SshNet.Session target = new Renci.SshNet.Session(co_info); //sftpClient.ConnectionInfo);

            
            //target.Connect();
            //Renci.SshNet.SftpClient sftpClient = new Renci.SshNet.SftpClient(co_info);

            sftpClient.Connect();


            Renci.SshNet.Session target = sftpClient.Session;
            //bool bIsConnected = target.IsConnected;
            //target.UnRegisterMessage(messageName);


            IEnumerable<SftpFile> ief = sftpClient.ListDirectory("\\");

            //string newRemoteDir = "Downloads/new";

            //if (!sftpClient.Exists(newRemoteDir))
            //    sftpClient.CreateDirectory(newRemoteDir);
  

            //SftpFile sf = sftpClient.Get("\\Test1.txt");
            //string[] strLines = sftpClient.ReadAllLines(sf.FullName);

            ////var files = sftpClient.ListDirectory(null);
            //var files2 = sftpClient.ListDirectory("Documents");

            //Renci.SshNet.Tests.Classes.SftpClientTest sftpTest = new SftpClientTest();
            ////sftpTest.Sftp_Multiple_Async_Download_Files(sftpClient, @"G:\TestDownload", "Documents", "*.*");
            //////sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, @"G:\Bak\Sdcard\new_patch.zip");
            ////sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, @"G:\Bak\BooksIT", "Downloads", "*.*");

            //sftpTest.Sftp_BeginSynchronizeDirectories(sftpClient, @"G:\Bak\BooksIT", "Downloads", "*.*");
            
            ////var filename = @"G:\Bak\Factura_ENEL_nr_3MF11997102_16.11.pdf";// Path.GetTempFileName();
            //////////this.CreateTestFile(filename, 1);
            ////sftpClient.UploadFile(File.OpenRead(filename), "Documents/fact_E.pdf");

            ////sftpClient.DownloadFile("fact_E.pdf", File.Create(@"G:\fact_En_G.pdf") );

            ////var async1 = sftpClient.BeginListDirectory("/", null, null);
            ////var async2 = sftpClient.BeginDownloadFile("fact_E.pdf", File.Create(@"G:\fact_E_bak.pdf"), null, null); //new MemoryStream()
            ////sftpClient.EndDownloadFile(async2);

            target.Disconnect();
            //sftpClient.Disconnect();
        }
    }
}
