using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using Triangles.Model.Shapes;

namespace Triangles.Model
{
    static class Generator
    {
        private static readonly Random Rnd = new Random();

        public static IEnumerable<Triangle> GenerateNonIntersecting(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), $"{nameof(amount)} of triangles to generate cannot be less than 0");
            }

            const int pointsAmount = 3;
            const int planeSideSize = 10000;
            int rectanglesPerSide = (int)Math.Ceiling(Math.Sqrt(amount));
            int rectangleSideSize = planeSideSize / rectanglesPerSide;
            var triangles = new List<Triangle>(amount);
            for (int x = 0; x < rectanglesPerSide; ++x)
            {
                for (int y = 0; y < rectanglesPerSide; ++y)
                {
                    Point point = new Point(x * rectangleSideSize, y * rectangleSideSize);
                    Size size = new Size(rectangleSideSize, rectangleSideSize);
                    Rectangle rect = new Rectangle(point, size);
                    var points = new List<Point>(pointsAmount);
                    for (int pt = 0; pt < pointsAmount; ++pt)
                    {
                        points.Add(new Point(Rnd.Next(rect.Left, rect.Right), Rnd.Next(rect.Top, rect.Bottom)));
                    }
                    triangles.Add(new Triangle(points[0], points[1], points[2]));
                }
            }

            return triangles.AsReadOnly();
        }
    }
}
