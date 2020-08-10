namespace Triangles.Model.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;

    public class Triangle
    {
        public Triangle(Point a, Point b, Point c)
        {
            this.Points = Array.AsReadOnly(new[] { a, b, c });
        }

        public Triangle(LineSegment ab, LineSegment bc)
        {
            if (ab is null)
            {
                throw new ArgumentNullException(nameof(ab));
            }

            if (bc is null)
            {
                throw new ArgumentNullException(nameof(ab));
            }

            this.Points = Array.AsReadOnly(new[] { ab.Point1, ab.Point2, bc.Point2 });
        }

        public IList<Point> Points
        {
            get => points;
            private set
            {
                points = value;
                this.AB = new LineSegment(this.A, this.B);
                this.BC = new LineSegment(this.B, this.C);
                this.CA = new LineSegment(this.C, this.A);
                this.Sides = Array.AsReadOnly(new[] { this.AB, this.BC, this.CA });
                this.Area = this.CalculateArea();
            }
        }

        public IEnumerable<LineSegment> Sides { get; private set; }

        public Point A => this.Points[0];

        public Point B => this.Points[1];

        public Point C => this.Points[2];

        public LineSegment AB { get; private set; }

        public LineSegment BC { get; private set; }

        public LineSegment CA { get; private set; }

        public Triangle Parent { get; set; }

        public int Level => CalculateLevel();

        public double Area { get; private set; }

        private IList<Point> points;

        private double CalculateArea()
        {
            // Формула Герона
            // S = SQRT(p * ( p − ab ) * ( p − bc ) * ( p − ca)), где p - полупериметр
            double p = (this.AB + this.BC + this.CA) / 2;
            return Math.Sqrt(p * (p - this.AB) * (p - this.BC) * (p - this.CA));
        }

        private int CalculateLevel()
        {
            int level = 0;
            Triangle parent = this.Parent;
            while (parent != null)
            {
                parent = parent.Parent;
                ++level;
            }

            return level;
        }
    }
}
