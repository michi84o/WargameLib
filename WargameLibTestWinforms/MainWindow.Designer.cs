namespace WargameLibTestWinforms
{
    partial class MainWindow
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
            this.groupBoxWADViewer = new System.Windows.Forms.GroupBox();
            this.buttonExportPng = new System.Windows.Forms.Button();
            this.label4x = new System.Windows.Forms.Label();
            this.label2x = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1x = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelImage = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxWADContent = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.extractDIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseWADDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPalette = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxWADViewer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxWADViewer
            // 
            this.groupBoxWADViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWADViewer.Controls.Add(this.label6);
            this.groupBoxWADViewer.Controls.Add(this.panelPalette);
            this.groupBoxWADViewer.Controls.Add(this.buttonExportPng);
            this.groupBoxWADViewer.Controls.Add(this.label4x);
            this.groupBoxWADViewer.Controls.Add(this.label2x);
            this.groupBoxWADViewer.Controls.Add(this.label5);
            this.groupBoxWADViewer.Controls.Add(this.label1x);
            this.groupBoxWADViewer.Controls.Add(this.label4);
            this.groupBoxWADViewer.Controls.Add(this.textBoxHeader);
            this.groupBoxWADViewer.Controls.Add(this.label3);
            this.groupBoxWADViewer.Controls.Add(this.panelImage);
            this.groupBoxWADViewer.Controls.Add(this.label2);
            this.groupBoxWADViewer.Controls.Add(this.listBoxWADContent);
            this.groupBoxWADViewer.Controls.Add(this.label1);
            this.groupBoxWADViewer.Controls.Add(this.listBoxFiles);
            this.groupBoxWADViewer.Location = new System.Drawing.Point(12, 27);
            this.groupBoxWADViewer.Name = "groupBoxWADViewer";
            this.groupBoxWADViewer.Size = new System.Drawing.Size(660, 523);
            this.groupBoxWADViewer.TabIndex = 0;
            this.groupBoxWADViewer.TabStop = false;
            this.groupBoxWADViewer.Text = "WAD Viewer";
            // 
            // buttonExportPng
            // 
            this.buttonExportPng.Enabled = false;
            this.buttonExportPng.Location = new System.Drawing.Point(300, 12);
            this.buttonExportPng.Name = "buttonExportPng";
            this.buttonExportPng.Size = new System.Drawing.Size(99, 23);
            this.buttonExportPng.TabIndex = 12;
            this.buttonExportPng.Text = "Export as PNG";
            this.buttonExportPng.UseVisualStyleBackColor = true;
            this.buttonExportPng.Click += new System.EventHandler(this.buttonExportPng_Click);
            // 
            // label4x
            // 
            this.label4x.BackColor = System.Drawing.Color.White;
            this.label4x.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4x.Location = new System.Drawing.Point(520, 14);
            this.label4x.Name = "label4x";
            this.label4x.Size = new System.Drawing.Size(30, 16);
            this.label4x.TabIndex = 11;
            this.label4x.Text = "4x";
            this.label4x.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4x.Click += new System.EventHandler(this.label4x_Click);
            // 
            // label2x
            // 
            this.label2x.BackColor = System.Drawing.Color.White;
            this.label2x.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2x.Location = new System.Drawing.Point(484, 14);
            this.label2x.Name = "label2x";
            this.label2x.Size = new System.Drawing.Size(30, 16);
            this.label2x.TabIndex = 10;
            this.label2x.Text = "2x";
            this.label2x.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2x.Click += new System.EventHandler(this.label2x_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Zoom:";
            // 
            // label1x
            // 
            this.label1x.BackColor = System.Drawing.Color.LightGreen;
            this.label1x.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1x.Location = new System.Drawing.Point(448, 14);
            this.label1x.Name = "label1x";
            this.label1x.Size = new System.Drawing.Size(30, 16);
            this.label1x.TabIndex = 8;
            this.label1x.Text = "1x";
            this.label1x.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1x.Click += new System.EventHandler(this.label1x_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Header Data:";
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHeader.Location = new System.Drawing.Point(6, 347);
            this.textBoxHeader.Multiline = true;
            this.textBoxHeader.Name = "textBoxHeader";
            this.textBoxHeader.ReadOnly = true;
            this.textBoxHeader.Size = new System.Drawing.Size(126, 170);
            this.textBoxHeader.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "WAD Image:";
            // 
            // panelImage
            // 
            this.panelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImage.BackColor = System.Drawing.Color.LightGray;
            this.panelImage.Location = new System.Drawing.Point(138, 37);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(413, 480);
            this.panelImage.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "WAD Content:";
            // 
            // listBoxWADContent
            // 
            this.listBoxWADContent.FormattingEnabled = true;
            this.listBoxWADContent.Location = new System.Drawing.Point(6, 190);
            this.listBoxWADContent.Name = "listBoxWADContent";
            this.listBoxWADContent.Size = new System.Drawing.Size(126, 134);
            this.listBoxWADContent.TabIndex = 2;
            this.listBoxWADContent.SelectedValueChanged += new System.EventHandler(this.listBoxWADContent_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WAD Files:";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(6, 37);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(126, 134);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.SelectedValueChanged += new System.EventHandler(this.listBoxFiles_SelectedValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractDIRToolStripMenuItem,
            this.chooseWADDirectoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // extractDIRToolStripMenuItem
            // 
            this.extractDIRToolStripMenuItem.Name = "extractDIRToolStripMenuItem";
            this.extractDIRToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.extractDIRToolStripMenuItem.Text = "Extract DIR";
            this.extractDIRToolStripMenuItem.Click += new System.EventHandler(this.extractDIRToolStripMenuItem_Click);
            // 
            // chooseWADDirectoryToolStripMenuItem
            // 
            this.chooseWADDirectoryToolStripMenuItem.Name = "chooseWADDirectoryToolStripMenuItem";
            this.chooseWADDirectoryToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.chooseWADDirectoryToolStripMenuItem.Text = "Choose WAD Directory";
            this.chooseWADDirectoryToolStripMenuItem.Click += new System.EventHandler(this.chooseWADDirectoryToolStripMenuItem_Click);
            // 
            // panelPalette
            // 
            this.panelPalette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPalette.BackColor = System.Drawing.Color.LightGray;
            this.panelPalette.Location = new System.Drawing.Point(557, 37);
            this.panelPalette.Name = "panelPalette";
            this.panelPalette.Size = new System.Drawing.Size(89, 353);
            this.panelPalette.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Palette:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 562);
            this.Controls.Add(this.groupBoxWADViewer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "Wargame Library Test";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            this.groupBoxWADViewer.ResumeLayout(false);
            this.groupBoxWADViewer.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxWADViewer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem extractDIRToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxWADContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.ToolStripMenuItem chooseWADDirectoryToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.Label label1x;
        private System.Windows.Forms.Label label4x;
        private System.Windows.Forms.Label label2x;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonExportPng;
        private System.Windows.Forms.Panel panelPalette;
        private System.Windows.Forms.Label label6;
    }
}

