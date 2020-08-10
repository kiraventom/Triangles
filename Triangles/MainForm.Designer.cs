namespace Triangles
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AddFromFileBt = new System.Windows.Forms.Button();
            this.StatusL = new System.Windows.Forms.Label();
            this.ColorBt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MainTV = new Triangles.TriangleViewer();
            this.SuspendLayout();
            // 
            // AddFromFileBt
            // 
            this.AddFromFileBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddFromFileBt.Location = new System.Drawing.Point(12, 472);
            this.AddFromFileBt.Name = "AddFromFileBt";
            this.AddFromFileBt.Size = new System.Drawing.Size(163, 29);
            this.AddFromFileBt.TabIndex = 1;
            this.AddFromFileBt.Text = "Открыть файл";
            this.AddFromFileBt.UseVisualStyleBackColor = true;
            this.AddFromFileBt.Click += new System.EventHandler(this.AddTrianglesBt_Click);
            // 
            // StatusL
            // 
            this.StatusL.AutoSize = true;
            this.StatusL.Location = new System.Drawing.Point(9, 9);
            this.StatusL.Name = "StatusL";
            this.StatusL.Size = new System.Drawing.Size(49, 17);
            this.StatusL.TabIndex = 2;
            this.StatusL.Text = "Ready";
            // 
            // ColorBt
            // 
            this.ColorBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColorBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColorBt.Location = new System.Drawing.Point(127, 420);
            this.ColorBt.Name = "ColorBt";
            this.ColorBt.Size = new System.Drawing.Size(48, 46);
            this.ColorBt.TabIndex = 3;
            this.ColorBt.UseVisualStyleBackColor = false;
            this.ColorBt.BackColorChanged += new System.EventHandler(this.ColorBt_BackColorChanged);
            this.ColorBt.Click += new System.EventHandler(this.ColorBt_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите цвет:";
            // 
            // MainTV
            // 
            this.MainTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTV.BackColor = System.Drawing.Color.White;
            this.MainTV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainTV.BackgroundImage")));
            this.MainTV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MainTV.BaseColor = System.Drawing.Color.White;
            this.MainTV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainTV.Location = new System.Drawing.Point(181, 12);
            this.MainTV.Name = "MainTV";
            this.MainTV.Size = new System.Drawing.Size(523, 489);
            this.MainTV.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 513);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColorBt);
            this.Controls.Add(this.StatusL);
            this.Controls.Add(this.AddFromFileBt);
            this.Controls.Add(this.MainTV);
            this.MinimumSize = new System.Drawing.Size(401, 303);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Triangle Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TriangleViewer MainTV;
        private System.Windows.Forms.Button AddFromFileBt;
        private System.Windows.Forms.Label StatusL;
        private System.Windows.Forms.Button ColorBt;
        private System.Windows.Forms.Label label1;
    }
}