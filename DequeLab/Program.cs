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
            AppCenter.Start("2f3ec93b-d718-4011-a06d-fd46ad3dad89", typeof(Analytics), typeof(Crashes));
            Application.Run(new MainForm());
        }
        
    }
}
