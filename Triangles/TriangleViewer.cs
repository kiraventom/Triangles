using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Triangles.Model.Shapes;

namespace Triangles
{
    public partial class TriangleViewer : UserControl
    {
        public TriangleViewer()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
            BaseColor = Color.White;
        }

        private IEnumerable<Triangle> OriginalTriangles;
        private readonly Pen pen;
        private readonly SolidBrush brush;
        private Color baseColor;
        public Color BaseColor
        {
            get => baseColor;
            set
            {
                baseColor = value;
                this.Invalidate();
            }
        }

        public void DrawTriangles(IEnumerable<Triangle> actualTriangles)
        {
            OriginalTriangles = actualTriangles;
            this.Invalidate();
        }

        private static IEnumerable<Triangle> ScaleTriangles(IEnumerable<Triangle> originalTriangles, Size size)
        {
            var points = originalTriangles.SelectMany(tr => tr.Points);
            var maxX = points.Max(pt => pt.X);
            var maxY = points.Max(pt => pt.Y);

            var horizontalRatio = (size.Width - 10) / maxX;
            var verticalRatio = (size.Height - 10) / maxY;
            var ratio = horizontalRatio <= verticalRatio ? horizontalRatio : verticalRatio;
            var scaledTriangles = originalTriangles.Select(tr =>
            {
                var newPoints = tr.Points.Select(pt => new PointF(pt.X * ratio, pt.Y * ratio)).ToArray();
                var newTriangle = new Triangle(newPoints[0], newPoints[1], newPoints[2], tr.Parent);
                return newTriangle;
            });
            return scaledTriangles;
        }

        private Color GetTriangleColor(int level)
        {
            const int multiplier = 25;
            var r = BaseColor.R - multiplier * (level + 1);
            var g = BaseColor.G - multiplier * (level + 1);
            var b = BaseColor.B - multiplier * (level + 1);
            var color = Color.FromArgb(r > 0 ? r : 0,
                                       g > 0 ? g : 0,
                                       b > 0 ? b : 0);
            return color;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (OriginalTriangles is null)
            {
                return;
            }
            var drawingTriangles = ScaleTriangles(OriginalTriangles, this.Size);

            using (var g = pe.Graphics)
            {
                g.Clear(BaseColor);
                foreach (var triangle in drawingTriangles)
                {
                    var points = triangle.Points.ToArray();
                    brush.Color = GetTriangleColor(triangle.Level);

                    g.FillPolygon(brush, points);
                    g.DrawPolygon(pen, points);
                }
            }
        }

        private void TriangleViewer_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
