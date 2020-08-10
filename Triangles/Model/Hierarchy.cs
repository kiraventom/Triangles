namespace Triangles.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using Triangles.Model.Shapes;

    public static class Hierarchy
    {
        /// <summary>
        /// Устанавливает каждому из треугольников соответстующее значение поля <see cref="Triangle.Parent"/>.
        /// </summary>
        /// <param name="triangles">Треугольники</param>
        public static void DefineHierarchy(IEnumerable<Triangle> triangles)
        {
            foreach (var triangle in triangles)
            {
                triangle.Parent = FindParentOf(triangle, triangles);
            }
        }

        private static Triangle FindParentOf(Triangle child, IEnumerable<Triangle> triangles)
        {
            // Берём все треугольники, которые больше нашего по площади
            var biggerTriangles = triangles.Where(tr => tr.Area > child.Area);

            // Из них выбираем те, внутри которого находится наш треугольник
            var parents = biggerTriangles.Where(tr => IsTriangleInsideTriangle(tr, child));

            // Если таких не оказалось, треугольник находится на поле
            if (!parents.Any())
            {
                return null;
            }

            // Иначе берём самого меньшего по площади родителя
            return parents.OrderBy(tr => tr.Area).First();
        }


        private static bool IsTriangleInsideTriangle(Triangle parent, Triangle child)
        {
            // если все три точки треугольника ABC находятся внутри треугольника DEF, то ABC находится внутри DEF
            return child.Points.All(point => point.IsInsideTriangle(parent));
        }
    }
}
