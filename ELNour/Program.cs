using DevExpress.Skins;
using DevExpress.UserSkins;
using ELNour.Classes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ELNour
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // تعيين الرخصة أول شيء عند بدء التطبيق
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmHome());
        }
    }
}
