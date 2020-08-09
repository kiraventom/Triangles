using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Triangles.Model;
using Triangles.Model.Shapes;

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
            bool isThereIntersection = Intersection.IsThereIntersection(triangles);
            if (isThereIntersection)
            {
                StatusL.Text = "Error!";
            }
            else
            {
                Hierarchy.DefineHierarchy(triangles);
                int colorsCounts = triangles.Max(tr => tr.Level) + 1 + 1; // + 1 for background color and + 1 because level is zero-based
                StatusL.Text = "Количество оттенков: " + colorsCounts;
            }

            this.MainTV.AddTriangles(triangles, !isThereIntersection);
        }

        private void ColorBt_Click(object sender, EventArgs e)
        {
            using (var cd = new ColorDialog() { Color = ColorBt.BackColor, AnyColor = true, FullOpen = true })
            {
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

        private void GenerateBt_Click(object sender, EventArgs e)
        {
            int amount = (int)TrianglesAmountNUD.Value;

            var triangles = Generator.GenerateNonIntersecting(amount);
            bool isThereIntersection = Intersection.IsThereIntersection(triangles);
            if (isThereIntersection)
            {
                StatusL.Text = "Error!";
            }
            else
            {
                Hierarchy.DefineHierarchy(triangles);
                int colorsCounts = triangles.Max(tr => tr.Level) + 1 + 1; // + 1 for background color and + 1 because level is zero-based
                StatusL.Text = "Количество оттенков: " + colorsCounts;
            }
            
            this.MainTV.AddTriangles(triangles, !isThereIntersection);
        }
    }
}
