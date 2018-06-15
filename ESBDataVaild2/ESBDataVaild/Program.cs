using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Engsound.Gateway;
using System.IO;

namespace ESBDataVaild
{
    static class Program
    {
        public static string m_RootPath = Path.GetDirectoryName(Application.ExecutablePath);
        public static GatewayClient m_Gateway;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            m_Gateway = new GatewayClient();
            if (m_Gateway.WaitConnect(5))
                Application.Run(new Form1());
            else
                MessageBox.Show("MessageGateWay無回應!");
            m_Gateway.Dispose();
        }
    }
}
