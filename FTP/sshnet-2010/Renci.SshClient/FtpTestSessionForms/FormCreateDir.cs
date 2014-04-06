using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TreeListViewDragDrop
{
    public partial class FormCreateDir : Form
    {
        public FormCreateDir()
        {
            InitializeComponent();
            NewDir = "";
        }

        public string NewDir;
        private void btnOk_Click(object sender, EventArgs e)
        {
            NewDir = txtNewDir.Text;
        }
    }
}
