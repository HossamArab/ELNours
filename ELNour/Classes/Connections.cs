using ELNour.Data;
using System;
using System.Linq;

namespace ELNour.Classes
{
    class Connections
    {
        //static string[] AllData()
        //{
        //    if (File.Exists(Application.StartupPath + "\\ServerSettings.txt"))
        //    {
        //        try
        //        {
        //            using (StreamReader str = new StreamReader(Application.StartupPath + "\\ServerSettings.txt", System.Text.Encoding.UTF8))
        //            {
        //                string line = str.ReadLine();
        //                if (line != null)
        //                {
        //                    return line.Split('|');
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //    }
        //    return null;
        //}
        //static string ServerName = AllData()[0];
        //static string Username = AllData()[1];
        //static string Pass = AllData()[2];

        // قراءة البيانات من الملف
        // Property لاسترجاع السلسلة بدلالة البيانات
        public static string Constr
        {
            get
            {
                if (Server.ServerName == "")
                {
                    return @"Server=.\EagleServer;Database=ElNour;Trusted_Connection=True;";
                }
                else
                {
                    return $@"Server={Server.ServerName};Database=ElNour;MultipleActiveResultSets=true;User Id= {Server.UserName}; Password= {Server.Password}";
                }
            }
        }

    }
}
