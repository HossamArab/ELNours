using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELNour.Classes
{
    internal static class Images
    {
        public static void ChoosePicture(this PictureBox Pbox)
        {
            try
            {
                OpenFileDialog a = new OpenFileDialog();
                a.AddExtension = true;
                a.CheckPathExists = true;
                a.CheckFileExists = true;
                a.Title = "Choose Image";
                a.Filter = "Choose Image (*.PNG; *.JPG; *.JPEG)| *.PNG; *.JPG; *.JPEG | All Files (*.*)|*.*";
                if (a.ShowDialog() == DialogResult.OK)
                {
                    Pbox.Image = Image.FromFile(a.FileName);
                }
            }
            catch { }

        }
        public static byte[] PicturetoArray(this byte[] bytes, PictureBox Pbox)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmp = new Bitmap(Pbox.Image);
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] data = ms.GetBuffer();
            return data;
        }
        public static Image ArraytoPicture(this Image image, Byte[] array)
        {
            Byte[] mybyte = new byte[0];
            mybyte = (Byte[])array;
            MemoryStream ms = new MemoryStream(mybyte);
            image = Image.FromStream(ms);
            return image;
        }
    }
}
