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
            return child.Points.All(pt => pt.IsInsideTriangle(parent));
        }
    }
}
