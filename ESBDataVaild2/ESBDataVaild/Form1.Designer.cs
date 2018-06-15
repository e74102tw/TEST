namespace ESBDataVaild
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbxADDParameter = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxFunc = new System.Windows.Forms.TextBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.tbxInputNo = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCompare = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCompareDetail = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbxInputType = new System.Windows.Forms.GroupBox();
            this.rbtnCard = new System.Windows.Forms.RadioButton();
            this.rbtnID = new System.Windows.Forms.RadioButton();
            this.dTCompareBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dSCompare = new ESBDataVaild.DSCompare();
            this.dsCompare1 = new ESBDataVaild.DSCompare();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompare)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompareDetail)).BeginInit();
            this.panel3.SuspendLayout();
            this.gbxInputType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dTCompareBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCompare1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbxInputType);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.tbxInputNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 757);
            this.panel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbxADDParameter);
            this.groupBox4.Location = new System.Drawing.Point(3, 73);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(242, 55);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ADDParameter:";
            // 
            // tbxADDParameter
            // 
            this.tbxADDParameter.Location = new System.Drawing.Point(6, 21);
            this.tbxADDParameter.Name = "tbxADDParameter";
            this.tbxADDParameter.Size = new System.Drawing.Size(230, 22);
            this.tbxADDParameter.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxFunc);
            this.groupBox3.Location = new System.Drawing.Point(3, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 55);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Function:";
            // 
            // tbxFunc
            // 
            this.tbxFunc.Location = new System.Drawing.Point(6, 18);
            this.tbxFunc.Name = "tbxFunc";
            this.tbxFunc.Size = new System.Drawing.Size(230, 22);
            this.tbxFunc.TabIndex = 1;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(13, 706);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(229, 37);
            this.btnExportExcel.TabIndex = 6;
            this.btnExportExcel.Text = "匯出EXCEL";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 620);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(230, 37);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 577);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(230, 37);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "比較";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 663);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(230, 37);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "匯出比較表";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tbxInputNo
            // 
            this.tbxInputNo.Location = new System.Drawing.Point(6, 183);
            this.tbxInputNo.Multiline = true;
            this.tbxInputNo.Name = "tbxInputNo";
            this.tbxInputNo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxInputNo.Size = new System.Drawing.Size(236, 371);
            this.tbxInputNo.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(248, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1194, 757);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1194, 617);
            this.panel4.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvCompare);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1194, 617);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "比對總表";
            // 
            // dgvCompare
            // 
            this.dgvCompare.AllowUserToAddRows = false;
            this.dgvCompare.AllowUserToDeleteRows = false;
            this.dgvCompare.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCompare.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompare.Location = new System.Drawing.Point(3, 18);
            this.dgvCompare.MultiSelect = false;
            this.dgvCompare.Name = "dgvCompare";
            this.dgvCompare.ReadOnly = true;
            this.dgvCompare.RowTemplate.Height = 24;
            this.dgvCompare.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompare.ShowCellErrors = false;
            this.dgvCompare.ShowEditingIcon = false;
            this.dgvCompare.Size = new System.Drawing.Size(1188, 596);
            this.dgvCompare.TabIndex = 0;
            this.dgvCompare.SelectionChanged += new System.EventHandler(this.dgvCompare_SelectionChanged);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.ForestGreen;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 617);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1194, 10);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 627);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1194, 110);
            this.panel5.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCompareDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1194, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "比對明細";
            // 
            // dgvCompareDetail
            // 
            this.dgvCompareDetail.AllowUserToAddRows = false;
            this.dgvCompareDetail.AllowUserToDeleteRows = false;
            this.dgvCompareDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCompareDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompareDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompareDetail.Location = new System.Drawing.Point(3, 18);
            this.dgvCompareDetail.MultiSelect = false;
            this.dgvCompareDetail.Name = "dgvCompareDetail";
            this.dgvCompareDetail.ReadOnly = true;
            this.dgvCompareDetail.RowTemplate.Height = 24;
            this.dgvCompareDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompareDetail.ShowCellErrors = false;
            this.dgvCompareDetail.ShowEditingIcon = false;
            this.dgvCompareDetail.Size = new System.Drawing.Size(1188, 89);
            this.dgvCompareDetail.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 737);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1194, 20);
            this.panel3.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1194, 20);
            this.progressBar1.TabIndex = 5;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // gbxInputType
            // 
            this.gbxInputType.Controls.Add(this.rbtnID);
            this.gbxInputType.Controls.Add(this.rbtnCard);
            this.gbxInputType.Location = new System.Drawing.Point(3, 134);
            this.gbxInputType.Name = "gbxInputType";
            this.gbxInputType.Size = new System.Drawing.Size(242, 43);
            this.gbxInputType.TabIndex = 11;
            this.gbxInputType.TabStop = false;
            this.gbxInputType.Text = "InputType:";
            // 
            // rbtnCard
            // 
            this.rbtnCard.AutoSize = true;
            this.rbtnCard.Checked = true;
            this.rbtnCard.Location = new System.Drawing.Point(10, 22);
            this.rbtnCard.Name = "rbtnCard";
            this.rbtnCard.Size = new System.Drawing.Size(71, 16);
            this.rbtnCard.TabIndex = 0;
            this.rbtnCard.Text = "CARDNO";
            this.rbtnCard.UseVisualStyleBackColor = true;
            // 
            // rbtnID
            // 
            this.rbtnID.AutoSize = true;
            this.rbtnID.Location = new System.Drawing.Point(87, 22);
            this.rbtnID.Name = "rbtnID";
            this.rbtnID.Size = new System.Drawing.Size(51, 16);
            this.rbtnID.TabIndex = 1;
            this.rbtnID.Text = "IDNO";
            this.rbtnID.UseVisualStyleBackColor = true;
            // 
            // dTCompareBindingSource
            // 
            this.dTCompareBindingSource.DataMember = "DTCompare";
            this.dTCompareBindingSource.DataSource = this.dSCompare;
            // 
            // dSCompare
            // 
            this.dSCompare.DataSetName = "DSCompare";
            this.dSCompare.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsCompare1
            // 
            this.dsCompare1.DataSetName = "DSCompare";
            this.dsCompare1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 757);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MLI 資料驗證";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompare)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompareDetail)).EndInit();
            this.panel3.ResumeLayout(false);
            this.gbxInputType.ResumeLayout(false);
            this.gbxInputType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dTCompareBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dSCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCompare1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox tbxInputNo;
        private System.Windows.Forms.TextBox tbxFunc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvCompare;
        private DSCompare dSCompare;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.BindingSource dTCompareBindingSource;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DSCompare dsCompare1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCompareDetail;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.TextBox tbxADDParameter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbxInputType;
        private System.Windows.Forms.RadioButton rbtnID;
        private System.Windows.Forms.RadioButton rbtnCard;
    }
}

