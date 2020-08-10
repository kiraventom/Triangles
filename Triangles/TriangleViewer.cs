namespace Triangles
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Triangles.Model.Shapes;

    public partial class TriangleViewer : UserControl
    {
        public TriangleViewer()
        {
            this.InitializeComponent();
            this.Pen = new Pen(Color.Black);
            this.BaseColor = Color.White;
            this.Brush = new SolidBrush(this.BaseColor);
        }

        public Color BaseColor
        {
            get => this.baseColor;
            set
            {
                this.baseColor = value;
                this.Invalidate();
            }
        }

        private Color baseColor;

        private Pen Pen { get; }

        private SolidBrush Brush { get; }

        private bool ShouldFillTriangles { get; set; }

        private IEnumerable<Triangle> OriginalTriangles { get; set; }

        private IEnumerable<Triangle> DrawingTriangles { get; set; }

        public void AddTriangles(IEnumerable<Triangle> triangles, bool fillTriangles = true)
        {
            if (triangles is null || !triangles.Any())
            {
                return;
            }

            this.ShouldFillTriangles = fillTriangles;

            // сортируем по возрастанию площади, чтобы бОльшие по размеру треугольники не перекрывали меньшие
            OriginalTriangles = triangles.OrderByDescending(tr => tr.Area);

            RedrawTriangles();
        }

        protected override void OnPaint(PaintEventArgs e) 
        { 
            base.OnPaint(e);
            if (this.OriginalTriangles is null)
            {
                return;
            }

            var g = e.Graphics;
            g.Clear(this.BaseColor);
            foreach (var triangle in this.DrawingTriangles)
            {
                var points = triangle.Points.ToArray();
                this.Brush.Color = GetTriangleColor(triangle.Level, this.BaseColor);

                // сначала треугольник закрашивается, а затем обрисовывается контуром, так как иначе контур будет перекрыт
                if (this.ShouldFillTriangles)
                {
                    g.FillPolygon(this.Brush, points);
                }

                g.DrawPolygon(this.Pen, points);
            }
        }

        /// <summary>
        /// Пропорционально изменяет размеры всех треугольников в перечислении так, чтобы заполнить прямугольник заданного размера.
        /// </summary>
        /// <param name="originalTriangles">Треугольники</param>
        /// <param name="size">Размер прямоугольника, который необходимо заполнить</param>
        /// <returns>Треугольники с изменнённым размером</returns>
        private static IEnumerable<Triangle> ScaleTrianglesToSize(IEnumerable<Triangle> originalTriangles, Size size)
        {
            const int Margin = 10; // отступ, необходим для корректного отображения треугольников в TriangleViewer
            var points = originalTriangles.SelectMany(tr => tr.Points);
            var maxX = points.Max(pt => pt.X);
            var maxY = points.Max(pt => pt.Y);

            // коэффициент пропорции ширины прямоугольника и точки с самым большим отклонением по X
            var horizontalRatio = (size.Width - Margin) / (double)maxX;

            // коэффициент пропорции высоты прямоугольника и точки с самым большим отклонением по Y
            var verticalRatio = (size.Height - Margin) / (double)maxY;

            // выбирается меньший коэффициент, чтобы выбрать точку с бОльшим отклонением
            var ratio = horizontalRatio <= verticalRatio ? horizontalRatio : verticalRatio; 
            var scaledTriangles = originalTriangles.Select(tr =>
            {
                // точка каждого треугольника домножается на коэффициент
                var newPoints = tr.Points.Select(pt => new Point((int)(pt.X * ratio), (int)(pt.Y * ratio))).ToArray();
                var newTriangle = new Triangle(newPoints[0], newPoints[1], newPoints[2]) { Parent = tr.Parent };
                return newTriangle;
            });
            return scaledTriangles;
        }

        private enum Origin { BottomLeft, TopLeft, BottomRight, TopRight };

        private static IEnumerable<Triangle> SetOrigin(IEnumerable<Triangle> originalTriangles, Origin origin, Size planeSize)
        {
            IEnumerable<Triangle> rotatedTriangles = originalTriangles; ;
            switch (origin)
            {
                case Origin.BottomLeft:
                    rotatedTriangles = originalTriangles.Select(tr =>
                    {
                        var newPoints = tr.Points.Select(point => new Point(point.X, planeSize.Height - point.Y)).ToArray();
                        var newTriangle = new Triangle(newPoints[0], newPoints[1], newPoints[2]) { Parent = tr.Parent };
                        return newTriangle;
                    });
                    break;
                case Origin.TopLeft:

                    break;
                case Origin.BottomRight:
                    rotatedTriangles = originalTriangles.Select(tr =>
                    {
                        var newPoints = tr.Points.Select(point => new Point(planeSize.Width - point.X, planeSize.Height - point.Y)).ToArray();
                        var newTriangle = new Triangle(newPoints[0], newPoints[1], newPoints[2]) { Parent = tr.Parent };
                        return newTriangle;
                    });
                    break;
                case Origin.TopRight:
                    rotatedTriangles = originalTriangles.Select(tr =>
                    {
                        var newPoints = tr.Points.Select(point => new Point(planeSize.Width - point.X, point.Y)).ToArray();
                        var newTriangle = new Triangle(newPoints[0], newPoints[1], newPoints[2]) { Parent = tr.Parent };
                        return newTriangle;
                    });
                    break;
            }

            return rotatedTriangles;
        }

        private static Color GetTriangleColor(int level, Color baseColor)
        {
            // коэффициент изменения тона цвета треугольника
            // чем меньше коэффициент, тем меньше будет разница между цветами треугольников разных уровней
            const int Multiplier = 25; 
            var r = baseColor.R - (Multiplier * (level + 1));
            var g = baseColor.G - (Multiplier * (level + 1));
            var b = baseColor.B - (Multiplier * (level + 1));
            var color = Color.FromArgb(r > 0 ? r : 0, g > 0 ? g : 0, b > 0 ? b : 0);
            return color;
        }

        private void RedrawTriangles()
        {
            // увеличиваем треугольники соответственно размеру площади для отрисовки
            DrawingTriangles = ScaleTrianglesToSize(OriginalTriangles, this.Size);

            // устанавливаем начало координат в нижний левый угол
            DrawingTriangles = SetOrigin(DrawingTriangles, Origin.BottomLeft, this.Size);
            this.Invalidate();
        }

        private void TriangleViewer_Load(object sender, EventArgs e)
        {
            // чтобы не перерисовывать треугольники каждый момент изменения размера,
            // подписываемся на событие окончания процесса изменения размера
            this.ParentForm.ResizeEnd += this.ParentForm_ResizeEnd;
        }

        private void ParentForm_ResizeEnd(object sender, EventArgs e)
        {
            if (this.OriginalTriangles is null)
            {
                return;
            }

            RedrawTriangles();
        }
    }
}
