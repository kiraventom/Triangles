using Advanced.Algorithms.Geometry;
using System.Collections.Generic;
using System.Linq;
using Triangles.Model.Shapes;

namespace Triangles.Model
{
    public static class Intersection
    {
        public static bool IsThereIntersection(IEnumerable<LineSegment> segments)
        {
            // currently not working, so
            return false;

            //BentleyOttmann bentleyOttmann = new BentleyOttmann();
            //var lines = segments.Select(seg =>
            //    new Line(new Point(seg.Start.X, seg.Start.Y),
            //             new Point(seg.End.X, seg.End.Y))).ToList();
            //var dict = bentleyOttmann.FindIntersections(lines);
            //return dict.Count > 0;
        }
    }
}
