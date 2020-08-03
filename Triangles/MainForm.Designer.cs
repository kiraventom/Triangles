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
            this.AddTrianglesBt = new System.Windows.Forms.Button();
            this.MainTV = new Triangles.TriangleViewer();
            this.StatusL = new System.Windows.Forms.Label();
            this.ColorBt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddTrianglesBt
            // 
            this.AddTrianglesBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddTrianglesBt.Location = new System.Drawing.Point(12, 410);
            this.AddTrianglesBt.Name = "AddTrianglesBt";
            this.AddTrianglesBt.Size = new System.Drawing.Size(120, 59);
            this.AddTrianglesBt.TabIndex = 1;
            this.AddTrianglesBt.Text = "Добавить треугольники";
            this.AddTrianglesBt.UseVisualStyleBackColor = true;
            this.AddTrianglesBt.Click += new System.EventHandler(this.AddTrianglesBt_Click);
            // 
            // MainTV
            // 
            this.MainTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTV.BackColor = System.Drawing.Color.White;
            this.MainTV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainTV.Location = new System.Drawing.Point(138, 12);
            this.MainTV.Name = "MainTV";
            this.MainTV.Size = new System.Drawing.Size(710, 457);
            this.MainTV.TabIndex = 0;
            // 
            // StatusL
            // 
            this.StatusL.AutoSize = true;
            this.StatusL.Location = new System.Drawing.Point(41, 12);
            this.StatusL.Name = "StatusL";
            this.StatusL.Size = new System.Drawing.Size(28, 17);
            this.StatusL.TabIndex = 2;
            this.StatusL.Text = "OK";
            // 
            // ColorBt
            // 
            this.ColorBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColorBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColorBt.Location = new System.Drawing.Point(44, 315);
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
            this.label1.Location = new System.Drawing.Point(9, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите цвет:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 481);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColorBt);
            this.Controls.Add(this.StatusL);
            this.Controls.Add(this.AddTrianglesBt);
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
        private System.Windows.Forms.Button AddTrianglesBt;
        private System.Windows.Forms.Label StatusL;
        private System.Windows.Forms.Button ColorBt;
        private System.Windows.Forms.Label label1;
    }
}

