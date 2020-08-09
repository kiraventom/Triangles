using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Triangles.Model.Shapes;

namespace Triangles
{
    public partial class TriangleViewer : UserControl
    {
        public TriangleViewer()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            BaseColor = Color.White;
            brush = new SolidBrush(BaseColor);
        }

        private bool fillTriangles;
        private IEnumerable<Triangle> drawingTriangles;
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

        private void TriangleViewer_Load(object sender, EventArgs e)
        {
            this.ParentForm.ResizeEnd += this.ParentForm_ResizeEnd;
        }

        private void ParentForm_ResizeEnd(object sender, EventArgs e)
        {
            if (drawingTriangles is null)
            {
                return;
            }

            drawingTriangles = ScaleTrianglesToSize(drawingTriangles, this.Size);
            this.Invalidate();
        }

        public void AddTriangles(IEnumerable<Triangle> newTriangles, bool fillTriangles = true)
        {
            if (!newTriangles.Any())
            {
                return;
            }
            this.fillTriangles = fillTriangles;

            drawingTriangles = newTriangles.OrderByDescending(tr => tr.Area);
            drawingTriangles = ScaleTrianglesToSize(drawingTriangles, this.Size);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) 
        { 
            base.OnPaint(e);
            if (drawingTriangles is null)
            {
                return;
            }
            var g = e.Graphics;
            g.Clear(BaseColor);
            foreach (var triangle in drawingTriangles)
            {
                var points = triangle.Points.ToArray();
                brush.Color = GetTriangleColor(triangle.Level, BaseColor);

                if (fillTriangles)
                {
                    g.FillPolygon(brush, points);
                }
                g.DrawPolygon(pen, points);
            }
        }

        private static IEnumerable<Triangle> ScaleTrianglesToSize(IEnumerable<Triangle> originalTriangles, Size size)
        {
            const int margin = 10;
            var points = originalTriangles.SelectMany(tr => tr.Points);
            var maxX = points.Max(pt => pt.X);
            var maxY = points.Max(pt => pt.Y);

            var horizontalRatio = (size.Width - margin) / maxX;
            var verticalRatio = (size.Height - margin) / maxY;
            var ratio = horizontalRatio <= verticalRatio ? horizontalRatio : verticalRatio;
            var scaledTriangles = originalTriangles.Select(tr =>
            {
                var newPoints = tr.Points.Select(pt => new PointF(pt.X * ratio, pt.Y * ratio)).ToArray();
                var newTriangle = new Triangle(newPoints[0], newPoints[1], newPoints[2]) { Parent = tr.Parent };
                return newTriangle;
            });
            return scaledTriangles;
        }

        private static Color GetTriangleColor(int level, Color baseColor)
        {
            const int multiplier = 25;
            var r = baseColor.R - multiplier * (level + 1);
            var g = baseColor.G - multiplier * (level + 1);
            var b = baseColor.B - multiplier * (level + 1);
            var color = Color.FromArgb(r > 0 ? r : 0,
                                       g > 0 ? g : 0,
                                       b > 0 ? b : 0);
            return color;
        }
    }
}
