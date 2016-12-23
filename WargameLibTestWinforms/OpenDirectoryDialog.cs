using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WargameLibTestWinforms
{
    public partial class OpenDirectoryDialog : Form
    {
        public OpenDirectoryDialog()
        {
            InitializeComponent();
        }

        private void textBoxDirectory_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = Directory.Exists(textBoxDirectory.Text);            
        }

        private void labelDragDrop_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void labelDragDrop_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (data != null && data.Length > 0)
                    textBoxDirectory.Text = data[0];
            }
            catch { }
        }

        private void buttonOpenFileDialog_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Any File|*.*";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxDirectory.Text = Path.GetDirectoryName(dlg.FileName);
            }
        }

        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxDirectory.Text = dlg.SelectedPath;
            }
        }

        public string SelectedDirectory { get { return textBoxDirectory.Text; } }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxDirectory.Text)) return;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
