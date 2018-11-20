namespace WargameLibLevelViewer
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbPolygons = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSelectedPolygon = new System.Windows.Forms.TextBox();
            this.lbTiles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbVertices = new System.Windows.Forms.ListBox();
            this.tbSelectedTile = new System.Windows.Forms.TextBox();
            this.pnMap = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // lbPolygons
            // 
            this.lbPolygons.FormattingEnabled = true;
            this.lbPolygons.Location = new System.Drawing.Point(15, 40);
            this.lbPolygons.Name = "lbPolygons";
            this.lbPolygons.Size = new System.Drawing.Size(155, 108);
            this.lbPolygons.TabIndex = 1;
            this.lbPolygons.SelectedValueChanged += new System.EventHandler(this.lbPolygons_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Polygons";
            // 
            // tbSelectedPolygon
            // 
            this.tbSelectedPolygon.Location = new System.Drawing.Point(15, 154);
            this.tbSelectedPolygon.Multiline = true;
            this.tbSelectedPolygon.Name = "tbSelectedPolygon";
            this.tbSelectedPolygon.ReadOnly = true;
            this.tbSelectedPolygon.Size = new System.Drawing.Size(155, 108);
            this.tbSelectedPolygon.TabIndex = 4;
            // 
            // lbTiles
            // 
            this.lbTiles.FormattingEnabled = true;
            this.lbTiles.Location = new System.Drawing.Point(15, 281);
            this.lbTiles.Name = "lbTiles";
            this.lbTiles.Size = new System.Drawing.Size(155, 69);
            this.lbTiles.TabIndex = 5;
            this.lbTiles.SelectedValueChanged += new System.EventHandler(this.lbTiles_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tiles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 494);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vertices";
            // 
            // lbVertices
            // 
            this.lbVertices.FormattingEnabled = true;
            this.lbVertices.Location = new System.Drawing.Point(15, 510);
            this.lbVertices.Name = "lbVertices";
            this.lbVertices.Size = new System.Drawing.Size(155, 69);
            this.lbVertices.TabIndex = 7;
            // 
            // tbSelectedTile
            // 
            this.tbSelectedTile.Location = new System.Drawing.Point(15, 356);
            this.tbSelectedTile.Multiline = true;
            this.tbSelectedTile.Name = "tbSelectedTile";
            this.tbSelectedTile.ReadOnly = true;
            this.tbSelectedTile.Size = new System.Drawing.Size(155, 135);
            this.tbSelectedTile.TabIndex = 9;
            // 
            // pnMap
            // 
            this.pnMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMap.AutoScroll = true;
            this.pnMap.BackColor = System.Drawing.Color.Gray;
            this.pnMap.Location = new System.Drawing.Point(176, 40);
            this.pnMap.Name = "pnMap";
            this.pnMap.Size = new System.Drawing.Size(612, 539);
            this.pnMap.TabIndex = 10;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 591);
            this.Controls.Add(this.pnMap);
            this.Controls.Add(this.tbSelectedTile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbVertices);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbTiles);
            this.Controls.Add(this.tbSelectedPolygon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPolygons);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Wargame Lib Level Viewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ListBox lbPolygons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSelectedPolygon;
        private System.Windows.Forms.ListBox lbTiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbVertices;
        private System.Windows.Forms.TextBox tbSelectedTile;
        private System.Windows.Forms.Panel pnMap;
    }
}

