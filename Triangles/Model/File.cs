namespace Triangles.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Triangles.Model.Shapes;

    public static class File
    {
        public static IEnumerable<Triangle> GetTrianglesFromFile(string filename)
        {
            if (!IsFileOk(filename))
            {
                return null;
            }

            // предполагается, что координаты будут разделены пробелами
            const char Separator = ' ';
            var triangles = new List<Triangle>();
            using (var sr = System.IO.File.OpenText(filename))
            {
                int trianglesCount = int.Parse(sr.ReadLine(), CultureInfo.InvariantCulture); // получаем количество треугольников
                for (int i = 0; i < trianglesCount; ++i)
                {
                    string line = sr.ReadLine();
                    var coords = line.Split(Separator).Select(numStr => int.Parse(numStr, CultureInfo.InvariantCulture)).ToArray();

                    var points = new List<Point>();
                    for (int j = 0; j < coords.Length; j += 2)
                    {
                        points.Add(new Point(coords[j], coords[j + 1]));
                    }

                    triangles.Add(new Triangle(points[0], points[1], points[2]));
                }
            }

            return triangles;
        }

        private static bool IsFileOk(string filename)
        {
            // Предполагается, что на каждой строке, кроме первой, будет по три пары координат
            const int ExpectedValuesPerLine = 6;
            try
            {
                CheckFile(filename, ExpectedValuesPerLine);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private static void CheckFile(string filename, int expectedValuesPerLine)
        {
            if (!System.IO.File.Exists(filename))
            {
                throw new FileNotFoundException($"Файл \"{filename}\" не найден.");
            }

            using (var sr = System.IO.File.OpenText(filename))
            {
                string firstLine = sr.ReadLine();
                if (!int.TryParse(firstLine, out int count) || count <= 0 || count > 1000)
                {
                    throw new FormatException($"Количество треугольников равно \"{firstLine}\", что не является корректным целочисленным значением.");
                }

                for (int i = 0; i < count; ++i)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        throw new FormatException($"Неожиданный конец файла на строке {i + 1}.");
                    }

                    string[] coordsAsStr = line.Split(' ');
                    if (coordsAsStr.Length != expectedValuesPerLine)
                    {
                        throw new FormatException($"{coordsAsStr.Length} значений на строке {i + 1}, ожидалось {expectedValuesPerLine}.");
                    }

                    if (coordsAsStr.Any(coordAsStr => !int.TryParse(coordAsStr, out int _)))
                    {
                        throw new FormatException($"Не целочисленное значение на строке {i + 1}.");
                    }
                }
            }
        }
    }
}