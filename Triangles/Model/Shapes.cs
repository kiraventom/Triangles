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
        public Triangle(PointF a, PointF b, PointF c)
        {
            A = a;
            B = b;
            C = c;
            Points = Array.AsReadOnly(new[] { A, B, C });
            Sides = Array.AsReadOnly(new[] { AB, BC, CA });
        }

        public Triangle(LineSegment ab, LineSegment bc)
        {
            A = ab.Point1;
            B = ab.Point2;
            C = bc.Point2;
            Points = Array.AsReadOnly(new [] { A, B, C });
            Sides = Array.AsReadOnly(new [] { AB, BC, CA });
        }

        public IEnumerable<PointF> Points { get; }
        public IEnumerable<LineSegment> Sides { get; }

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
        public LineSegment(PointF point1, PointF point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
        }

        public LineSegment(float x1, float y1, float x2, float y2)
        {
            this.Point1 = new PointF(x1, y1);
            this.Point2 = new PointF(x2, y2);
        }

        public PointF LeftPoint => Point1.X <= Point2.X ? Point1 : Point2;
        public PointF RightPoint => Point1.X > Point2.X ? Point1 : Point2;

        public PointF Point1 { get; }
        public PointF Point2 { get; }

        public double Length => Math.Sqrt(Math.Pow((Point2.X - Point1.X), 2) + Math.Pow((Point2.Y - Point1.Y), 2));

        public IList<PointF> Points => new ReadOnlyCollection<PointF>(new[] { LeftPoint, RightPoint });

        public static implicit operator double(LineSegment seg) => seg.Length;

        public Vector ToVector() => new Vector(Point2.X - Point1.X, Point2.Y - Point1.Y);
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

    public static class PointExtensions
    {
        public static bool ArePointsCollinear(PointF a, PointF b, PointF c)
        {
            double abSlope = (b.Y - a.Y) / (b.X - a.X);
            double bcSlope = (c.Y - b.Y) / (c.X - b.X);
            double caSlope = (a.Y - c.Y) / (a.X - c.X);
            return abSlope == bcSlope && bcSlope == caSlope;
        }

        public static bool IsInsideTriangle(this PointF point, Triangle triangle)
        {
            // Чтобы убедиться, что точка находится точка внутри трегоульника,
            // необходимо, чтобы относительно всех трёх сторон треугольника
            // точка располагается с внутренней стороны.
            // Так же в вычислении используется третья вершина треугольника,
            // для которой положение относительно линии известно по умолчанию.
            return
                ArePointsOnSameSideOfLine(point, triangle.C, triangle.AB) &&
                ArePointsOnSameSideOfLine(point, triangle.B, triangle.CA) &&
                ArePointsOnSameSideOfLine(point, triangle.A, triangle.BC);
        }

        private static bool ArePointsOnSameSideOfLine(PointF point, PointF apex, LineSegment line)
        {
            // Берём вектор линии, относительно которой мы рассматриваем точку
            Vector lineVector = line.ToVector();
            // Вектор к точке, чьё положение относительно линии мы изучаем
            Vector vectorToPoint = Vector.PointToVector(point, line.Point1);
            // Вектор к третьей вершине треугольника, которая заведомо находится с внутренней стороны линии
            Vector vectorToApex = Vector.PointToVector(apex, line.Point1);
            // Берём псевдоскалярное произведение векторов. 
            // Если знаки совпадут - точки находятся на одной стороне линии, не совпадут - на разных
            var cp1 = Vector.CrossProduct(lineVector, vectorToPoint);
            var cp2 = Vector.CrossProduct(lineVector, vectorToApex);

            return cp1 * cp2 > 0;
        }

        //public static ArePointsCollinear(params PointF[] points)
        //{

        //}
    }
}
