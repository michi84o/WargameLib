using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WargameLib;

namespace WargameLibTestWinforms
{
    public partial class ExtractDirDialog : Form
    {
        public ExtractDirDialog()
        {
            InitializeComponent();
            buttonUnpack.Enabled = File.Exists(textBoxFile.Text);
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFile.Text = dlg.FileName;
            }
        }

        private void buttonUnpack_Click(object sender, EventArgs e)
        {
            var file = textBoxFile.Text;
            if (!File.Exists(file)) return;
            var dir = Path.GetDirectoryName(file);
            var filename = Path.GetFileName(file);
            if (filename != "WARGAME.DIR")
                if (MessageBox.Show(
                    "File has not the name WARGAME.DIR. Continue?", "",
                    MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            buttonUnpack.Enabled = false;
            DIR.ExportDIR(dir, filename);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void textBoxFile_TextChanged(object sender, EventArgs e)
        {
            buttonUnpack.Enabled = File.Exists(textBoxFile.Text);
        }
    }
}
