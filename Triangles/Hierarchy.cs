using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Triangles.Model.Shapes;

namespace Triangles.Model
{
    public static class Hierarchy
    {
        public static void DefineHierarchy(IEnumerable<Triangle> triangles)
        {
            foreach(var triangle in triangles)
            {
                triangle.Parent = FindParentOf(triangle, triangles);
            }
        }

        private static Triangle FindParentOf(Triangle child, IEnumerable<Triangle> triangles)
        {
            // Берём все треугольники, которые больше нашего по площади
            var biggerTriangles = triangles.Where(tr => tr.Area > child.Area);
            // Из них выбираем те, внутри которого находится наш треугольник
            var parents = biggerTriangles.Where(tr => IsTriangleInsiseTriangle(tr, child));
            // Если таких не оказалось, треугольник находится на поле
            if (!parents.Any())
            {
                return null;
            }
            // Иначе берём самого меньшего по площади родителя
            return parents.OrderBy(tr => tr.Area).First();
        }

        private static bool IsTriangleInsiseTriangle(Triangle parent, Triangle child)
        {
            return child.Points.All(pt => IsPointInsideTriangle(pt, parent));
        }

        private static bool IsPointInsideTriangle(PointF point, Triangle triangle)
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
            Vector vectorToPoint = Vector.PointToVector(point, line.Start);
            // Вектор к третьей вершине треугольника, которая заведомо находится с внутренней стороны линии
            Vector vectorToApex = Vector.PointToVector(apex, line.Start);
            // Берём псевдоскалярное произведение векторов. 
            // Если знаки совпадут - точки находятся на одной стороне линии, не совпадут - на разных
            var cp1 = Vector.CrossProduct(lineVector, vectorToPoint);
            var cp2 = Vector.CrossProduct(lineVector, vectorToApex);

            return cp1 * cp2 > 0;
        }
    }
}
