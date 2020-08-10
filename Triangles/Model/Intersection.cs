namespace Triangles.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using Triangles.Model.Shapes;
    using Side = System.Tuple<Shapes.LineSegment, Shapes.Triangle>;
    using Vertex = System.Tuple<System.Drawing.PointF, System.Tuple<Shapes.LineSegment, Shapes.Triangle>>;

    public static class Intersection
    {
        /// <summary>
        /// Псевдо Shamos-Hoey алгоритм для определения пересечения треугольников. Сложность: O(n^2)
        /// </summary>
        /// <param name="triangles">Треугольники</param>
        /// <returns> <see href="true"/>, если было обнаружено пересечение, иначе <see href="false"/>.</returns>
        public static bool IsThereIntersection(IEnumerable<Triangle> triangles)
        {
            // собираем все стороны треугольников в пары вида сторона=(отрезок:треугольник)
            var sides = triangles.SelectMany(tr => tr.Sides.Select(segment => new Side(segment, tr)));

            // собираем все вершины треугольников в пары вида вершина=(точка:сторона)
            var vertices = sides.SelectMany(side => side.Item1.Points.Select(point => new Vertex(point, side)))
                                .OrderBy(vertex => vertex.Item1.X);

            var currentSides = new List<Side>();

            // идём по Х слева направо
            foreach (var vertex in vertices)
            {
                // берём сторону с текущей точкой;
                Side currentSide = vertex.Item2;

                // если точка начальная
                if (vertex.Item1 == currentSide.Item1.LeftPoint) 
                {
                    // добавляем новую сторону
                    currentSides.Add(currentSide);
                }
                else // если конечная
                {
                    // проверяем сторону на пересечение со всеми текущими сторонами, кроме относящихся к этому треугольнику
                    bool isThereIntersection = currentSides.Any(side => side.Item2 != currentSide.Item2
                                                                        && DoSegmentsIntersect(side.Item1, currentSide.Item1));
                    // если нашли пересечение
                    if (isThereIntersection)
                    {
                        return true;
                    }

                    // если пересечения нет, удаляем отрезок и идём дальше
                    currentSides.Remove(currentSide);
                }
            }

            return false;
        }

        private static bool DoSegmentsIntersect(LineSegment segment1, LineSegment segment2)
        {
            // Получаем уравнение линии вида "Ax + By = C"
            double A1 = segment1.Point2.Y - segment1.Point1.Y; // A = y2 - y1
            double B1 = segment1.Point1.X - segment1.Point2.X; // B = x1 - x2
            double C1 = (A1 * segment1.Point1.X) + (B1 * segment1.Point1.Y); // C = Ax1 + By1
            double A2 = segment2.Point2.Y - segment2.Point1.Y;
            double B2 = segment2.Point1.X - segment2.Point2.X;
            double C2 = (A2 * segment2.Point1.X) + (B2 * segment2.Point1.Y);

            // все части уравнения получены, находим точку пересечения
            double determinator = (A1 * B2) - (A2 * B1);
            if (determinator == 0)
            {
                // Линии параллельны
                return false;
            }

            double x = ((B2 * C1) - (B1 * C2)) / determinator;
            double y = ((A1 * C2) - (A2 * C1)) / determinator;

            // Определяем, принадлежит ли точка к обоим отрезкам
            return x >= segment1.LeftPoint.X
                          && x <= segment1.RightPoint.X
                          && x >= segment2.LeftPoint.X
                          && x <= segment2.RightPoint.X
                          && y >= segment1.BottomPoint.Y
                          && y <= segment1.TopPoint.Y
                          && y >= segment2.BottomPoint.Y
                          && y <= segment2.TopPoint.Y;
        }
    }
}
