namespace Triangles.Model.Shapes
{
    using System;
    using System.Drawing;

    public class Vector
    {
        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public static Vector PointToVector(PointF point, PointF start)
        {
            var x = point.X - start.X;
            var y = point.Y - start.Y;
            return new Vector(x, y);
        }

        /// <summary>
        /// Вычисление псевдоскалярного произведения векторов.
        /// </summary>
        /// <param name="vector1">Первый вектор</param>
        /// <param name="vector2">Второй вектор</param>
        /// <returns>Псевдоскалярное произведение</returns>
        public static double CrossProduct(Vector vector1, Vector vector2)
        {
            if (vector1 is null)
            {
                throw new ArgumentNullException(nameof(vector1));
            }
            if (vector2 is null)
            {
                throw new ArgumentNullException(nameof(vector2));
            }

            return (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
        }
    }
}
