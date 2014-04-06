using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TreeListViewDragDrop {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 frm1 = new Form1();
            frm1.host = "192.168.0.104";
            frm1.port = 21;
            frm1.username = "one";
            frm1.password = "one";
            frm1.FtpMode = 1;
            frm1.LocalPathRoot = @"G:\Bak";
            //frm1.UsedForDownload = true;
            //frm1.UsedForUpload = true;

            Application.Run(frm1);
            string str = frm1.LocalPathRoot;
            //Application.Run(new Form1());
        }
    }
}
