namespace Triangles.Model.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;

    public class LineSegment
    {
        private IList<Point> points;

        public LineSegment(Point point1, Point point2)
        {
            Points = new ReadOnlyCollection<Point>(new[] { point1, point2 });
        }

        public LineSegment(int x1, int y1, int x2, int y2)
        {
            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            Points = new ReadOnlyCollection<Point>(new[] { point1, point2 });
        }

        public IList<Point> Points 
        { 
            get => points;
            private set
            {
                points = value;
                LeftPoint = this.Point1.X <= this.Point2.X ? this.Point1 : this.Point2;
                RightPoint = this.Point1.X > this.Point2.X ? this.Point1 : this.Point2;
                BottomPoint = this.Point1.Y <= this.Point2.Y ? this.Point1 : this.Point2;
                TopPoint = this.Point1.Y > this.Point2.Y ? this.Point1 : this.Point2;
                Length = Math.Sqrt(Math.Pow(this.Point2.X - this.Point1.X, 2) + Math.Pow(this.Point2.Y - this.Point1.Y, 2));
            }
        }

        public Point Point1 => Points[0];

        public Point Point2 => Points[1];

        public Point LeftPoint { get; private set; }

        public Point RightPoint { get; private set; }

        public Point BottomPoint { get; private set; }

        public Point TopPoint { get; private set; }

        public double Length { get; private set; }

        public static implicit operator double(LineSegment segment) => segment is null ? -1 : segment.Length;

        public Vector ToVector() => new Vector(this.Point2.X - this.Point1.X, this.Point2.Y - this.Point1.Y);
    }
}
