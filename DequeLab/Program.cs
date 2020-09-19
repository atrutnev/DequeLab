using System;
using System.Windows.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace DequeLab
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppCenter.Start("43ec4bfc-f8af-4ad0-b598-24a5b4fddf03", typeof(Analytics), typeof(Crashes));
            Application.Run(new MainForm());
        }
        
    }
}
