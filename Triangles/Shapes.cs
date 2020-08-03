using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Triangles.Model.Shapes
{
    public class Triangle
    {
        public Triangle(PointF a, PointF b, PointF c, Triangle parent)
        {
            A = a;
            B = b;
            C = c;
            points = new PointF[] { A, B, C };
            sides = new LineSegment[] { AB, BC, CA };
            this.Parent = parent;
        }

        public Triangle(LineSegment ab, LineSegment bc, Triangle parent)
        {
            A = ab.Start;
            B = ab.End;
            C = bc.End;
            points = new PointF[] { A, B, C };
            sides = new LineSegment[] { AB, BC, CA };
            this.Parent = parent;
        }

        private readonly PointF[] points;
        public IEnumerable<PointF> Points => new ReadOnlyCollection<PointF>(points);
        private readonly LineSegment[] sides;
        public IEnumerable<LineSegment> Sides => new ReadOnlyCollection<LineSegment>(sides);

        public PointF A { get; }
        public PointF B { get; }
        public PointF C { get; }
        public LineSegment AB => new LineSegment(A, B);
        public LineSegment BC => new LineSegment(B, C);
        public LineSegment CA => new LineSegment(C, A);

        public Triangle Parent { get; set; }
        public int Level => CalculateLevel(this);
        
        private static int CalculateLevel(Triangle triangle)
        {
            int level = 0;
            Triangle parent = triangle.Parent;
            while (parent != null)
            {
                parent = parent.Parent;
                ++level;
            }
            return level;
        }

        public double Area
        {
            get
            {
                // Формула Герона
                // S = SQRT(p * ( p − ab ) * ( p − bc ) * ( p − ca)), где p - полупериметр
                double p = (this.AB + this.BC + this.CA) / 2;
                double S = Math.Sqrt(p * (p - this.AB) * (p - this.BC) * (p - this.CA));
                return S;
            }
        }
    }

    public class LineSegment
    {
        public LineSegment(PointF start, PointF end)
        {
            this.Start = start;
            this.End = end;
        }

        public LineSegment(float x1, float y1, float x2, float y2)
        {
            this.Start = new PointF(x1, y1);
            this.End = new PointF(x2, y2);
        }

        public PointF Start { get; }
        public PointF End { get; }
        public double Length => Math.Sqrt(Math.Pow((End.X - Start.X), 2) + Math.Pow((End.Y - Start.Y), 2));

        public static implicit operator double(LineSegment seg) => seg.Length;

        public Vector ToVector() => new Vector(End.X - Start.X, End.Y - Start.Y);
    }

    public class Vector
    {
        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector PointToVector(PointF point, PointF start)
        {
            var x = point.X - start.X;
            var y = point.Y - start.Y;
            return new Vector(x, y);
        }

        public float X { get; }
        public float Y { get; }

        public static float CrossProduct(Vector vector1, Vector vector2)
        {
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }
    }
}
