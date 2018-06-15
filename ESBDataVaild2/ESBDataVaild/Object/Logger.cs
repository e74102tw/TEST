using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESBDataVaild.Interface;

namespace ESBDataVaild.Object
{
    public class Logger:ILogger
    {
        protected string m_LogPath = "";
        protected string m_LogName = "";

        public Logger(string LogName)
        {
            Logger_base(LogName,"");
        }

        private void Logger_base(string LogName,string LogPath)
        {
            m_LogPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),"log\\");
            if (LogPath.Trim() != "")
                m_LogPath = LogPath;
            m_LogName = LogName;
            if (!Directory.Exists(m_LogPath))
                Directory.CreateDirectory(m_LogPath);
        }

        public void WriteLog(string funcName, string Message)
        {
            StringBuilder l_Builder = new StringBuilder();

            l_Builder.AppendFormat("{0} FunctionName : {1}\r\n{2}\r\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), funcName, Message);
            System.IO.File.AppendAllText(Path.Combine(m_LogPath, m_LogName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), l_Builder.ToString(), Encoding.GetEncoding(950));
        }
    }
}
