using ELNour.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Classes
{
    internal class GetServerData
    {
        public void getServerData()
        {
            if (File.Exists("" + Application.StartupPath + "\\ServerSettings.txt"))
            {
                using (StreamReader str = new StreamReader("" + Application.StartupPath + "\\ServerSettings.txt", System.Text.Encoding.UTF8))
                {
                    string ServerData = str.ReadLine();
                    if (ServerData.Contains('|'))
                    {
                        string[] serverSetting = ServerData.Split('|');
                        Server.ServerName = serverSetting[0];
                        Server.UserName = serverSetting[1];
                        Server.Password = serverSetting[2];
                    }
                }
                    
            }
        }
    }
}
