using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Classes
{
    internal class GetPrinterData
    {
        public GetPrinterData()
        {
            getPrinterData();
        }
        public string PrinterA4Name = "";
        public string PrinterRecipteName = "";
        public int NoCopy = 0;
        public int RecipteCopy = 0;
        public bool PrintCompanyName = false;
        public bool PrintCompanyLogo = false;
        public bool PrintCompanyDescription = false;
        public bool PreviewBeforePrint = false;
        public int PrintAfterSave = -1;
        private void getPrinterData()
        {
            if (File.Exists("" + Application.StartupPath + "\\PrinterSettings.txt"))
            {
                using (StreamReader str = new StreamReader("" + Application.StartupPath + "\\PrinterSettings.txt", System.Text.Encoding.UTF8))
                {
                    string PrinterNamesData = str.ReadLine();
                    string PrinerSettingData = str.ReadLine();
                    string[] Names = PrinterNamesData.Split('|');
                    string[] strings = PrinerSettingData.Split('|');
                    PrinterA4Name = Names[0];
                    PrinterRecipteName = Names[1];
                    NoCopy = Convert.ToInt32(Names[2]);
                    RecipteCopy = Convert.ToInt32(Names[3]);
                    PrintCompanyName = Convert.ToBoolean(strings[0]);
                    PrintCompanyLogo = Convert.ToBoolean(strings[1]);
                    PrintCompanyDescription = Convert.ToBoolean(strings[2]);
                    PreviewBeforePrint = Convert.ToBoolean(strings[3]);
                    PrintAfterSave = Convert.ToInt32(strings[4]);
                }
            }
        }
    }
}
