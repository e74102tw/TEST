using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESBDataVaild.Interface
{
    public interface ILogger
    {
        void WriteLog(string funcName, string Message);
    }
}
