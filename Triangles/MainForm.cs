using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Triangles.Model;

namespace Triangles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ColorBt.BackColor = Color.LightGreen;
        }

        private void AddTrianglesBt_Click(object sender, EventArgs e)
        {
            string filename;
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                filename = ofd.FileName;
            }

            var triangles = IO.GetTrianglesFromFile(filename);
            Hierarchy.DefineHierarchy(triangles);
            this.MainTV.DrawTriangles(triangles);
        }

        private void ColorBt_Click(object sender, EventArgs e)
        {
            using (var cd = new ColorDialog())
            {
                cd.Color = ColorBt.BackColor;
                cd.AnyColor = true;
                cd.FullOpen = true;
                if (cd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                ColorBt.BackColor = cd.Color;
            }
        }

        private void ColorBt_BackColorChanged(object sender, EventArgs e)
        {
            MainTV.BaseColor = (sender as Control).BackColor;
        }
    }
}
