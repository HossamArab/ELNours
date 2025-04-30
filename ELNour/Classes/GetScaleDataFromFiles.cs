using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBoxes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using DataBaseOperations;

namespace ELNour.Classes
{
    internal class GetScaleDataFromFiles
    {
        
        public GetScaleDataFromFiles()
        {
            GetScaleDataFromFile();
        }
        public string Comport = "";
        public string BuadRate = "";
        public int StartWeight = -1;
        public int EndWeight = -1;
        
        private void GetScaleDataFromFile()
        {
            if (File.Exists("" + System.Windows.Forms.Application.StartupPath + "\\ScaleSettings.txt"))
            {
                using (StreamReader str = new StreamReader("" + System.Windows.Forms.Application.StartupPath + "\\ScaleSettings.txt", System.Text.Encoding.UTF8))
                {
                    string AllData;
                    string[] Data;
                    AllData = str.ReadLine();
                    if (AllData.Contains('|'))
                    {
                        Data = AllData.Split('|');
                        Comport = Data[0];
                        BuadRate = Data[1];
                        StartWeight = Convert.ToInt32(Data[2]);
                        EndWeight = Convert.ToInt32(Data[3]);
                    }
                }
                    
            }
        }
        
    }
}
