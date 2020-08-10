namespace Triangles.Model.Shapes
{
    using System.Drawing;

    public static class PointExtensions
    {
        public static bool IsInsideTriangle(this Point point, Triangle triangle)
        {
            /* Чтобы убедиться, что точка находится точка внутри трегоульника,
             * необходимо, чтобы относительно всех трёх сторон треугольника
             * точка располагается с внутренней стороны.
             * Так же в вычислении используется третья вершина треугольника,
             * для которой положение относительно линии известно по умолчанию. */
            return ArePointsOnSameSideOfLine(point, triangle.C, triangle.AB)
                && ArePointsOnSameSideOfLine(point, triangle.B, triangle.CA)
                && ArePointsOnSameSideOfLine(point, triangle.A, triangle.BC);
        }

        private static bool ArePointsOnSameSideOfLine(Point point, Point vertex, LineSegment segment)
        {
            // Берём вектор линии, относительно которой мы рассматриваем точку
            Vector lineVector = segment.ToVector();

            // Вектор к точке, чьё положение относительно линии мы изучаем
            Vector vectorToPoint = Vector.PointToVector(point, segment.Point1);

            // Вектор к третьей вершине треугольника, которая заведомо находится с внутренней стороны линии
            Vector vectorToVertex = Vector.PointToVector(vertex, segment.Point1);

            // Вычисляем псевдоскалярное произведение векторов. 
            // Если знаки совпадут - точки находятся на одной стороне линии, не совпадут - на разных
            var cp1 = Vector.CrossProduct(lineVector, vectorToPoint);
            var cp2 = Vector.CrossProduct(lineVector, vectorToVertex);

            return cp1 * cp2 > 0;
        }
    }
}
