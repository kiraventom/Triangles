using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Triangles.Model.Shapes;

namespace Triangles.Model
{
    static class IO
    {
        public static IEnumerable<Triangle> GetTrianglesFromFile(string filename)
        {
            if (!IsFileOk(filename))
            {
                return null;
            }

            var triangles = new List<Triangle>();
            using (var sr = File.OpenText(filename))
            {
                int trianglesCount = int.Parse(sr.ReadLine());
                for (int i = 0; i < trianglesCount; ++i)
                {
                    string line = sr.ReadLine();
                    var coords = line.Split(' ').Select(numStr => int.Parse(numStr)).ToArray();

                    var points = new List<PointF>();
                    for (int j = 0; j < coords.Length; j += 2)
                    {
                        points.Add(new PointF(coords[j], coords[j + 1]));
                    }
                    triangles.Add(new Triangle(points[0], points[1], points[2]));
                }
            }

            return triangles;
        }

        private static bool IsFileOk(string filename)
        {
            const int expectedValuesPerLine = 6;
            try
            {
                CheckFile(filename, expectedValuesPerLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(text: e.Message,
                                caption: "Ошибка!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private static void CheckFile(string filename, int expectedValuesPerLine)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File '{filename}' not found.");
            }
            using (var sr = File.OpenText(filename))
            {
                string firstLine = sr.ReadLine();
                if (!int.TryParse(firstLine, out int count) || count <= 0 || count > 1000)
                {
                    throw new FormatException($"Triangles count is equal to '{firstLine}', which is not a parseable integer value.");
                }

                for (int i = 0; i < count; ++i)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        throw new FormatException($"Unexpected empty line at line {i + 1}.");
                    }

                    string[] coordsAsStr = line.Split(' ');
                    if (coordsAsStr.Length != expectedValuesPerLine)
                    {
                        throw new FormatException($"Found {coordsAsStr.Length} values at line {i + 1}, expected {expectedValuesPerLine}.");
                    }

                    if (coordsAsStr.Any(coordAsStr => !int.TryParse(coordAsStr, out int _)))
                    {
                        throw new FormatException($"Found not parseable value at line {i + 1}.");
                    }
                }
            }
        }

    }
}
