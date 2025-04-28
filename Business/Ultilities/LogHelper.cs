using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ultilities
{
    public class LogHelper
    {
        static public void WriteLog(string fileName, string Content)
        {
            try
            {
                fileName = fileName != "" ? fileName : "debug.log";
                string strFunction = "";
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + fileName, FileMode.Append, FileAccess.Write);
                StreamWriter srLog = new StreamWriter(fs);
                string strWrite = Environment.NewLine + "====================================================" + Environment.NewLine;
                strWrite = strFunction + DateTime.Now.ToString() + "--" + DateTime.Now.ToShortTimeString();
                strWrite += ": ";
                strWrite += Content;
                srLog.WriteLine(strWrite);
                srLog.Flush();
                srLog.Close();
                fs.Close();
            }
            //}
            catch { }
        }

        static public void WriteLog(string Content)
        {
            string fileName = "debug.log";
            WriteLog(fileName, Content);

        }
    }
}
