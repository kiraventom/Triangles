namespace Triangles
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Triangles.Model;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
            // устанавливаем цвет по умолчанию, светло-зелёный выбран для примера
            this.ColorBt.BackColor = Color.LightGreen;
        }

        private void AddTrianglesBt_Click(object sender, EventArgs e)
        {
            string filename;
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                filename = ofd.FileName;
            }

            var triangles = File.GetTrianglesFromFile(filename);
            if (triangles is null) // некорректный файл
            {
                return;
            }

            const string IntersectionMessage = "Ошибка!";
            bool isThereIntersection = Intersection.IsThereIntersection(triangles);
            if (isThereIntersection)
            {
               this.StatusL.Text = IntersectionMessage;
            }
            else
            {
                Hierarchy.DefineHierarchy(triangles);
                int colorsCount = triangles.Max(tr => tr.Level) + 1 + 1; // + 1 за фон и + 1, так как уровень отсчитывается с нуля
                this.StatusL.Text = "Количество оттенков: " + colorsCount;
            }

            this.MainTV.AddTriangles(triangles, !isThereIntersection);
        }

        private void ColorBt_Click(object sender, EventArgs e)
        {
            using (var cd = new ColorDialog() { Color = this.ColorBt.BackColor, AnyColor = true, FullOpen = true })
            {
                if (cd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.ColorBt.BackColor = cd.Color;
            }
        }

        private void ColorBt_BackColorChanged(object sender, EventArgs e)
        {
            // Таким образом цвет фона кнопки передаётся в контрол отрисовки треугольников
            this.MainTV.BaseColor = (sender as Control).BackColor;
        }
    }
}
