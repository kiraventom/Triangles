using Advanced.Algorithms.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Triangles.Model.Shapes;

namespace Triangles.Model
{
    public static class Intersection
    {
        public static bool IsThereIntersection(IEnumerable<LineSegment> segments)
        {
            BentleyOttmann bentleyOttmann = new BentleyOttmann();
            var lines = segments.Select(seg =>
                new Line(new Advanced.Algorithms.Geometry.Point(seg.Start.X, seg.Start.Y),
                         new Advanced.Algorithms.Geometry.Point(seg.End.X, seg.End.Y))).ToList();
            var dict = bentleyOttmann.FindIntersections(lines);
            return dict.Count > 0;
        }
    }
}
