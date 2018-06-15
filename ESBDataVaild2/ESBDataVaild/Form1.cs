using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Engsound.Gateway;
using Engsound.Configuration;
using Engsound.Network;
using ExtendMethods;
using ExtendMethods.Enum;
using ExtendMethods.Module;
using ESBDataVaild.Enum;
using ESBDataVaild.Object;


namespace ESBDataVaild
{
    public partial class Form1 : Form
    {
        #region 共用參數
        private bool m_CancelCase = false;
        private Logger m_Log;
        private ESBDataVaildSetting ESBSetting;
        private string m_Func = "";
        private Enum_InputType m_enumInputType;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            m_Log = new Logger(Program.m_Gateway.LogName);
            ESBSetting = new ESBDataVaildSetting();
            this.Text = this.Text + "  V." + Application.ProductVersion;
            EnableFunc(true);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_CancelCase = true;
            Program.m_Gateway.Dispose();
            backgroundWorker1.CancelAsync();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(tbxFunc.Text.Trim() == "")
            {
                MessageBox.Show("請輸入Function名稱");
            }
            DataSetClear();
            EnableFunc(false);
            m_Func = tbxFunc.Text;
            this.m_CancelCase = false;
            backgroundWorker1.RunWorkerAsync();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.m_CancelCase = true;
            this.backgroundWorker1.CancelAsync();
            EnableFunc(true);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DSCompare.DTCompareRow drC in dSCompare.DTCompare)
            {
                if (drC.ECSDATACount != drC.ESBDATACount)
                {
                    sb.AppendLine(string.Format(@"Card No: {0}|Action: {1}|ADDParameter: {2}|ECMS Time: {3}|MLI Time: {4}", drC.CARDNO, drC.FUNCTION, drC.ADDParameter, drC.ECSRecState, drC.ESBRecState));
                    sb.AppendLine(string.Format(@"Key: DATACOUNT|ECMS Value: {0}|ESBS Value: {1}|Compare: 回應卡號筆數不符", drC.ECSDATACount,drC.ESBDATACount));
                    sb.AppendLine("************************************************");
                }
                else
                {
                    sb.AppendLine(string.Format(@"Card No: {0}|Action: {1}|ADDParameter: {2}|ECMS Time: {3}|MLI Time: {4}", drC.CARDNO, drC.FUNCTION, drC.ADDParameter, drC.ECSRecState, drC.ESBRecState));
                    foreach(DSCompare.DTCompareDetailRow drCD in dSCompare.DTCompareDetail)
                    {
                        if(drCD.C_ID != drC.C_ID)
                            continue;
                        sb.AppendLine(string.Format(@"Key: {0}|ECMS Value: {1}|ECMS Type: {2}|ESBS Value: {3}|ESBS Type: {4}|Compare: {5}", drCD.ECSParameterName
                            , drCD.ECSParameterValue, drCD.ECSParameterType, drCD.ESBParameterValue, drCD.ESBParameterType, drCD.CompareResult));
                    }
                    sb.AppendLine("**********ECMS Detail:");
                    DSCompare.DTECSRow[] drECSArray = dSCompare.DTECS.Cast<DSCompare.DTECSRow>().Where(w => w.C_ID == drC.C_ID).ToArray();
                    if (drECSArray != null && drECSArray.Length > 0)
                    {
                        foreach (DSCompare.DTECSDetailRow drECSD in dSCompare.DTECSDetail)
                        {
                            if (drECSD.ECS_ID != drECSArray[0].ECS_ID)
                                continue;
                            sb.AppendLine(string.Format(@"Key: {0}|ECMS Value: {1}", drECSD.ParameterName, drECSD.ParameterValue));
                        }
                    }
                    sb.AppendLine("*********MLI Detail:");
                    DSCompare.DTESBRow[] drESBArray = dSCompare.DTESB.Cast<DSCompare.DTESBRow>().Where(w => w.C_ID == drC.C_ID).ToArray();
                    if (drESBArray != null && drESBArray.Length > 0)
                    {
                        foreach (DSCompare.DTESBDetailRow drESBD in dSCompare.DTESBDetail)
                        {
                            if (drESBD.ESB_ID != drESBArray[0].ESB_ID)
                                continue;
                            sb.AppendLine(string.Format(@"Key: {0}|ESBS Value: {1}", drESBD.ParameterName, drESBD.ParameterValue));
                        }
                    }
                    sb.AppendLine("************************************************");
                }
            }

            string strLogName = Program.m_Gateway.LogName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + this.m_Func + ".txt";
            File.AppendAllText(Path.Combine(this.ESBSetting.SaveOutPutPath, strLogName), sb.ToString());
            MessageBox.Show("匯出完成!");
        }
        private void dgvCompare_SelectionChanged(object sender, EventArgs e)
        {
            int C_ID = -1;
            if (dgvCompare.SelectedRows.Count > 0)
            {
                DataGridViewRow vw = dgvCompare.SelectedRows[0] as DataGridViewRow;
                DataRow row = (vw.DataBoundItem as DataRowView).Row;
                C_ID = Convert.ToInt32(row["C_ID"].ToString());
            }
            DataTable dt = new DataTable();
            try
            {
                dt = dSCompare.DTCompareDetail.Cast<DSCompare.DTCompareDetailRow>().Where(w => w.C_ID == C_ID).CopyToDataTable();
            }
            catch { };
            SetDGVDetail(dt);
        }

        //設定DataGridView
        private void SetDGV(DataTable dt)
        {
            this.dgvCompare.DataSource = dt;
            try
            {
                BuildGridSchema(dgvCompare);
            }
            catch { };
        }
        private void BuildGridSchema(DataGridView dgv)
        {
            //dgv.RowHeadersVisible = false;
            //dgv.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            dgv.Columns["ECS_ID"].Visible = false;
            dgv.Columns["ESB_ID"].Visible = false;
        }
        //設定DataGridView
        private void SetDGVDetail(DataTable dt)
        {
            this.dgvCompareDetail.DataSource = dt;
            try
            {
                BuildGridSchemaDetail(dgvCompareDetail);
            }
            catch { };
        }
        private void BuildGridSchemaDetail(DataGridView dgv)
        {
            //dgv.RowHeadersVisible = false;
            dgv.AutoResizeColumns();
            dgv.Columns["CD_ID"].Visible = false;
            dgv.Columns["ECSD_ID"].Visible = false;
            dgv.Columns["ESBD_ID"].Visible = false;
        }

        #region Reques Method
        //ECMSWorker-卡號查詢
        public IMessage ECMSWorkerRequest_GetData(string FunctionName, Enum_InputType EIType, string Input, Dictionary<string, string> dicAddParameter, uint attach)
        {
            Dictionary<string, string> dicParm = new Dictionary<string, string>();
            dicParm.Add(EIType.ToString(), Input);
            
            foreach (string str in dicAddParameter.Keys)
            {
                if(!dicParm.Keys.Contains(str))
                    dicParm.Add(str,dicAddParameter[str]);
            }
            return Program.m_Gateway.MessageGateWayRequest(FunctionName, Program.m_Gateway.ECMSClassName, dicParm, attach, 0);
        }
        //ESBGateway-卡號查詢
        public IMessage ESBGatewayRequest_GetGetData(string FunctionName, Enum_InputType EIType, string Input, Dictionary<string, string> dicAddParameter, uint attach, out string strCUSTNO)
        {
            Dictionary<string, string> dicParm = new Dictionary<string, string>();
            strCUSTNO = "";
            dicParm.Add(EIType.ToString(), Input);
            if (EIType == Enum_InputType.IDNO)
            {
                Dictionary<string, string> dicTemp = new Dictionary<string, string>();
                dicTemp.Add("IDNO", Input.Trim());
                IMessage IMTemp = Program.m_Gateway.MessageGateWayRequest("ESBGETCUSTNBR", Program.m_Gateway.ECMSClassName, dicTemp, attach, 0);
                if (IMTemp != null && (IMTemp.ResultCode == 0 && IMTemp.Parameters.ContainsKey("RETCD") && IMTemp.Parameters["RETCD"] == 0))
                {
                    strCUSTNO = IMTemp.Parameters["CustomerNo"].ToString().Trim();
                    dicParm.Add("CUSTNO", strCUSTNO);
                }
            }

            foreach (string str in dicAddParameter.Keys)
            {
                if (!dicParm.Keys.Contains(str))
                    dicParm.Add(str, dicAddParameter[str]);
            }
            return Program.m_Gateway.MessageGateWayRequest(FunctionName, Program.m_Gateway.ESBClassName, dicParm, attach, 0);
        }
        #endregion

        #region Method
        private int ADDECSDataToDS(int C_ID,IMessage ECS_Data)
        {
            int ECS_ID = -1;
            if (ECS_Data == null)
                return ECS_ID; 
            //ECS
            DSCompare.DTECSRow drECS = dSCompare.DTECS.NewDTECSRow();
            drECS.C_ID = C_ID;
            drECS.Action = ECS_Data.Action;
            drECS.ResultCode = ECS_Data.ResultCode;
            drECS.Sender = ECS_Data.Sender;
            ECS_ID = drECS.ECS_ID;//紀錄ID
            dSCompare.DTECS.AddDTECSRow(drECS);
            //detail
            Dictionary<string, PElement> dicECS = ECS_Data.Parameters as Dictionary<string, PElement>;
            if (dicECS != null)
            {
                foreach (string key in dicECS.Keys)
                {
                    DSCompare.DTECSDetailRow drECSD = dSCompare.DTECSDetail.NewDTECSDetailRow();
                    drECSD.ECS_ID = ECS_ID;
                    drECSD.ParameterName = key;
                    drECSD.ParameterValue = dicECS[key].Value;
                    drECSD.ParameterType = dicECS[key].Value.GetType().ToString();
                    dSCompare.DTECSDetail.AddDTECSDetailRow(drECSD);
                }
            }
            return ECS_ID;
        }
        private int ADDESBDataToDS(int C_ID, IMessage ESB_Data)
        {
            int ESB_ID = -1;
            if (ESB_Data == null)
                return ESB_ID;
            //ESB
            DSCompare.DTESBRow drESB = dSCompare.DTESB.NewDTESBRow();
            drESB.C_ID = C_ID;
            drESB.Action = ESB_Data.Action;
            drESB.ResultCode = ESB_Data.ResultCode;
            drESB.Sender = ESB_Data.Sender;
            ESB_ID = drESB.ESB_ID;//紀錄ID
            dSCompare.DTESB.AddDTESBRow(drESB);
            //detail
            Dictionary<string, PElement> dicESB = ESB_Data.Parameters as Dictionary<string, PElement>;
            if (dicESB != null)
            {
                foreach (string key in dicESB.Keys)
                {
                    DSCompare.DTESBDetailRow drESBD = dSCompare.DTESBDetail.NewDTESBDetailRow();
                    drESBD.ESB_ID = ESB_ID;
                    drESBD.ParameterName = key;
                    drESBD.ParameterValue = dicESB[key].Value;
                    drESBD.ParameterType = dicESB[key].Value.GetType().ToString();
                    dSCompare.DTESBDetail.AddDTESBDetailRow(drESBD);
                }
            }
            return ESB_ID;
        }
        private void ADDCompareResultToDS(int C_ID, DSCompare.DTECSDetailRow drECSD, DSCompare.DTESBDetailRow drESBD)
        {
            DSCompare.DTCompareDetailRow drDTCD = dSCompare.DTCompareDetail.NewDTCompareDetailRow();
            drDTCD.C_ID = C_ID;
            drDTCD.ECSD_ID = drECSD.ECSD_ID;
            drDTCD.ECSParameterName = drECSD.ParameterName;
            drDTCD.ECSParameterValue = drECSD.ParameterValue;
            drDTCD.ECSParameterType = drECSD.ParameterType;
            if (drESBD == null)
            {
                drDTCD.ESBD_ID = -1;
                drDTCD.ESBParameterName = "";
                drDTCD.ESBParameterValue = null;
                drDTCD.ESBParameterType = "";
                drDTCD.CompareResult = enum_CompareResult.不符合.ToString();
            }
            else
            {
                drDTCD.ESBD_ID = drESBD.ESBD_ID;
                drDTCD.ESBParameterName = drESBD.ParameterName;
                drDTCD.ESBParameterValue = drESBD.ParameterValue;
                drDTCD.ESBParameterType = drESBD.ParameterType;
                drDTCD.CompareResult = drDTCD.ECSParameterValue.ToString() == drDTCD.ESBParameterValue.ToString() ? enum_CompareResult.符合.ToString() : enum_CompareResult.不符合.ToString();
            }

            dSCompare.DTCompareDetail.AddDTCompareDetailRow(drDTCD);
        }
        private Dictionary<string, string> GetAddParameter(string strADD)
        {
            //ex: FETCHALL=Y,FETCHWIBO=Y
            Dictionary<string, string> dicRet = new Dictionary<string, string>();
            try
            {
                string[] arraySplit = strADD.Split(',');
                foreach (string str1 in arraySplit)
                {
                    if (str1.Trim() == "")
                        continue;
                    string[] arraySplit2 = str1.Split('=');//ex: FETCHALL=Y
                    if (arraySplit2.Length == 2)
                    {
                        string strSplit2_1 = arraySplit2[0].ToUpper();
                        string strSplit2_2 = arraySplit2[1].ToUpper();
                        if (strSplit2_1.Trim() != "")
                        {
                            if (!dicRet.Keys.Contains(strSplit2_1))
                                dicRet.Add(strSplit2_1, strSplit2_2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dicRet.Clear();
            }
            return dicRet;
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            decimal i = 0;
            const decimal Percent100 = 100;
            List<string> sl = new List<string>();
            CheckInputType();
            foreach (string strInputNo in tbxInputNo.Lines)
            {
                if (strInputNo.Trim() != "")
                    sl.Add(strInputNo);
            }
            foreach (string strInputNo in sl)
            {
                if (m_CancelCase == true)
                {
                    e.Cancel = true;
                    break;
                }
                i++;
                decimal slCount = Convert.ToDecimal(sl.Count.ToString());
                int report = Convert.ToInt32( (i * (Percent100 / slCount)).ToString().strToDecPointWithOption(0, enumStrToIntOption.Unconditional_Carry).ToString() );
                if (report > 100)
                    report = 100;
                FilterData(strInputNo.Trim());
                Application.DoEvents();
                backgroundWorker1.ReportProgress(report); //回報進度
            }
        }
        private void FilterData(string strInputNo)
        {
            System.Diagnostics.Stopwatch _sw = new System.Diagnostics.Stopwatch();
            DSCompare.DTCompareRow drC = dSCompare.DTCompare.NewDTCompareRow();
            Dictionary<string, string> dicAddParameter = GetAddParameter(this.tbxADDParameter.Text.Trim());
            string strCUSTNO = "";
            _sw.Reset();
            _sw.Start();

            //ECMS
            IMessage ECS_Data = ECMSWorkerRequest_GetData(this.tbxFunc.Text.Trim(), m_enumInputType, strInputNo, dicAddParameter, 0);
            _sw.Stop();
            drC.ECSRecState = (_sw.Elapsed.TotalMilliseconds/1000.0).ToString("0.00");

            _sw.Reset();
            _sw.Start();
            //ESB
            IMessage ESB_Data = ESBGatewayRequest_GetGetData(this.tbxFunc.Text.Trim(), m_enumInputType, strInputNo, dicAddParameter, 0, out strCUSTNO);
            _sw.Stop();
            drC.ESBRecState = (_sw.Elapsed.TotalMilliseconds/1000.0).ToString("0.00");


            switch (m_enumInputType)
            {
                case Enum_InputType.CARDNO:
                    drC.IDNO = "";
                    drC.CARDNO = strInputNo.Trim();
                    drC.CUSTNO = "";
                    break;
                case Enum_InputType.IDNO:
                    drC.IDNO = strInputNo.Trim();
                    drC.CARDNO = "";
                    drC.CUSTNO = strCUSTNO;
                    break;
            }
            drC.InputType = m_enumInputType.ToString();
            drC.FUNCTION = this.tbxFunc.Text.Trim();
            drC.ADDParameter = this.tbxADDParameter.Text.Trim();
            drC.ECS_ID = ADDECSDataToDS(drC.C_ID, ECS_Data);
            drC.ECSResultCode = ECS_Data == null ? "-1": ECS_Data.ResultCode.ToString();
            
            
            drC.ESB_ID = ADDESBDataToDS(drC.C_ID, ESB_Data);
            drC.ESBResultCode = ESB_Data == null ? "-1" : ESB_Data.ResultCode.ToString();

            //取得DataCount
            int intECSDataCount = -1, intECSRETCD = -1, intESBDataCount = -1, intESBRETCD = -1;
            GetDataCount(drC.C_ID, out intECSDataCount, out intECSRETCD, out intESBDataCount, out intESBRETCD);
            drC.ECSDATACount = intECSDataCount;
            drC.ESBDATACount = intESBDataCount;
            drC.ECSRetCD = intECSRETCD;
            drC.ESBRetCD = intESBRETCD;
            drC.CompareResult = GetCompareResult(drC.C_ID, drC.ECSDATACount, drC.ESBDATACount).ToString();
            dSCompare.DTCompare.AddDTCompareRow(drC);
            dSCompare.AcceptChanges();

        }
        private enum_CompareResult GetCompareResult(int C_ID, int ECSDATACount, int ESBDATACount)
        {
            enum_CompareResult Result = enum_CompareResult.待比較;
            try
            {
                DSCompare.DTECSRow[] drECSArray = dSCompare.DTECS.Cast<DSCompare.DTECSRow>().Where(w => w.C_ID == C_ID).ToArray();
                DSCompare.DTESBRow[] drESBArray = dSCompare.DTESB.Cast<DSCompare.DTESBRow>().Where(w => w.C_ID == C_ID).ToArray();
                int ECS_ID = -1, ESB_ID = -1;
                if (drECSArray != null && drECSArray.Length > 0)
                    ECS_ID = drECSArray[0].ECS_ID;
                if (drESBArray != null && drESBArray.Length > 0)
                    ESB_ID = drESBArray[0].ESB_ID;

                DSCompare.DTECSDetailRow[] drECSDArray = dSCompare.DTECSDetail.Cast<DSCompare.DTECSDetailRow>().Where(w => w.ECS_ID == ECS_ID).ToArray();
                DSCompare.DTESBDetailRow[] drESBDArray = dSCompare.DTESBDetail.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ESB_ID == ESB_ID).ToArray();

                if (ECS_ID == -1 || ESB_ID == -1 || drECSDArray == null || drESBDArray == null)
                {
                    throw new Exception("無比對資料");
                }
                List<string> Childkeys = ESBSetting.GetChildkeys(m_Func.Trim().ToUpper());
                List<string> ChildColumns = ESBSetting.GetChildColumns(m_Func.Trim().ToUpper());
                #region 寫Compare明細
                if (Childkeys != null && Childkeys.Count() > 0)//針對Child多筆做處理
                {
                    Dictionary<string, string> lstParam = new Dictionary<string, string>();
                    for (int i = 0; i < ECSDATACount; i++)
                    {
                        lstParam.Clear();
                        foreach (string sParamName in Childkeys)
                        {
                            DSCompare.DTECSDetailRow drECSD = drECSDArray.Cast<DSCompare.DTECSDetailRow>().Where(w => w.ParameterName.Trim().ToUpper() == (sParamName + i.ToString())).FirstOrDefault();
                            if (drECSD == null)
                                continue;
                            if (lstParam.ContainsKey(sParamName))
                                lstParam[sParamName] = drECSD.ParameterValue.ToString().Trim();
                            else
                                lstParam.Add(sParamName, drECSD.ParameterValue.ToString().Trim());
                        }
                        int intESBIndex = -1;
                        intESBIndex = GetIndexWithBaseParamWithMutiKey(lstParam, C_ID, ESBDATACount);
                        foreach (string strChildColumns in ChildColumns)
                        {
                            if (strChildColumns.Trim() == "")
                                continue;
                            if (ESBSetting.isRemoveCompareItem(strChildColumns))
                                continue;
                            DSCompare.DTECSDetailRow[] drECSDSelectDArray = drECSDArray.Cast<DSCompare.DTECSDetailRow>().Where(w => w.ParameterName.Trim().ToUpper() == (strChildColumns + i.ToString())).ToArray();
                            if (drECSDSelectDArray == null || drECSDSelectDArray.Length == 0)
                                continue;
                            DSCompare.DTECSDetailRow drECSD = drECSDSelectDArray[0];
                            DSCompare.DTESBDetailRow[] drESBSelectDArray = drESBDArray.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ParameterName.Trim().ToUpper() == (strChildColumns + intESBIndex.ToString())).ToArray();
                            if (drESBSelectDArray == null || drESBSelectDArray.Length == 0)
                                ADDCompareResultToDS(C_ID, drECSD, null);
                            else
                                ADDCompareResultToDS(C_ID, drECSD, drESBSelectDArray[0]);
                        }
                    }
                }
                else
                {
                    foreach (DSCompare.DTECSDetailRow drECSD in drECSDArray)
                    {
                        if (drECSD.ParameterName.Trim() == "")
                            continue;
                        if (ESBSetting.isRemoveCompareItem(drECSD.ParameterName.Trim(), ECSDATACount))
                            continue;
                        DSCompare.DTESBDetailRow[] drESBSelectDArray = drESBDArray.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ParameterName.Trim().ToUpper() == drECSD.ParameterName.ToUpper()).ToArray();
                        if (drESBSelectDArray == null || drESBSelectDArray.Length == 0)
                            ADDCompareResultToDS(C_ID, drECSD, null);
                        else
                            ADDCompareResultToDS(C_ID, drECSD, drESBSelectDArray[0]);
                    }
                }
                #endregion
                int NotCompareCount = dSCompare.DTCompareDetail.Cast<DSCompare.DTCompareDetailRow>().Where(w => w.C_ID == C_ID && w.CompareResult == enum_CompareResult.不符合.ToString()).Count();
                Result = NotCompareCount > 0 ? enum_CompareResult.不符合 : enum_CompareResult.符合;
            }
            catch (Exception ex)
            {
                this.m_Log.WriteLog("GetCompareResult", ex.Message);
                Result = enum_CompareResult.比對錯誤;
            }

            return Result;
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            //this.dgvCompare.Refresh();
            this.progressBar1.Visible = true; //顯示進度條
            Application.DoEvents();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableFunc(true);
            this.progressBar1.Visible = false; //顯示進度條
            MessageBox.Show("處理完畢!");
            DataTable dt = new DataTable();
            try
            {
                dt = dSCompare.DTCompare.Cast<DSCompare.DTCompareRow>().CopyToDataTable();
            }
            catch { };
            SetDGV(dt);
        }
        
        private void EnableFunc(bool enable)
        {
            this.btnStart.Enabled = enable;
            this.btnStop.Enabled = !enable;
            this.btnExport.Enabled = enable;
            this.tbxFunc.Enabled = enable;
            this.tbxADDParameter.Enabled = enable;
            this.tbxInputNo.Enabled = enable;
            this.btnExportExcel.Enabled = enable;
            //this.dgvCompare.Enabled = enable;
            this.panel4.Enabled = enable;
            this.panel5.Enabled = enable;
            this.gbxInputType.Enabled = enable;
        }
        private void CheckInputType()
        {
            m_enumInputType = Enum_InputType.CARDNO;
            if (rbtnCard.Checked == true)
                m_enumInputType = Enum_InputType.CARDNO;
            else if (rbtnID.Checked == true)
                m_enumInputType = Enum_InputType.IDNO;
            
        }
        private void DataSetClear()
        {
            foreach (DataTable dt in dSCompare.Tables)
            {
                dt.Rows.Clear();
                foreach (DataColumn co in dt.Columns)
                {
                    if (co.AutoIncrement)
                    {
                        co.AutoIncrementSeed = -1;
                        co.AutoIncrementStep = -1;
      
                        co.AutoIncrementSeed = 0;
                        co.AutoIncrementStep = 1;
                    }
                }
            }
            dSCompare.AcceptChanges();
        }
        private bool GetDataCount(int C_ID, out int intECSDataCount, out int intECSRETCD, out int intESBDataCount, out int intESBRETCD)
        {
            bool blnRet = false;
            intECSDataCount = -1;
            intESBDataCount = -1;
            intECSRETCD = -1;
            intESBRETCD = -1;

            DSCompare.DTECSRow[] drECSArray = dSCompare.DTECS.Cast<DSCompare.DTECSRow>().Where(w => w.C_ID == C_ID).ToArray();
            DSCompare.DTESBRow[] drESBArray = dSCompare.DTESB.Cast<DSCompare.DTESBRow>().Where(w => w.C_ID == C_ID).ToArray();

            int ECS_ID = -1, ESB_ID = -1;
            if (drECSArray != null && drECSArray.Length > 0)
                ECS_ID = drECSArray[0].ECS_ID;
            if (drESBArray != null && drESBArray.Length > 0)
                ESB_ID = drESBArray[0].ESB_ID;

            DSCompare.DTECSDetailRow[] drECSDArray_DATACOUNT = dSCompare.DTECSDetail.Cast<DSCompare.DTECSDetailRow>().Where(w => w.ECS_ID == ECS_ID && w.ParameterName.Trim().ToUpper() == "DATACOUNT").ToArray();
            DSCompare.DTESBDetailRow[] drESBDArray_DATACOUNT = dSCompare.DTESBDetail.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ESB_ID == ESB_ID && w.ParameterName.Trim().ToUpper() == "DATACOUNT").ToArray();
            if (drECSDArray_DATACOUNT != null && drECSDArray_DATACOUNT.Length > 0)
                intECSDataCount = drECSDArray_DATACOUNT[0].ParameterValue == null ? 0 : Convert.ToInt32(drECSDArray_DATACOUNT[0].ParameterValue);
            if (drESBDArray_DATACOUNT != null && drESBDArray_DATACOUNT.Length > 0)
                intESBDataCount = drESBDArray_DATACOUNT[0].ParameterValue == null ? 0 : Convert.ToInt32(drESBDArray_DATACOUNT[0].ParameterValue);

            DSCompare.DTECSDetailRow[] drECSDArray_RETCD = dSCompare.DTECSDetail.Cast<DSCompare.DTECSDetailRow>().Where(w => w.ECS_ID == ECS_ID && w.ParameterName.Trim().ToUpper() == "RETCD").ToArray();
            DSCompare.DTESBDetailRow[] drESBDArray_RETCD = dSCompare.DTESBDetail.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ESB_ID == ESB_ID && w.ParameterName.Trim().ToUpper() == "RETCD").ToArray();
            if (drECSDArray_RETCD != null && drECSDArray_RETCD.Length > 0)
                intECSRETCD = drECSDArray_RETCD[0].ParameterValue == null ? 0 : Convert.ToInt32(drECSDArray_RETCD[0].ParameterValue);
            if (drESBDArray_RETCD != null && drESBDArray_RETCD.Length > 0)
                intESBRETCD = drESBDArray_RETCD[0].ParameterValue == null ? 0 : Convert.ToInt32(drESBDArray_RETCD[0].ParameterValue);

            return blnRet;
        }
        private int GetIndexWithBaseParam(string strParamName,string strParamValue, int C_ID, int ESBDATACount)
        {
            int intRet = -1;
            if (strParamValue.Trim() == "")
                return intRet;
            try
            {
                if(strParamName == "CARDNO")
                    strParamValue = strParamValue.PadLeft(19, '0');
                int intDataCount = ESBDATACount;
                DSCompare.DTESBRow[] drESBArray = dSCompare.DTESB.Cast<DSCompare.DTESBRow>().Where(w => w.C_ID == C_ID).ToArray();
                int ESB_ID = -1;
                if (drESBArray != null && drESBArray.Length > 0)
                    ESB_ID = drESBArray[0].ESB_ID;
                
                for (int i = 0; i < intDataCount; i++)
                {
                    DSCompare.DTESBDetailRow[] drESBDArray = dSCompare.DTESBDetail.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ESB_ID == ESB_ID && w.ParameterName == strParamName + i.ToString()).ToArray();
                    string strITEMParamValue = "";
                    if (drESBDArray != null && drESBDArray.Length > 0)
                    {
                        if (strParamName == "CARDNO")
                            strITEMParamValue = drESBDArray[0].ParameterValue.ToString().Trim().PadLeft(19, '0');
                        else
                            strITEMParamValue = drESBDArray[0].ParameterValue.ToString().Trim();
                    }
                    if (strITEMParamValue == strParamValue)
                    {
                        intRet = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_Log.WriteLog("GetIndexWithBaseParam", ex.Message);
                intRet = -1;
            }
            return intRet;
        }
        private int GetIndexWithBaseParamWithMutiKey(Dictionary<string,string> lstParam, int C_ID, int ESBDATACount)
        {
            int intRet = -1;
            if (lstParam.Count == 0)
                return intRet;
            try
            {
                lstParam.Keys.Cast<string>().Where(w => w.ToUpper().Trim() == "CARDNO").Select(s => lstParam[s] = lstParam[s].PadLeft(19, '0')).ToList();
                DSCompare.DTESBRow drESB = dSCompare.DTESB.Cast<DSCompare.DTESBRow>().Where(w => w.C_ID == C_ID).FirstOrDefault();
                int ESB_ID = -1;
                if (drESB != null)
                    ESB_ID = drESB.ESB_ID;

                Dictionary<string, string> lstParamCompare = new Dictionary<string, string>();
                bool blnCompare = false;
                for (int i = 0; i < ESBDATACount; i++)
                {
                    lstParamCompare.Clear();
                    foreach (string sParameterName in lstParam.Keys)
                    {
                        DSCompare.DTESBDetailRow drESBD = dSCompare.DTESBDetail.Cast<DSCompare.DTESBDetailRow>().Where(w => w.ESB_ID == ESB_ID && w.ParameterName == sParameterName + i.ToString()).FirstOrDefault();
                        if (drESBD == null)
                            continue;
                        if (lstParamCompare.ContainsKey(sParameterName))
                            lstParamCompare[sParameterName] = drESBD.ParameterValue.ToString().Trim();
                        else
                            lstParamCompare.Add(sParameterName, drESBD.ParameterValue.ToString().Trim());
                    }

                    blnCompare = lstParam.Keys.Cast<string>().Where(w => lstParamCompare .ContainsKey(w) && lstParam[w] == lstParamCompare[w]).Count() == lstParam.Count;
                    if (blnCompare)
                    {
                        intRet = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_Log.WriteLog("GetIndexWithBaseParamWithMutiKey", ex.Message);
                intRet = -1;
            }
            return intRet;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //DTCompare
            DataGridView dgv_export = new DataGridView();
            DataTable dtTemp = dSCompare.DTCompare.CopyToDataTable();
            dgv_export.DataSource = dtTemp;
            this.Controls.Add(dgv_export);
            dgv_export.Name = "dgv_export";
            dgv_export.Dock = DockStyle.Fill;
            dgv_export.Visible = false;
            //DTCompareDetail
            DataGridView dgv_exportDetail = new DataGridView();
            DataTable dtTempDetail = dSCompare.DTCompareDetail.CopyToDataTable();
            dgv_exportDetail.DataSource = dtTempDetail;
            this.Controls.Add(dgv_exportDetail);
            dgv_exportDetail.Name = "dgv_exportDetail";
            dgv_exportDetail.Dock = DockStyle.Fill;
            dgv_exportDetail.Visible = false;
            //DTECS
            DataGridView dgv_exportECS = new DataGridView();
            DataTable dtTempECS = dSCompare.DTECS.CopyToDataTable();
            dgv_exportECS.DataSource = dtTempECS;
            this.Controls.Add(dgv_exportECS);
            dgv_exportECS.Name = "dgv_exportECS";
            dgv_exportECS.Dock = DockStyle.Fill;
            dgv_exportECS.Visible = false;
            //DTECSDetail
            DataGridView dgv_exportECSDetail = new DataGridView();
            DataTable dtTempECSDetail = dSCompare.DTECSDetail.CopyToDataTable();
            dgv_exportECSDetail.DataSource = dtTempECSDetail;
            this.Controls.Add(dgv_exportECSDetail);
            dgv_exportECSDetail.Name = "dgv_exportECSDetail";
            dgv_exportECSDetail.Dock = DockStyle.Fill;
            dgv_exportECSDetail.Visible = false;
            //DTESB
            DataGridView dgv_exportESB = new DataGridView();
            DataTable dtTempESB = dSCompare.DTESB.CopyToDataTable();
            dgv_exportESB.DataSource = dtTempESB;
            this.Controls.Add(dgv_exportESB);
            dgv_exportESB.Name = "dgv_exportESB";
            dgv_exportESB.Dock = DockStyle.Fill;
            dgv_exportESB.Visible = false;
            //DTESBDetail
            DataGridView dgv_exportESBDetail = new DataGridView();
            DataTable dtTempESBDetail = dSCompare.DTESBDetail.CopyToDataTable();
            dgv_exportESBDetail.DataSource = dtTempESBDetail;
            this.Controls.Add(dgv_exportESBDetail);
            dgv_exportESBDetail.Name = "dgv_exportESBDetail";
            dgv_exportESBDetail.Dock = DockStyle.Fill;
            dgv_exportESBDetail.Visible = false;

            List<DataGridView> lstDGV = new List<DataGridView>();
            lstDGV.Add(dgv_export);
            lstDGV.Add(dgv_exportDetail);
            lstDGV.Add(dgv_exportECS);
            lstDGV.Add(dgv_exportECSDetail);
            lstDGV.Add(dgv_exportESB);
            lstDGV.Add(dgv_exportESBDetail);
            List<string> lstSheetName = new List<string>();
            lstSheetName.Add("DTCompare");
            lstSheetName.Add("DTCompareDetail");
            lstSheetName.Add("DTECS");
            lstSheetName.Add("DTECSDetail");
            lstSheetName.Add("DTESB");
            lstSheetName.Add("DTESBDetail");
            string FileName = string.Format(@"ESBDataVaild_{0}.xls",DateTime.Now.ToString("yyyyMMddHHmmssffff"));
            string PathN = Path.Combine(ESBSetting.SaveOutPutPath, FileName);
            if (CommonUnit.ExportMultiSheetToExcelFile(lstDGV, ref PathN, false, false, true, true, lstSheetName))
                MessageBox.Show("匯出Excel成功!");
            else
                MessageBox.Show("匯出Excel失敗!");

            foreach (DataGridView dgv in lstDGV)
            {
                this.Controls.Remove(dgv);
            }
        }

    }
}
