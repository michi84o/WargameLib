namespace WargameLibTestWinforms
{
    partial class ExtractDirDialog
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
            this.buttonUnpack = new System.Windows.Forms.Button();
            this.buttonFile = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonUnpack
            // 
            this.buttonUnpack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUnpack.Location = new System.Drawing.Point(284, 53);
            this.buttonUnpack.Name = "buttonUnpack";
            this.buttonUnpack.Size = new System.Drawing.Size(134, 35);
            this.buttonUnpack.TabIndex = 7;
            this.buttonUnpack.Text = "Extract";
            this.buttonUnpack.UseVisualStyleBackColor = true;
            this.buttonUnpack.Click += new System.EventHandler(this.buttonUnpack_Click);
            // 
            // buttonFile
            // 
            this.buttonFile.Location = new System.Drawing.Point(386, 23);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(32, 23);
            this.buttonFile.TabIndex = 6;
            this.buttonFile.Text = "...";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(12, 25);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(368, 20);
            this.textBoxFile.TabIndex = 5;
            this.textBoxFile.TextChanged += new System.EventHandler(this.textBoxFile_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the file to extract:";
            // 
            // ExtractDirDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 96);
            this.Controls.Add(this.buttonUnpack);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtractDirDialog";
            this.ShowIcon = false;
            this.Text = "ExtractDirDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUnpack;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Label label1;
    }
}