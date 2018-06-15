using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data.OleDb;
using System.IO;

namespace ESBDataVaild
{
    class CommonUnit
    {
        public static bool IsAVRECSID(string id)
        {
            if (id.Length >= 3)
            {
                string IdList = "WAVES23;WAVET24;WAVDW01;WAVDX02;WAVDY03;WAVDZ04;WAVEA05;WAVEB06;WAVEU25;WAVEV26;WAVEE09;WAVEF10;WAVEG11;WAVEH12;WAVEI13;WAVEJ14;WAVAO15;WAVAP16;WAVAQ17;WAVAR18;WAVBJ36;WAVBK37;WAVAA01;WAVAB02;WAVDU99;WAVCO67;WAVCP68;WAVCQ69;WAVCR70;WAVCS71;WAVCT72;WAVCU73;WAVCV74;WAVCW75;WAVCX76;WAVCY77;WAVCZ78;WAVDA79;WAVDB80;WAVDC81;WAVDD82;WAVBC29;WAVBD30;WAVBE31;WAVBI35;WAVBQ43;WAVBR44;WAVBS45;WAVBT46;WAVBU47;WAVCG59;WAVCH60;WAVCI61;WAVCJ62;WAVCK63;WAVCL64;WAVCM65;WAVCN66;WAVEW17;WAVEX28;WAVEM17;WAVEN18;WAVEO19;WAVEP20;WAVEQ21;WAVER22;WAVAS19;WAVAT20;WAVBA27;WAVBB28;WAVBV48;WAVBW49;WAVBX50;WAVBO41;WAVBP42;WAVEC07;WAVAV22;WAVBG33;WAVBH34;WAVAC03;WAVAD04;WAVAE05;WAVAF06;WAVAW23;WAVAX24;WAVAG07;WAVAH08;WAVAI09;WAVAJ10;WAVAK11;WAVAL12;WAVAY25;WAVAZ26;WAVAM13;WAVAN14;TWCCTI19;WAVEL16;WAVED08;WAVDV00;WAVAU21;TWCCTI01;TWCCTI02;TWCCTI03;TWCCTI04;TWCCTI05;TWCCTI06;TWCCTI07;TWCCTI08;TWCCTI09;TWCCTI10;TWCCTI11;TWCCTI12;TWCCTI13;TWCCTI14;TWCCTI15;TWCCTI16;TWCCTI17;TWCCTI18;TWCCTI19;TWCCTI20;TWCCTI21;TWCCTI22;TWCCTI23;TWCCTI24;TWCCTI25;TWCCTI26;TWCCTI27;TWCCTI28;TWCCTI29;TWCCTI30;TWCCTI31;TWCCTI32;TWCCTI33;TWCCTI34;TWCCTI35;TWCCTI36;TWCCTI37;TWCCTI38;TWCCTI39;TWCCTI40;WAVEK15;WAVDE83;WAVDG85;WAVDH86;WAVDI87;WAVDJ88;WAVDK89;WAVDL90;WAVDM91;WAVDN92;WAVDO93;WAVDP94;WAVDQ95;WAVDR96;WAVDS97;WAVDT98;WAVBF32;WAVDF84;WAVBL38;WAVBM39;WAVBN40;TWCCTI01;TWCCTI02;TWCCTI03;TWCCTI04;TWCCTI05;TWCCTI06;TWCCTI07;TWCCTI08;TWCCTI09;TWCCTI10;TWCCTI11;TWCCTI12;TWCCTI13;TWCCTI14;TWCCTI15;TWCCTI16;TWCCTI17;TWCCTI18;TWCCTI20;TWCCTI21;TWCCTI22;TWCCTI23;TWCCTI24;TWCCTI25;TWCCTI26;TWCCTI27;TWCCTI28;TWCCTI29;TWCCTI30;TWCCTI31;TWCCTI32;TWCCTI33;TWCCTI34;TWCCTI35;TWCCTI36;TWCCTI37;TWCCTI38;TWCCTI39;TWCCTI40;";
                return IdList.IndexOf(id) >= 0;
            }
            else
            {
                return false;
            }
        }

        public static DataTable SelectTable(DataTable SrcTable, string sfilter)
        {
            return SelectTable(SrcTable, sfilter, "");
        }

        public static DataTable SelectTable(DataTable SrcTable, string sfilter, string sSort)
        {
            DataTable table = SrcTable.Clone();
            table.Rows.Clear();

            DataRow[] rows = SrcTable.Select(sfilter, sSort);
            foreach (DataRow row in rows)
            {
                object[] ary_obj = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    ary_obj[i] = row[i];
                } // for

                table.Rows.Add(ary_obj);
            } // foreach

            return table;
        }

        public static string ParamStr(string str)
        {
            return str.Replace("'", "''");
        }

        public static string RightStr(string s, int len)
        {
            if (s.Length <= len)
                return s;

            return s.Substring(s.Length - len, len);

        }

        public static string LeftStr(string s, int len)
        {
            if (s.Length <= len)
                return s;

            return s.Substring(0, len);

        }

        public static string MidStr(string s, int index, int len)
        {
            if ((index < 1) || (index > s.Length))
                return "";
            else if ((index + len - 1) > s.Length)
                return s.Substring(index - 1, s.Length - index + 1);

            return s.Substring(index - 1, len);
        }

        public static int StrLenB(string s)
        {
            return System.Text.Encoding.Default.GetBytes(s).Length;
        }

        public static string MidStrB(string s, int index, int len)
        {
            int nStrLen = StrLenB(s);

            if ((index < 1) || (index > nStrLen))
                return "";
            else if ((index + len - 1) > nStrLen)
                return System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(s), index - 1, nStrLen - index + 1);

            return System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(s), index - 1, len);
        }

        public static double ToABS(double n)
        {
            return double.Parse(Math.Abs(decimal.Parse(n.ToString())).ToString());
        }

        public static Decimal ToDecimal(object dr)
        {
            if (IsDBNull(dr))
                return 0;
            else if (dr.ToString() == "")
                return 0;
            return Decimal.Parse(dr.ToString());
        }

        public static bool IsDBNull(object dr)
        {
            return (dr is System.DBNull);
        }
        public static bool IsNotDBNull(object dr)
        {
            return !(dr is System.DBNull);
        }

        public static double ToDouble(object dr)
        {
            if (IsDBNull(dr))
                return 0;
            else if (dr.ToString() == "")
                return 0;
            return double.Parse(dr.ToString());
        }

        public static string DBDateTimeToString(object dr, string sFormat)
        {
            if (IsDBNull(dr))
                return "";
            return ((DateTime)dr).ToString(sFormat);
        }

        public static int ToInt(object dr)
        {
            //bool b = dr as bool;

            if (IsDBNull(dr))
                return 0;
            else if (dr.GetType() != typeof(bool))
                return int.Parse(dr.ToString());
            else
                return (bool.Parse(dr.ToString()) ? 1 : 0);
        }

        public static double RoundEx(double n)
        {
            return RoundEx(n, 0);
        }
        public static double RoundEx(double n, int d)
        {
            string fmt;

            fmt = (d > 0 ? "0." : "0");

            while (d > 0)
            {
                fmt = fmt + "#";
                d = d - 1;
            } // while

            return double.Parse(n.ToString(fmt));
        }

        public static bool ExportToCSVFile(DataGridView dbgResult)
        {
            return ExportToCSVFile(dbgResult, true);

        }

        public static bool ExportToCSVFile(DataGridView dbgResult, bool bForceStr)
        {
            return ExportToCSVFile(dbgResult, true, true, "", "");

        }

        /// <summary>
        /// 匯出 DataGridView 到 CSV 檔案
        /// </summary>
        /// <param name="dbgResult">DataGridView</param>
        /// <param name="bForceStr">是否強制所有的欄位值都使用 =("Value") 的格式匯出</param>
        /// <param name="bExportColName">是否匯出欄位名稱</param>
        /// <param name="sDefFilePath">預設檔案路徑, 空字串表示不指定預設檔案路徑</param>
        /// <param name="sDefFileName">預設檔名, 空字串表示不指定預設檔名</param>
        public static bool ExportToCSVFile(DataGridView dbgResult, bool bForceStr, bool bExportColName, string sDefFilePath, string sDefFileName)
        {
            string sExportFileName = "";
            string sLineStr = "";
            SaveFileDialog dlgSaveFile = new SaveFileDialog();

            if (Directory.Exists(sDefFilePath))
                dlgSaveFile.InitialDirectory = sDefFilePath;
            else
                dlgSaveFile.InitialDirectory = Application.StartupPath;

            if (sDefFileName.Length > 0)
                dlgSaveFile.FileName = sDefFileName;

            dlgSaveFile.DefaultExt = ".csv";
            dlgSaveFile.Filter = "CSV File (*.csv)|*.csv";
            dlgSaveFile.FilterIndex = 0;
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                sExportFileName = dlgSaveFile.FileName;
                try
                {
                    if (File.Exists(sExportFileName))
                    {
                        File.Delete(sExportFileName);
                    } //if

                    StreamWriter sw = new StreamWriter(sExportFileName, false, Encoding.GetEncoding(950));

                    if (bExportColName)
                    {
                        sLineStr = "";
                        foreach (DataGridViewColumn col in dbgResult.Columns)
                        {
                            sLineStr += (sLineStr == "" ? "" : ",") + "=(\"" + col.HeaderText.Trim() + "\")";
                        } // foreach
                        sw.WriteLine(sLineStr);
                        sw.Flush();
                    } // if

                    foreach (DataGridViewRow grid_row in dbgResult.Rows)
                    {
                        DataRow row = ((DataRowView)grid_row.DataBoundItem).Row;
                        sLineStr = "";
                        int colIdx = 0;
                        foreach (DataGridViewColumn col in dbgResult.Columns)
                        {
                            if (bForceStr)
                                sLineStr += (colIdx == 0 ? "" : ",") + "=(\"" + row[col.DataPropertyName].ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Trim() + "\")";
                            else
                                sLineStr += (colIdx == 0 ? "" : ",") + row[col.DataPropertyName].ToString().Replace(",", " ").Replace("\r", "").Replace("\n", " ").Trim();

                            colIdx++;
                        } // foreach
                        sw.WriteLine(sLineStr);
                        sw.Flush();
                    } // foreach

                    sw.Close();

                    MessageBox.Show("資料已匯出完成！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                } // try
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                } // catch
            } // if
            return false;
        }

        public static bool ExportToExcelFile(DataGridView dbgResult, ref string sExportFileName, bool IsShowOK,
                bool IsShowDialog, bool IsDeleteFile, bool IsCopyFromTemplate, string WorkSheetName, string TemplateName)
        {

            SaveFileDialog dlgSaveFile = new SaveFileDialog();
            string sTempExcelFile = Application.StartupPath + "\\" + TemplateName;
            string sql = "";
            string connstr = "";
            System.Data.OleDb.OleDbConnection conn = null;
            System.Data.OleDb.OleDbCommand cmd = null;

            dlgSaveFile.InitialDirectory = Application.StartupPath;
            dlgSaveFile.DefaultExt = ".xls";
            dlgSaveFile.Filter = "Excel File (*.xls) | *.xls";
            dlgSaveFile.FilterIndex = 0;
            if (!IsShowDialog || dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                if (IsShowDialog)
                    sExportFileName = dlgSaveFile.FileName;

                try
                {
                    if (IsDeleteFile && File.Exists(sExportFileName))
                    {
                        File.Delete(sExportFileName);
                    } //if

                    if (IsCopyFromTemplate) File.Copy(sTempExcelFile, sExportFileName, true);
                    connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExportFileName +
                        ";Extended Properties=\"Excel 8.0;HDR=Yes\";";
                    conn = new System.Data.OleDb.OleDbConnection(connstr);
                    cmd = new System.Data.OleDb.OleDbCommand();

                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    //sql = "Drop Table [Report$];";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();

                    sql = "";
                    //foreach (DataGridViewColumn grid_col in dbgResult.Columns)
                    //{
                    //    if (!grid_col.Visible) continue;
                    //    sql += (sql.Length > 0 ? ", " : "Create Table [" + WorkSheetName + "] ( ") +
                    //        "[" + grid_col.HeaderText.Replace('.', '#') + "]";
                    //    switch (grid_col.ValueType.Name)
                    //    {
                    //        case "Int":
                    //        case "Int32":
                    //            sql += " Integer";
                    //            break;
                    //        case "Decimal":
                    //        case "Single":
                    //            sql += " Decimal";
                    //            break;
                    //        case "DateTime":
                    //            sql += " DateTime";
                    //            break;
                    //        default:
                    //            sql += " Text";
                    //            break;
                    //    } // switch
                    //} // foreach
                    //sql += " );";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();

                    //sql = "Delete From [Report$] where PID='';";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();


                    foreach (DataGridViewRow grid_row in dbgResult.Rows)
                    {
                        DataRowView rowView = grid_row.DataBoundItem as DataRowView;
                        if (rowView == null)
                            continue;

                        DataRow row = rowView.Row;
                        string sql_col_List = "", sql_value_List = "";
                        foreach (DataGridViewColumn grid_col in dbgResult.Columns)
                        {
                            if (!grid_col.Visible) continue;
                            sql_col_List += (sql_col_List.Length > 0 ? "," : "") +
                                "[" + grid_col.HeaderText.Replace('.', '#') + "]";
                            switch (grid_col.ValueType.Name)
                            {
                                case "Int":
                                case "Int32":
                                    sql_value_List += (sql_value_List.Length > 0 ? "," : "") +
                                        ParamStr(CommonUnit.ToInt(row[grid_col.DataPropertyName]).ToString());
                                    break;
                                case "Decimal":
                                case "Single":
                                    sql_value_List += (sql_value_List.Length > 0 ? "," : "") +
                                        ParamStr(CommonUnit.ToDecimal(row[grid_col.DataPropertyName]).ToString());
                                    break;
                                case "DateTime":
                                    if (row[grid_col.DataPropertyName].Equals(DBNull.Value))
                                        sql_value_List += (sql_value_List.Length > 0 ? "," : "") + " Null ";
                                    else
                                        sql_value_List += (sql_value_List.Length > 0 ? "," : "") + "'" +
                                            ((DateTime)row[grid_col.DataPropertyName]).ToString("yyyy/MM/dd HH:mm:ss") + "'";
                                    break;
                                default:
                                    sql_value_List += (sql_value_List.Length > 0 ? "," : "") + "'" +
                                        ParamStr(row[grid_col.DataPropertyName].ToString()) + "'";
                                    break;
                            } // switch
                        } // foreach
                        sql = "Insert Into [" + WorkSheetName + "] (" + sql_col_List + ") Values(" +
                            sql_value_List + ")";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    } // foreach

                    //sql = "Alter Table [Sheet1] Add Column ABC Text;";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();


                    //sql = "Drop Table [Report$];";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();

                    //sql = "Create Table [Sheet1$] (abc text);";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();


                    conn.Close();

                    if (IsShowOK)
                        MessageBox.Show("資料已匯出完成！", "完成", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    return true;
                } // try
                catch (Exception E)
                {
                    if (IsShowDialog)
                        MessageBox.Show(E.Message);

                    if (conn != null && conn.State == ConnectionState.Open)
                        conn.Close();

                    if (!IsShowDialog)
                        throw;
                } // catch
            } // if
            return false;
        }

        #region 匯出多個sheet
        //20140606 by Jack
        public static bool ExportMultiSheetToExcelFile(List<DataGridView> dbgResult, List<string> WorkSheetName)
        {
            string sExportFileName = "";
            return ExportMultiSheetToExcelFile(dbgResult, ref sExportFileName, true, true, true, false, WorkSheetName);
        }
        #endregion

        #region 匯出多個sheet
        //20140606 by Jack
        public static bool ExportMultiSheetToExcelFile(List<DataGridView> dbgResultAll, ref string sExportFileName, bool IsShowOK,
        bool IsShowDialog, bool IsDeleteFile, bool IsCopyFromTemplate, List<string> WorkSheetName)
        {

            SaveFileDialog dlgSaveFile = new SaveFileDialog();
            string sTempExcelFile = Application.StartupPath + "\\ReportTemplate.xl_";
            string sql = "";
            string connstr = "";
            System.Data.OleDb.OleDbConnection conn = null;
            System.Data.OleDb.OleDbCommand cmd = null;

            dlgSaveFile.InitialDirectory = Application.StartupPath;
            dlgSaveFile.DefaultExt = ".xls";
            dlgSaveFile.Filter = "Excel File (*.xls) | *.xls";
            dlgSaveFile.FilterIndex = 0;
            if (!IsShowDialog || dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                if (IsShowDialog)
                    sExportFileName = dlgSaveFile.FileName;

                try
                {
                    if (IsDeleteFile && File.Exists(sExportFileName))
                    {
                        File.Delete(sExportFileName);
                    } //if

                    if (IsCopyFromTemplate) File.Copy(sTempExcelFile, sExportFileName, true);
                    connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sExportFileName +
                        ";Extended Properties=\"Excel 8.0;HDR=Yes\";";
                    conn = new System.Data.OleDb.OleDbConnection(connstr);
                    cmd = new System.Data.OleDb.OleDbCommand();

                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    //sql = "Drop Table [Report$];";
                    //cmd.CommandText = sql;
                    //cmd.ExecuteNonQuery();

                    foreach (DataGridView dbgResult in dbgResultAll)
                    {
                        sql = "";
                        foreach (DataGridViewColumn grid_col in dbgResult.Columns)
                        {
                            if (!grid_col.Visible) continue;
                            sql += (sql.Length > 0 ? ", " : "Create Table [" + WorkSheetName[dbgResultAll.IndexOf(dbgResult)] + "] ( ") +
                                "[" + grid_col.HeaderText.Replace('.', '#') + "]";
                            switch (grid_col.ValueType.Name)
                            {
                                case "Int":
                                case "Int32":
                                    sql += " Integer";
                                    break;
                                case "Decimal":
                                case "Single":
                                    sql += " Decimal";
                                    break;
                                case "DateTime":
                                    sql += " DateTime";
                                    break;
                                default:
                                    sql += " Text";
                                    break;
                            } // switch
                        } // foreach
                        sql += " );";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        foreach (DataGridViewRow grid_row in dbgResult.Rows)
                        {
                            DataRowView rowView = grid_row.DataBoundItem as DataRowView;
                            if (rowView == null)
                                continue;

                            DataRow row = rowView.Row;
                            string sql_col_List = "", sql_value_List = "";
                            foreach (DataGridViewColumn grid_col in dbgResult.Columns)
                            {
                                if (!grid_col.Visible) continue;
                                sql_col_List += (sql_col_List.Length > 0 ? "," : "") +
                                    "[" + grid_col.HeaderText.Replace('.', '#') + "]";
                                switch (grid_col.ValueType.Name)
                                {
                                    case "Int":
                                    case "Int32":
                                        sql_value_List += (sql_value_List.Length > 0 ? "," : "") +
                                            ParamStr(CommonUnit.ToInt(row[grid_col.DataPropertyName]).ToString());
                                        break;
                                    case "Decimal":
                                    case "Single":
                                        sql_value_List += (sql_value_List.Length > 0 ? "," : "") +
                                            ParamStr(CommonUnit.ToDecimal(row[grid_col.DataPropertyName]).ToString());
                                        break;
                                    case "DateTime":
                                        if (row[grid_col.DataPropertyName].Equals(DBNull.Value))
                                            sql_value_List += (sql_value_List.Length > 0 ? "," : "") + " Null ";
                                        else
                                            sql_value_List += (sql_value_List.Length > 0 ? "," : "") + "'" +
                                                ((DateTime)row[grid_col.DataPropertyName]).ToString("yyyy/MM/dd HH:mm:ss") + "'";
                                        break;
                                    default:
                                        sql_value_List += (sql_value_List.Length > 0 ? "," : "") + "'" +
                                            ParamStr(row[grid_col.DataPropertyName].ToString()) + "'";
                                        break;
                                } // switch
                            } // foreach
                            sql = "Insert Into [" + WorkSheetName[dbgResultAll.IndexOf(dbgResult)] + "] (" + sql_col_List + ") Values(" +
                                sql_value_List + ")";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        } // foreach
                    }// foreach

                    conn.Close();

                    if (IsShowOK)
                        MessageBox.Show("資料已匯出完成！", "完成", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    return true;
                } // try
                catch (Exception E)
                {
                    if (IsShowDialog)
                        MessageBox.Show(E.Message);

                    if (conn != null && conn.State == ConnectionState.Open)
                        conn.Close();

                    if (!IsShowDialog)
                        throw;
                } // catch
            } // if
            return false;
        }
        #endregion

        // InputBox for single line text
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            return InputBox(title, promptText, ref value, new Size(404, 134), FormBorderStyle.FixedDialog);
        }

        // InputBox for multi-line text (show editor box)
        public static DialogResult InputBox(string title, string promptText, ref string value, Size size, FormBorderStyle formStyle)
        {
            TextBox textBox = new TextBox();
            Form form = CreateInputBoxForm(title, promptText, value, size, formStyle, ref textBox);
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        // custom input box
        public static Form CreateInputBoxForm(string title, string promptText, string value, Size size, FormBorderStyle style,
            ref TextBox textBox)
        {
            Form form = new Form();
            Label label = new Label();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            form.Size = size;
            label.Text = promptText;
            textBox.Text = value;

            if (size.Height > 134)
            {
                textBox.Multiline = true;
                textBox.ScrollBars = ScrollBars.Both;
            }

            buttonOk.Text = "確定";
            buttonCancel.Text = "取消";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 4, form.ClientSize.Width - 24, 13);
            textBox.SetBounds(12, 20, form.ClientSize.Width - 24, form.ClientSize.Height - 49);
            buttonOk.SetBounds(form.ClientSize.Width - 168, textBox.Bottom + 16, 75, 23);
            buttonCancel.SetBounds(form.ClientSize.Width - 87, textBox.Bottom + 16, 75, 23);

            label.AutoSize = true;


            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(form.ClientSize.Width, label.Right + 10),
                buttonOk.Bottom + 10);
            form.FormBorderStyle = style;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            textBox.Anchor = textBox.Anchor | AnchorStyles.Right | AnchorStyles.Bottom;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            return form;
        }

        public static bool ImportExcel(out DataSet dsExcel, List<string> lsWorkSheetNameList)
        {
            for (int i = 0; i < lsWorkSheetNameList.Count; i++)
            {
                lsWorkSheetNameList[i] = lsWorkSheetNameList[i].ToUpper() + "$";
            } // for

            dsExcel = new DataSet();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "請選擇 Excel 檔案";
            dialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';",
                dialog.FileName);

            DataTable worksheets = null;
            using (OleDbConnection connection =
                new OleDbConnection(connectionString))
            {
                connection.Open();
                worksheets = connection.GetSchema("Tables");
                if (worksheets.Rows.Count == 0)
                    throw new Exception("Excel 檔案中找不到可用的工作表!");

                foreach (DataRow row in worksheets.Rows)
                {
                    string sTableName = row["TABLE_NAME"].ToString();
                    if (lsWorkSheetNameList.Contains(sTableName.ToUpper()))
                    {
                        string queryString = "SELECT * FROM [" + sTableName + "]";

                        DataTable excelTable = dsExcel.Tables.Add(sTableName.Replace("$", ""));

                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = new OleDbCommand(
                            queryString, connection);
                        adapter.Fill(excelTable);

                    } // if
                } // foreach
            }

            return true;
        }

        public static bool ImportExcel(out DataTable excelTable)
        {
            return ImportExcel(out excelTable, null);
        }

        public static bool ImportExcel(out DataTable excelTable, string worksheetName)
        {
            excelTable = new DataTable();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "請選擇 Excel 檔案";
            dialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';",
                dialog.FileName);

            // Get First worksheets
            if (worksheetName == null)
            {
                DataTable worksheets = null;
                using (OleDbConnection connection =
                    new OleDbConnection(connectionString))
                {
                    connection.Open();
                    worksheets = connection.GetSchema("Tables");
                    if (worksheets.Rows.Count == 0)
                        throw new Exception("Excel 檔案中找不到可用的工作表!");

                    worksheetName = worksheets.Rows[0]["TABLE_NAME"].ToString();
                }
            }

            string queryString = "SELECT * FROM [" + worksheetName + "]";

            using (OleDbConnection connection =
                new OleDbConnection(connectionString))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = new OleDbCommand(
                    queryString, connection);
                adapter.Fill(excelTable);
            }
            return true;
        }
    }
}
