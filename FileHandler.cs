using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mathee_Steophanus_PRG282_ST
{
    internal class FileHandler
    {
        string path = @"c:\Log.txt";
        FileStream fs;
        StreamReader sr;
        StreamWriter sw;

        public void Write(string str)
        {
            try
            {
                using(fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(str);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Read()
        {
            try
            {
                using (fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using(sr = new StreamReader(fs))
                    {
                        sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
