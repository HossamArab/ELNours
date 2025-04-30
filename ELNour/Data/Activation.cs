using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ELNour.Data
{
    internal class Activation
    {
        public static System.Management.ManagementObjectSearcher cmicWmiobj = new System.Management.ManagementObjectSearcher("Select * From Win32_DiskDrive where InterfaceType <> 'USB'");
        public static System.Management.ManagementObjectSearcher cmicWmiobjBios = new System.Management.ManagementObjectSearcher("Select * From Win32_Bios");
        public static string diskSN;
        public static MD5 md5;
        public static byte[] result2;
        public static StringBuilder strbuil = new StringBuilder();
        public static string f1;
        public static string f2;
        public static bool ISVirtualMachine()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                string manufacturer = obj["Manufacturer"].ToString();
                string model = obj["Model"].ToString();
                if (manufacturer.Contains("VMware") || manufacturer.Contains("VirtualBox") || manufacturer.Contains("Microsoft Corporation") || model.Contains("Virtual Machine"))
                {
                    return true; // Virtual machine detected
                }
            }
            return false; // No virtual machine detected
        }
        public static void secu()
        {
            if (ISVirtualMachine())
            {
                foreach (ManagementObject cmicWmi in cmicWmiobjBios.Get())
                {
                    //diskid = cmicWmi["Signature"].ToString();
                    diskSN = cmicWmi["serialnumber"].ToString();
                }
            }
            else
            {
                foreach (ManagementObject cmicWmi in cmicWmiobj.Get())
                {
                    //diskid = cmicWmi["Signature"].ToString();
                    diskSN = cmicWmi["serialnumber"].ToString();
                }
            }
            
        }
        public static string md5ha(string crypt)
        {
            byte[] result3;
            md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.ASCII.GetBytes(crypt));
            result3 = md5.Hash;
            for (int i = 0; i <= result3.Length - 1; i++)
            {
                strbuil.Append(result3[i].ToString("X2"));

            }


            f1 = strbuil.ToString();
            f2 = f1.Substring(0, 5) + "-" + f1.Substring(5, 5) + "-" + f1.Substring(10, 5) + "-" + f1.Substring(15, 5);
            return f2;

        }
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);

                // استخدام IV ثابت - يمكنك اختيار أي قيمة ثابتة هنا
                aesAlg.IV = new byte[16]; // مصفوفة من الأصفار كقيمة IV ثابتة

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    string Serial = Convert.ToBase64String(msEncrypt.ToArray());
                    string buildSerial = Serial.Substring(0, 5) + "-" + Serial.Substring(5, 5) + "-" + Serial.Substring(10, 5) + "-" + Serial.Substring(15, 5);
                    return Serial;
                }
            }
        }
        public static string GetKeyFromReg()
        {
            string CK_Key = "";
            try
            {
                RegistryKey CheckKey;
                CheckKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                if (CheckKey != null)
                {
                    CK_Key = CheckKey.GetValue("Key_Sales").ToString();

                }

                return CK_Key;
            }
            catch
            {
                return "";
            }
        }
        public static string LienceKey()
        {
            secu();
            string Key = "";
            Key = Activation.md5ha(Activation.Encrypt(diskSN.Trim(), "Ceaser11@H2e.Com"));
            return Key;
        }
        public static bool chkActivate()
        {
            if (GetKeyFromReg() != "")
            {
                if (GetKeyFromReg() == LienceKey())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }
        public static DateTime getEndDay()
        {
            DateTime CK_Key = new DateTime(2024, 1, 1);
            try
            {
                RegistryKey CheckKey;
                CheckKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                if (CheckKey != null)
                {
                    CK_Key = Convert.ToDateTime(CheckKey.GetValue("EndDay"));

                }
                return CK_Key;
            }
            catch
            {
                return new DateTime(2024, 1, 1);
            }
        }
        public static DateTime getStartDay()
        {
            DateTime CK_Key = new DateTime(2024, 1, 1);
            try
            {
                RegistryKey CheckKey;
                CheckKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ElNourSystem", true);
                if (CheckKey != null)
                {
                    CK_Key = Convert.ToDateTime(CheckKey.GetValue("StartDay"));

                }
                return CK_Key;
            }
            catch
            {
                return new DateTime(2024, 1, 1);
            }
        }
        public static bool chkTrial()
        {
            bool IsTrail = false;
            if (getEndDay() != new DateTime(2024, 1, 1))
            {
                if (getEndDay() > DateTime.Now && getStartDay() < DateTime.Now)
                {
                    IsTrail = true;
                }
                if (getEndDay() < DateTime.Now) { IsTrail = false; }
            }
            else { IsTrail = false; }
            return IsTrail;
        }
    }
}
