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
            this.label6 = new System.Windows.Forms.Label();
            this.buttonExportPng = new System.Windows.Forms.Button();
            this.label4x = new System.Windows.Forms.Label();
            this.label2x = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1x = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxWADContent = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.extractDIRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseWADDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelImageScroll = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbSelectedVol = new System.Windows.Forms.TextBox();
            this.labelZoomLevel14 = new System.Windows.Forms.Label();
            this.labelZoomLevel12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelZoomLevel1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.listBoxVolFiles = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSelectedTile = new System.Windows.Forms.TextBox();
            this.lbPolygons = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSelectedPolygon = new System.Windows.Forms.TextBox();
            this.lbVertices = new System.Windows.Forms.ListBox();
            this.lbTiles = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelLevelScroll = new System.Windows.Forms.Panel();
            this.panelLevel = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.busyBar = new System.Windows.Forms.ToolStripProgressBar();
            this.busyLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imagePanel = new WargameLibTestWinforms.ImagePanel();
            this.palettePanel = new WargameLibTestWinforms.PalettePanel();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelImageScroll.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelLevelScroll.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(897, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Palette:";
            // 
            // buttonExportPng
            // 
            this.buttonExportPng.Enabled = false;
            this.buttonExportPng.Location = new System.Drawing.Point(232, 6);
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
            this.label4x.Location = new System.Drawing.Point(452, 8);
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
            this.label2x.Location = new System.Drawing.Point(416, 8);
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
            this.label5.Location = new System.Drawing.Point(337, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Zoom:";
            // 
            // label1x
            // 
            this.label1x.BackColor = System.Drawing.Color.LightGreen;
            this.label1x.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1x.Location = new System.Drawing.Point(380, 8);
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
            this.label4.Location = new System.Drawing.Point(6, 352);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Header Data:";
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxHeader.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHeader.Location = new System.Drawing.Point(6, 368);
            this.textBoxHeader.Multiline = true;
            this.textBoxHeader.Name = "textBoxHeader";
            this.textBoxHeader.ReadOnly = true;
            this.textBoxHeader.Size = new System.Drawing.Size(126, 159);
            this.textBoxHeader.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "WAD Image:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "WAD Content:";
            // 
            // listBoxWADContent
            // 
            this.listBoxWADContent.FormattingEnabled = true;
            this.listBoxWADContent.Location = new System.Drawing.Point(6, 211);
            this.listBoxWADContent.Name = "listBoxWADContent";
            this.listBoxWADContent.Size = new System.Drawing.Size(126, 134);
            this.listBoxWADContent.TabIndex = 2;
            this.listBoxWADContent.SelectedValueChanged += new System.EventHandler(this.listBoxWADContent_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "WAD Files:";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(6, 32);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(126, 160);
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
            this.menuStrip1.Size = new System.Drawing.Size(1013, 24);
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
            this.chooseWADDirectoryToolStripMenuItem.Size = new System.Drawing.Size(149, 20);
            this.chooseWADDirectoryToolStripMenuItem.Text = "Choose DATOS Directory";
            this.chooseWADDirectoryToolStripMenuItem.Click += new System.EventHandler(this.chooseDatosDirectoryToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1009, 573);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.palettePanel);
            this.tabPage1.Controls.Add(this.panelImageScroll);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonExportPng);
            this.tabPage1.Controls.Add(this.listBoxFiles);
            this.tabPage1.Controls.Add(this.label4x);
            this.tabPage1.Controls.Add(this.listBoxWADContent);
            this.tabPage1.Controls.Add(this.label2x);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textBoxHeader);
            this.tabPage1.Controls.Add(this.label1x);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1001, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "WAD Viewer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelImageScroll
            // 
            this.panelImageScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelImageScroll.AutoScroll = true;
            this.panelImageScroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImageScroll.Controls.Add(this.imagePanel);
            this.panelImageScroll.Location = new System.Drawing.Point(138, 32);
            this.panelImageScroll.Name = "panelImageScroll";
            this.panelImageScroll.Size = new System.Drawing.Size(756, 495);
            this.panelImageScroll.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbSelectedVol);
            this.tabPage2.Controls.Add(this.labelZoomLevel14);
            this.tabPage2.Controls.Add(this.labelZoomLevel12);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.labelZoomLevel1);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.listBoxVolFiles);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tbSelectedTile);
            this.tabPage2.Controls.Add(this.lbPolygons);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.tbSelectedPolygon);
            this.tabPage2.Controls.Add(this.lbVertices);
            this.tabPage2.Controls.Add(this.lbTiles);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.panelLevelScroll);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1001, 547);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Level Viewer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbSelectedVol
            // 
            this.tbSelectedVol.Location = new System.Drawing.Point(167, 32);
            this.tbSelectedVol.Multiline = true;
            this.tbSelectedVol.Name = "tbSelectedVol";
            this.tbSelectedVol.ReadOnly = true;
            this.tbSelectedVol.Size = new System.Drawing.Size(155, 108);
            this.tbSelectedVol.TabIndex = 18;
            // 
            // labelZoomLevel14
            // 
            this.labelZoomLevel14.BackColor = System.Drawing.Color.White;
            this.labelZoomLevel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelZoomLevel14.Location = new System.Drawing.Point(297, 7);
            this.labelZoomLevel14.Name = "labelZoomLevel14";
            this.labelZoomLevel14.Size = new System.Drawing.Size(30, 16);
            this.labelZoomLevel14.TabIndex = 17;
            this.labelZoomLevel14.Text = "1/4";
            this.labelZoomLevel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelZoomLevel14.Click += new System.EventHandler(this.labelZoomLevel14_Click);
            // 
            // labelZoomLevel12
            // 
            this.labelZoomLevel12.BackColor = System.Drawing.Color.White;
            this.labelZoomLevel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelZoomLevel12.Location = new System.Drawing.Point(261, 7);
            this.labelZoomLevel12.Name = "labelZoomLevel12";
            this.labelZoomLevel12.Size = new System.Drawing.Size(30, 16);
            this.labelZoomLevel12.TabIndex = 16;
            this.labelZoomLevel12.Text = "1/2x";
            this.labelZoomLevel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelZoomLevel12.Click += new System.EventHandler(this.labelZoomLevel12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(182, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Zoom:";
            // 
            // labelZoomLevel1
            // 
            this.labelZoomLevel1.BackColor = System.Drawing.Color.LightGreen;
            this.labelZoomLevel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelZoomLevel1.Location = new System.Drawing.Point(225, 7);
            this.labelZoomLevel1.Name = "labelZoomLevel1";
            this.labelZoomLevel1.Size = new System.Drawing.Size(30, 16);
            this.labelZoomLevel1.TabIndex = 14;
            this.labelZoomLevel1.Text = "1";
            this.labelZoomLevel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelZoomLevel1.Click += new System.EventHandler(this.labelZoomLevel1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "VOL Files:";
            // 
            // listBoxVolFiles
            // 
            this.listBoxVolFiles.FormattingEnabled = true;
            this.listBoxVolFiles.Location = new System.Drawing.Point(6, 32);
            this.listBoxVolFiles.Name = "listBoxVolFiles";
            this.listBoxVolFiles.Size = new System.Drawing.Size(155, 108);
            this.listBoxVolFiles.TabIndex = 10;
            this.listBoxVolFiles.SelectedValueChanged += new System.EventHandler(this.listBoxVolFiles_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Polygons";
            // 
            // tbSelectedTile
            // 
            this.tbSelectedTile.Location = new System.Drawing.Point(167, 246);
            this.tbSelectedTile.Multiline = true;
            this.tbSelectedTile.Name = "tbSelectedTile";
            this.tbSelectedTile.ReadOnly = true;
            this.tbSelectedTile.Size = new System.Drawing.Size(155, 135);
            this.tbSelectedTile.TabIndex = 9;
            // 
            // lbPolygons
            // 
            this.lbPolygons.FormattingEnabled = true;
            this.lbPolygons.Location = new System.Drawing.Point(6, 159);
            this.lbPolygons.Name = "lbPolygons";
            this.lbPolygons.Size = new System.Drawing.Size(155, 108);
            this.lbPolygons.TabIndex = 1;
            this.lbPolygons.SelectedValueChanged += new System.EventHandler(this.lbPolygons_SelectedValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(164, 384);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Vertices";
            // 
            // tbSelectedPolygon
            // 
            this.tbSelectedPolygon.Location = new System.Drawing.Point(6, 273);
            this.tbSelectedPolygon.Multiline = true;
            this.tbSelectedPolygon.Name = "tbSelectedPolygon";
            this.tbSelectedPolygon.ReadOnly = true;
            this.tbSelectedPolygon.Size = new System.Drawing.Size(155, 108);
            this.tbSelectedPolygon.TabIndex = 4;
            // 
            // lbVertices
            // 
            this.lbVertices.FormattingEnabled = true;
            this.lbVertices.Location = new System.Drawing.Point(167, 400);
            this.lbVertices.Name = "lbVertices";
            this.lbVertices.Size = new System.Drawing.Size(155, 69);
            this.lbVertices.TabIndex = 7;
            // 
            // lbTiles
            // 
            this.lbTiles.FormattingEnabled = true;
            this.lbTiles.Location = new System.Drawing.Point(167, 159);
            this.lbTiles.Name = "lbTiles";
            this.lbTiles.Size = new System.Drawing.Size(155, 82);
            this.lbTiles.TabIndex = 5;
            this.lbTiles.SelectedValueChanged += new System.EventHandler(this.lbTiles_SelectedValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(164, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Tiles";
            // 
            // panelLevelScroll
            // 
            this.panelLevelScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLevelScroll.AutoScroll = true;
            this.panelLevelScroll.Controls.Add(this.panelLevel);
            this.panelLevelScroll.Location = new System.Drawing.Point(335, 6);
            this.panelLevelScroll.Name = "panelLevelScroll";
            this.panelLevelScroll.Size = new System.Drawing.Size(660, 534);
            this.panelLevelScroll.TabIndex = 13;
            // 
            // panelLevel
            // 
            this.panelLevel.BackColor = System.Drawing.Color.LightGray;
            this.panelLevel.Location = new System.Drawing.Point(3, 3);
            this.panelLevel.Name = "panelLevel";
            this.panelLevel.Size = new System.Drawing.Size(654, 528);
            this.panelLevel.TabIndex = 12;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.busyBar,
            this.busyLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 579);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1013, 22);
            this.statusStrip.TabIndex = 17;
            this.statusStrip.Text = "statusStrip1";
            // 
            // busyBar
            // 
            this.busyBar.Name = "busyBar";
            this.busyBar.Size = new System.Drawing.Size(100, 16);
            // 
            // busyLabel
            // 
            this.busyLabel.Name = "busyLabel";
            this.busyLabel.Size = new System.Drawing.Size(28, 17);
            this.busyLabel.Text = "XXX";
            // 
            // imagePanel
            // 
            this.imagePanel.Image = null;
            this.imagePanel.Location = new System.Drawing.Point(3, 3);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(748, 487);
            this.imagePanel.TabIndex = 0;
            this.imagePanel.Zoom = 1;
            // 
            // palettePanel
            // 
            this.palettePanel.Image = null;
            this.palettePanel.Location = new System.Drawing.Point(900, 32);
            this.palettePanel.Name = "palettePanel";
            this.palettePanel.Size = new System.Drawing.Size(89, 353);
            this.palettePanel.TabIndex = 16;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 601);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "Wargame Library Test";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panelImageScroll.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelLevelScroll.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Label label1x;
        private System.Windows.Forms.Label label4x;
        private System.Windows.Forms.Label label2x;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonExportPng;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSelectedTile;
        private System.Windows.Forms.ListBox lbPolygons;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbSelectedPolygon;
        private System.Windows.Forms.ListBox lbVertices;
        private System.Windows.Forms.ListBox lbTiles;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBoxVolFiles;
        private System.Windows.Forms.Panel panelLevel;
        private System.Windows.Forms.Panel panelLevelScroll;
        private System.Windows.Forms.Label labelZoomLevel14;
        private System.Windows.Forms.Label labelZoomLevel12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelZoomLevel1;
        private System.Windows.Forms.TextBox tbSelectedVol;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar busyBar;
        private System.Windows.Forms.ToolStripStatusLabel busyLabel;
        private System.Windows.Forms.Panel panelImageScroll;
        private ImagePanel imagePanel;
        private PalettePanel palettePanel;
    }
}

