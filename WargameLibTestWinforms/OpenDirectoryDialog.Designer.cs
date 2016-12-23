namespace WargameLibTestWinforms
{
    partial class OpenDirectoryDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.buttonOpenFileDialog = new System.Windows.Forms.Button();
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelDragDrop = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter the location of the directory or use one of the 3 options below.\r\nFi" +
    "les will be searched recursively. You just have to select the top directory.";
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Location = new System.Drawing.Point(12, 38);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(391, 20);
            this.textBoxDirectory.TabIndex = 1;
            this.textBoxDirectory.TextChanged += new System.EventHandler(this.textBoxDirectory_TextChanged);
            // 
            // buttonOpenFileDialog
            // 
            this.buttonOpenFileDialog.Location = new System.Drawing.Point(131, 61);
            this.buttonOpenFileDialog.Name = "buttonOpenFileDialog";
            this.buttonOpenFileDialog.Size = new System.Drawing.Size(126, 23);
            this.buttonOpenFileDialog.TabIndex = 2;
            this.buttonOpenFileDialog.Text = "Open File Dialog";
            this.buttonOpenFileDialog.UseVisualStyleBackColor = true;
            this.buttonOpenFileDialog.Click += new System.EventHandler(this.buttonOpenFileDialog_Click);
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Location = new System.Drawing.Point(131, 90);
            this.buttonFolderBrowser.Name = "buttonFolderBrowser";
            this.buttonFolderBrowser.Size = new System.Drawing.Size(126, 23);
            this.buttonFolderBrowser.TabIndex = 3;
            this.buttonFolderBrowser.Text = "Folder Browser Dialog";
            this.buttonFolderBrowser.UseVisualStyleBackColor = true;
            this.buttonFolderBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Enabled = false;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(263, 61);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(140, 52);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelDragDrop
            // 
            this.labelDragDrop.AllowDrop = true;
            this.labelDragDrop.BackColor = System.Drawing.Color.White;
            this.labelDragDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDragDrop.Location = new System.Drawing.Point(12, 61);
            this.labelDragDrop.Name = "labelDragDrop";
            this.labelDragDrop.Size = new System.Drawing.Size(113, 52);
            this.labelDragDrop.TabIndex = 5;
            this.labelDragDrop.Text = "Drag\'n\'Drop \r\na Directory or File \r\ninto this area";
            this.labelDragDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelDragDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.labelDragDrop_DragDrop);
            this.labelDragDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.labelDragDrop_DragEnter);
            // 
            // OpenDirectoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 122);
            this.Controls.Add(this.labelDragDrop);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonFolderBrowser);
            this.Controls.Add(this.buttonOpenFileDialog);
            this.Controls.Add(this.textBoxDirectory);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenDirectoryDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Directory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonOpenFileDialog;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelDragDrop;
    }
}