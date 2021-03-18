namespace Tools
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ProjectSelectForm = new System.Windows.Forms.TabPage();
            this.ExcelExportForm = new System.Windows.Forms.TabPage();
            this.ProtoExportForm = new System.Windows.Forms.TabPage();
            this.DBExportForm = new System.Windows.Forms.TabPage();
            this.CDKeyForm = new System.Windows.Forms.TabPage();
            this.GMExportForm = new System.Windows.Forms.TabPage();
            this.LogExportForm = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.CleanAllBtn = new System.Windows.Forms.Button();
            this.CheckLog = new System.Windows.Forms.CheckBox();
            this.CheckLogWarning = new System.Windows.Forms.CheckBox();
            this.CheckLogError = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.ProjectSelectForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.ProjectSelectForm);
            this.tabControl.Controls.Add(this.ExcelExportForm);
            this.tabControl.Controls.Add(this.ProtoExportForm);
            this.tabControl.Controls.Add(this.DBExportForm);
            this.tabControl.Controls.Add(this.CDKeyForm);
            this.tabControl.Controls.Add(this.GMExportForm);
            this.tabControl.Controls.Add(this.LogExportForm);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 380);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // ProjectSelectForm
            // 

            this.ProjectSelectForm.Location = new System.Drawing.Point(4, 22);
            this.ProjectSelectForm.Name = "ProjectSelectForm";
            this.ProjectSelectForm.Padding = new System.Windows.Forms.Padding(3);
            this.ProjectSelectForm.Size = new System.Drawing.Size(752, 354);
            this.ProjectSelectForm.TabIndex = 0;
            this.ProjectSelectForm.Text = "项目选择";
            this.ProjectSelectForm.UseVisualStyleBackColor = true;
            // 
            // ExcelExportForm
            // 
            this.ExcelExportForm.Location = new System.Drawing.Point(4, 22);
            this.ExcelExportForm.Name = "ExcelExportForm";
            this.ExcelExportForm.Padding = new System.Windows.Forms.Padding(3);
            this.ExcelExportForm.Size = new System.Drawing.Size(752, 354);
            this.ExcelExportForm.TabIndex = 1;
            this.ExcelExportForm.Text = "Excel导出";
            this.ExcelExportForm.UseVisualStyleBackColor = true;
            // 
            // ProtoExportForm
            // 
            this.ProtoExportForm.Location = new System.Drawing.Point(4, 22);
            this.ProtoExportForm.Name = "ProtoExportForm";
            this.ProtoExportForm.Padding = new System.Windows.Forms.Padding(3);
            this.ProtoExportForm.Size = new System.Drawing.Size(752, 354);
            this.ProtoExportForm.TabIndex = 2;
            this.ProtoExportForm.Text = "Proto导出";
            this.ProtoExportForm.UseVisualStyleBackColor = true;
            // 
            // DBExportForm
            // 
            this.DBExportForm.Location = new System.Drawing.Point(4, 22);
            this.DBExportForm.Name = "DBExportForm";
            this.DBExportForm.Padding = new System.Windows.Forms.Padding(3);
            this.DBExportForm.Size = new System.Drawing.Size(752, 354);
            this.DBExportForm.TabIndex = 3;
            this.DBExportForm.Text = "数据库结构导出";
            this.DBExportForm.UseVisualStyleBackColor = true;
            // 
            // CDKeyForm
            // 
            this.CDKeyForm.Location = new System.Drawing.Point(4, 22);
            this.CDKeyForm.Name = "CDKeyForm";
            this.CDKeyForm.Padding = new System.Windows.Forms.Padding(3);
            this.CDKeyForm.Size = new System.Drawing.Size(752, 354);
            this.CDKeyForm.TabIndex = 4;
            this.CDKeyForm.Text = "CDKey生成";
            this.CDKeyForm.UseVisualStyleBackColor = true;
            // 
            // GMExportForm
            // 
            this.GMExportForm.Location = new System.Drawing.Point(4, 22);
            this.GMExportForm.Name = "GMExportForm";
            this.GMExportForm.Padding = new System.Windows.Forms.Padding(3);
            this.GMExportForm.Size = new System.Drawing.Size(752, 354);
            this.GMExportForm.TabIndex = 5;
            this.GMExportForm.Text = "GM相关导出";
            this.GMExportForm.UseVisualStyleBackColor = true;
            // 
            // LogExportForm
            // 
            this.LogExportForm.Location = new System.Drawing.Point(4, 22);
            this.LogExportForm.Name = "LogExportForm";
            this.LogExportForm.Padding = new System.Windows.Forms.Padding(3);
            this.LogExportForm.Size = new System.Drawing.Size(752, 354);
            this.LogExportForm.TabIndex = 6;
            this.LogExportForm.Text = "服务器日志结构";
            this.LogExportForm.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(12, 423);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(760, 126);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // CleanAllBtn
            // 
            this.CleanAllBtn.Location = new System.Drawing.Point(12, 394);
            this.CleanAllBtn.Name = "CleanAllBtn";
            this.CleanAllBtn.Size = new System.Drawing.Size(76, 23);
            this.CleanAllBtn.TabIndex = 2;
            this.CleanAllBtn.Text = "全部清除";
            this.CleanAllBtn.UseVisualStyleBackColor = true;
            this.CleanAllBtn.Click += new System.EventHandler(this.CleanAllBtn_Click);
            // 
            // CheckLog
            // 
            this.CheckLog.AutoSize = true;
            this.CheckLog.Location = new System.Drawing.Point(107, 398);
            this.CheckLog.Name = "CheckLog";
            this.CheckLog.Size = new System.Drawing.Size(72, 16);
            this.CheckLog.TabIndex = 3;
            this.CheckLog.Text = "普通日志";
            this.CheckLog.UseVisualStyleBackColor = true;
            this.CheckLog.Click += new System.EventHandler(this.CheckLog_Click);
            // 
            // CheckLogWarning
            // 
            this.CheckLogWarning.AutoSize = true;
            this.CheckLogWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.CheckLogWarning.Location = new System.Drawing.Point(191, 398);
            this.CheckLogWarning.Name = "CheckLogWarning";
            this.CheckLogWarning.Size = new System.Drawing.Size(72, 16);
            this.CheckLogWarning.TabIndex = 4;
            this.CheckLogWarning.Text = "警告日志";
            this.CheckLogWarning.UseVisualStyleBackColor = true;
            this.CheckLogWarning.Click += new System.EventHandler(this.CheckLog_Click);
            // 
            // CheckLogError
            // 
            this.CheckLogError.AutoSize = true;
            this.CheckLogError.ForeColor = System.Drawing.Color.Red;
            this.CheckLogError.Location = new System.Drawing.Point(275, 398);
            this.CheckLogError.Name = "CheckLogError";
            this.CheckLogError.Size = new System.Drawing.Size(72, 16);
            this.CheckLogError.TabIndex = 5;
            this.CheckLogError.Text = "错误日志";
            this.CheckLogError.UseVisualStyleBackColor = true;
            this.CheckLogError.Click += new System.EventHandler(this.CheckLog_Click);
      
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.CheckLogError);
            this.Controls.Add(this.CheckLogWarning);
            this.Controls.Add(this.CheckLog);
            this.Controls.Add(this.CleanAllBtn);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.tabControl);
            this.Name = "Main";
            this.Text = "游戏工具";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl.ResumeLayout(false);
            this.ProjectSelectForm.ResumeLayout(false);
            this.ProjectSelectForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ProjectSelectForm;
        private System.Windows.Forms.TabPage ExcelExportForm;
        private System.Windows.Forms.TabPage ProtoExportForm;
        private System.Windows.Forms.TabPage DBExportForm;
        private System.Windows.Forms.TabPage CDKeyForm;
        private System.Windows.Forms.TabPage GMExportForm;
        private System.Windows.Forms.TabPage LogExportForm;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button CleanAllBtn;
        private System.Windows.Forms.CheckBox CheckLog;
        private System.Windows.Forms.CheckBox CheckLogWarning;
        private System.Windows.Forms.CheckBox CheckLogError;
    }
}

