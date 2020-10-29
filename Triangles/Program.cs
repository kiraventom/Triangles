namespace Triangles
{
    using System;
    using System.Windows.Forms;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
#pragma warning disable CA2000
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
#pragma warning restore CA2000
    }
}
