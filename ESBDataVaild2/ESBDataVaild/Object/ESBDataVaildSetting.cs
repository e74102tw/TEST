using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engsound.Configuration;


namespace ESBDataVaild.Object
{
    public class ESBDataVaildSetting
    {
        private IniProfile l_Ini;
        private List<string> m_RemoveCompareItem = new List<string>();
        private string m_SaveOutPutPath = "";
        public string SaveOutPutPath { get { return m_SaveOutPutPath; } }
        private Dictionary<string, string> m_ChildkeyColumns;

        public ESBDataVaildSetting()
        {
            IniProfile l_Ini = new IniProfile(Path.GetFullPath("configure.ini"));
            int RemoveCompareItemCount = l_Ini.GetInt32("RemoveCompareItem", "Count", 0);
            //不加入比對參數至List
            for (int i = 0; i < RemoveCompareItemCount; i++)
            {
                string strRemoveCompareItem = l_Ini.GetString("RemoveCompareItem", "Item" + (i + 1), "");
                if (strRemoveCompareItem.Trim() != "" && !m_RemoveCompareItem.Contains(strRemoveCompareItem))
                    this.m_RemoveCompareItem.Add(strRemoveCompareItem);
            }

            m_SaveOutPutPath = Path.Combine(Application.StartupPath, "SAVE");
            if (!Directory.Exists(m_SaveOutPutPath))
                Directory.CreateDirectory(m_SaveOutPutPath);

            m_ChildkeyColumns = new Dictionary<string, string>();
            if (l_Ini.SectionExist("Childkeys"))
            {
                foreach (KeyValuePair<string, string> item in l_Ini["Childkeys"])
                {
                    m_ChildkeyColumns.Add(item.Key, item.Value);
                }
            }
        }

        public bool isRemoveCompareItem(string strParameterName, int intDATACount = 0)
        {
            //檢查是否在剔除清單內(多筆檢查)
            bool blnRemove = false;
            foreach (string strR in m_RemoveCompareItem)
            {
                for (int i = 0; i < intDATACount; i++)
                {
                    string strK = strR.Trim().ToUpper() + i.ToString();
                    if (strK == strParameterName.Trim().ToUpper())
                    {
                        blnRemove = true;
                        break;
                    }
                }
            }
            if (blnRemove || isRemoveCompareItem(strParameterName.Trim().ToUpper()))
                return true;
            else
                return false;
        }

        private bool isRemoveCompareItem(string strChildkeys)
        {
            if (strChildkeys.Trim() == "" || !m_RemoveCompareItem.Contains(strChildkeys.Trim().ToUpper()))
                return false;
            else
                return true;
        }

        public List<string> GetChildkeys(string ActionName)
        {
            try
            {
                List<string> lstRet = null;
                if (this.m_ChildkeyColumns.Keys.Contains(ActionName + "Key"))
                {
                    string[] ArraryChildkeys = this.m_ChildkeyColumns[ActionName + "Key"].Split(',');
                    lstRet =  ArraryChildkeys == null ? null : new List<string>(ArraryChildkeys);
                }
                else
                {
                    lstRet = null;
                }
                return lstRet;
            }
            catch { return null; }
        }
        public List<string> GetChildColumns(string ActionName)
        {
            try
            {
                List<string> lstRet = null;
                if (this.m_ChildkeyColumns.Keys.Contains(ActionName))
                {
                    string[] ArraryChildColumns = this.m_ChildkeyColumns[ActionName].Split(',');
                    lstRet = ArraryChildColumns == null ? null : new List<string>(ArraryChildColumns);
                }
                else
                {
                    lstRet = null;
                }
                return lstRet;
            }
            catch { return null; }
        }
    }
}
