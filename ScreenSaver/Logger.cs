using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSaver
{
    internal static class Logger
    {
        public static void log(string message)
        {
            string path = "E:\\PhotoScreenSaver.log";
            string preparedMessage = $@"{DateTime.Now} - {message}";

            //using (StreamWriter sw = File.AppendText(path))
            //{
            //    sw.WriteLine(preparedMessage);
            //}
        }
    }
}
