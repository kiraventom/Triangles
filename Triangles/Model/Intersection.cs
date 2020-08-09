using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Triangles.Model.Shapes;
using Side = System.Tuple<Triangles.Model.Shapes.LineSegment, Triangles.Model.Shapes.Triangle>;
using Vertex = System.Tuple<System.Drawing.PointF, System.Tuple<Triangles.Model.Shapes.LineSegment, Triangles.Model.Shapes.Triangle>>;

namespace Triangles.Model
{
    public static class Intersection
    {
        // Псевдо Shamos-Hoey алгоритм для треугольников
        public static bool IsThereIntersection(IEnumerable<Triangle> triangles)
        {
            // собираем все стороны треугольников в пары вида (отрезок:треугольник)
            var sides = triangles.SelectMany(tr => tr.Sides.Select(segment => new Side(segment, tr)));
            // собираем все вершины треугольников в пары вида (точка:сторона)
            var vertices = sides.SelectMany(side => side.Item1.Points.Select(point => new Vertex(point, side)))
                                .OrderBy(vertex => vertex.Item1.X);

            var currentSides = new List<Side>();
            // идём по Х слева направо
            foreach (var vertex in vertices)
            {
                // берём сторону с текущей точкой;
                Side currentSide = vertex.Item2;
                
                if (vertex.Item1 == currentSide.Item1.LeftPoint) // если точка начальная
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
            // Generating line equations of type "Ax + By = C"
            float A1 = segment1.Point2.Y - segment1.Point1.Y; // A = y2 - y1
            float B1 = segment1.Point1.X - segment1.Point2.X; // B = x1 - x2
            float C1 = A1 * segment1.Point1.X + B1 * segment1.Point1.Y; // C = Ax1 + By1
            float A2 = segment2.Point2.Y - segment2.Point1.Y;
            float B2 = segment2.Point1.X - segment2.Point2.X;
            float C2 = A2 * segment2.Point1.X + B2 * segment2.Point1.Y;

            // now, when we have all of the parts of the equations, 
            // let's find the point of intersection
            float det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                // Lines are parallel
                return false;
            }
            float X = (B2 * C1 - B1 * C2) / det;
            float Y = (A1 * C2 - A2 * C1) / det;

            // Finally, let's found out if point belong to both of our lines
            float line1MinX = segment1.Point1.X < segment1.Point2.X ? segment1.Point1.X : segment1.Point2.X;
            float line1MaxX = segment1.Point1.X > segment1.Point2.X ? segment1.Point1.X : segment1.Point2.X;
            float line2MinX = segment2.Point1.X < segment2.Point2.X ? segment2.Point1.X : segment2.Point2.X;
            float line2MaxX = segment2.Point1.X > segment2.Point2.X ? segment2.Point1.X : segment2.Point2.X;
            float line1MinY = segment1.Point1.Y < segment1.Point2.Y ? segment1.Point1.Y : segment1.Point2.Y;
            float line1MaxY = segment1.Point1.Y > segment1.Point2.Y ? segment1.Point1.Y : segment1.Point2.Y;
            float line2MinY = segment2.Point1.Y < segment2.Point2.Y ? segment2.Point1.Y : segment2.Point2.Y;
            float line2MaxY = segment2.Point1.Y > segment2.Point2.Y ? segment2.Point1.Y : segment2.Point2.Y;

            bool result =
                X >= line1MinX && X <= line1MaxX && X >= line2MinX && X <= line2MaxX &&
                Y >= line1MinY && Y <= line1MaxY && Y >= line2MinY && Y <= line2MaxY;

            return result;
        }
    }
}
