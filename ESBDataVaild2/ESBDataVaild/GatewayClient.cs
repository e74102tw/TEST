using System;
using System.IO;
using System.Threading;
using Engsound.Gateway;
using Engsound.Configuration;
using System.Collections.Generic;

namespace ESBDataVaild
{
    public class GatewayClient:IDisposable
    {
        ManualResetEvent m_GatewaySync = new ManualResetEvent(false);
        GatewayMessageArgs m_GatewayResult = null;
        GatewayConnector m_Gateway;
        uint m_GatewaySendId = 0;
        int m_GatewayTimeout = 60000;
        private bool m_GatewayConnect = false;
        public bool GatewayConnect
        {
            get { return m_GatewayConnect; }
        }
        private string _DBClassName;
        public string DBClassName
        {
            get { return _DBClassName; }
        }

        private string _ECMSClassName;
        public string ECMSClassName
        {
            get { return _ECMSClassName; }
        }

        private string _SMSServerClassName;
        public string SMSServerClassName
        {
            get { return _SMSServerClassName; }
        }

        private string _ESBClassName;
        public string ESBClassName
        {
            get { return _ESBClassName; }
        }

        private string _LogName;
        public string LogName
        {
            get { return _LogName; }
        }

        private string _SettingConfigurePath;
        public string SettingConfigurePath
        {
            get { return _SettingConfigurePath; }
        }

        public GatewayClient()
        {
            IniProfile l_Ini = new IniProfile(Path.GetFullPath("configure.ini"));
            _LogName = l_Ini.GetString("System", "LogName", "ESBDataVaild");

            _SettingConfigurePath = l_Ini.GetString("System", "SettingConfigurePath", "");
            IniProfile lSettingConfigure_Ini = new IniProfile(Path.GetFullPath(_SettingConfigurePath));
            _DBClassName = lSettingConfigure_Ini.GetString("System", "DBClassName", "");
            _ECMSClassName = lSettingConfigure_Ini.GetString("System", "ECMSClassName", "");
            _SMSServerClassName = lSettingConfigure_Ini.GetString("System", "SMSServerClassName", "");
            _ESBClassName = lSettingConfigure_Ini.GetString("System", "ESBClassName", "");
            m_GatewayTimeout = lSettingConfigure_Ini.GetInt32("System", "GatewayTimeout", 60) * 1000;

            m_Gateway = new GatewayConnector();
            m_Gateway.Profile = l_Ini.GetString("System", "Profile", "");
            m_Gateway.EntityName = l_Ini.GetString("System", "EntityName", "");
            m_Gateway.EnableLog = true;
            m_Gateway.OnMessageArrival += new GatewayEventHandler(OnMessageArrival);
            m_Gateway.OpenFreeThread();
        }

        void OnMessageArrival(object sender, GatewayMessageArgs args)
        {
            switch (args.EventId)
            {
                case ManagedEventType.Discnnect:
                    m_GatewayConnect = false;
                    break;
                case ManagedEventType.EntityArrival:
                case ManagedEventType.ResumeRequest:
                    if (args.Destination == m_Gateway.EntityName)
                        m_GatewayConnect = true;
                    break;
                default:
                    if ((m_GatewaySendId != 0) && (m_GatewaySendId == args.MessageId))
                    {
                        m_GatewaySendId = 0;
                        m_GatewayResult = args;
                        m_GatewaySync.Set();
                    }
                    break;
            }
        }

        public IMessage MessageGateWayRequest(string StdMessageName, string destClassName, Dictionary<string, string> dicParam, uint attach, int timeout)
        {
            StdMessage message = new StdMessage(StdMessageName);  
            foreach (string strKey in dicParam.Keys)
            {
                message.Parameters.Add(strKey, dicParam[strKey]);
            }
            
            int l_TimeOut = timeout == 0 ? m_GatewayTimeout : timeout;
            m_GatewaySync.Reset();
            m_GatewayResult = null;
            m_GatewaySendId = this.m_Gateway.Request(destClassName, message, attach);
            if (m_GatewaySendId == 0)
                return null;
            if (!m_GatewaySync.WaitOne(l_TimeOut, true))
            {
                m_GatewaySendId = 0;
                return null;
            }
            return m_GatewayResult.UserMessage;
        }

        public bool WaitConnect(int timeout)
        {
            Thread.Sleep(100);
            while (!m_GatewayConnect && (timeout > 0))
            {
                timeout--;
                Thread.Sleep(1000);
            }
            return m_GatewayConnect;
        }

        public void Dispose()
        {
            this.ReleaseConnection();
        }

        //取消連線
        private void ReleaseConnection()
        {
            try
            {
                //若是連線存在且非斷線狀態，需要將連線關閉
                if (this.m_Gateway != null && this.m_GatewayConnect == true)
                    this.m_Gateway.Close();
            }
            catch (Exception ex) { };
        }
    }
}
