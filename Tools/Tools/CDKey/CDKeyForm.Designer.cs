﻿namespace Tools
{
    partial class CDKeyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectCDKeyExcelPath = new System.Windows.Forms.Button();
            this.listCDKey = new System.Windows.Forms.ListBox();
            this.labName = new System.Windows.Forms.Label();
            this.labUseCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labId = new System.Windows.Forms.Label();
            this.labNum = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labEndTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labDes = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtCDKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCDKeyResult = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCheckCDKey = new System.Windows.Forms.Button();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.txtCDKeyExcelPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(741, 340);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(162, 67);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "生成CDKey";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSavePath
            // 
            this.btnSavePath.Location = new System.Drawing.Point(12, 49);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(128, 23);
            this.btnSavePath.TabIndex = 11;
            this.btnSavePath.Text = "保存路径";
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "使用次数限制:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "CDKey名称:";
            // 
            // btnSelectCDKeyExcelPath
            // 
            this.btnSelectCDKeyExcelPath.Location = new System.Drawing.Point(12, 16);
            this.btnSelectCDKeyExcelPath.Name = "btnSelectCDKeyExcelPath";
            this.btnSelectCDKeyExcelPath.Size = new System.Drawing.Size(128, 23);
            this.btnSelectCDKeyExcelPath.TabIndex = 25;
            this.btnSelectCDKeyExcelPath.Text = "CDKey文件";
            this.btnSelectCDKeyExcelPath.UseVisualStyleBackColor = true;
            this.btnSelectCDKeyExcelPath.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // listCDKey
            // 
            this.listCDKey.Cursor = System.Windows.Forms.Cursors.Default;
            this.listCDKey.FormattingEnabled = true;
            this.listCDKey.ItemHeight = 12;
            this.listCDKey.Location = new System.Drawing.Point(12, 86);
            this.listCDKey.Name = "listCDKey";
            this.listCDKey.Size = new System.Drawing.Size(309, 316);
            this.listCDKey.TabIndex = 26;
            this.listCDKey.SelectedIndexChanged += new System.EventHandler(this.listCDKey_SelectedIndexChanged);
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(433, 113);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(47, 12);
            this.labName.TabIndex = 27;
            this.labName.Text = "labName";
            // 
            // labUseCount
            // 
            this.labUseCount.AutoSize = true;
            this.labUseCount.Location = new System.Drawing.Point(433, 136);
            this.labUseCount.Name = "labUseCount";
            this.labUseCount.Size = new System.Drawing.Size(71, 12);
            this.labUseCount.TabIndex = 28;
            this.labUseCount.Text = "labUseCount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "CDKey编号:";
            // 
            // labId
            // 
            this.labId.AutoSize = true;
            this.labId.Location = new System.Drawing.Point(433, 90);
            this.labId.Name = "labId";
            this.labId.Size = new System.Drawing.Size(35, 12);
            this.labId.TabIndex = 30;
            this.labId.Text = "labId";
            // 
            // labNum
            // 
            this.labNum.AutoSize = true;
            this.labNum.Location = new System.Drawing.Point(433, 160);
            this.labNum.Name = "labNum";
            this.labNum.Size = new System.Drawing.Size(41, 12);
            this.labNum.TabIndex = 32;
            this.labNum.Text = "labNum";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(368, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "CDKey数量:";
            // 
            // labEndTime
            // 
            this.labEndTime.AutoSize = true;
            this.labEndTime.Location = new System.Drawing.Point(433, 184);
            this.labEndTime.Name = "labEndTime";
            this.labEndTime.Size = new System.Drawing.Size(65, 12);
            this.labEndTime.TabIndex = 34;
            this.labEndTime.Text = "labEndTime";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(373, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 33;
            this.label10.Text = "过期时间:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(373, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "物品描述:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(472, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "可使用此编号的CDKey次数";
            // 
            // labDes
            // 
            this.labDes.Location = new System.Drawing.Point(433, 207);
            this.labDes.Name = "labDes";
            this.labDes.Size = new System.Drawing.Size(274, 98);
            this.labDes.TabIndex = 37;
            this.labDes.Text = "labDes";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(348, 378);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(359, 23);
            this.progressBar.TabIndex = 38;
            this.progressBar.Value = 20;
            // 
            // txtCDKey
            // 
            this.txtCDKey.Location = new System.Drawing.Point(699, 80);
            this.txtCDKey.Name = "txtCDKey";
            this.txtCDKey.Size = new System.Drawing.Size(204, 21);
            this.txtCDKey.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(700, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "CDKey检测";
            // 
            // txtCDKeyResult
            // 
            this.txtCDKeyResult.Location = new System.Drawing.Point(699, 127);
            this.txtCDKeyResult.Name = "txtCDKeyResult";
            this.txtCDKeyResult.Size = new System.Drawing.Size(204, 21);
            this.txtCDKeyResult.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(704, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 42;
            this.label7.Text = "检测结果";
            // 
            // btnCheckCDKey
            // 
            this.btnCheckCDKey.Location = new System.Drawing.Point(699, 160);
            this.btnCheckCDKey.Name = "btnCheckCDKey";
            this.btnCheckCDKey.Size = new System.Drawing.Size(162, 37);
            this.btnCheckCDKey.TabIndex = 43;
            this.btnCheckCDKey.Text = "检测CDKey";
            this.btnCheckCDKey.UseVisualStyleBackColor = true;
            this.btnCheckCDKey.Click += new System.EventHandler(this.btnCheckCDKey_Click);
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(146, 51);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.ReadOnly = true;
            this.txtSavePath.Size = new System.Drawing.Size(479, 21);
            this.txtSavePath.TabIndex = 56;
            // 
            // txtCDKeyExcelPath
            // 
            this.txtCDKeyExcelPath.Location = new System.Drawing.Point(146, 18);
            this.txtCDKeyExcelPath.Name = "txtCDKeyExcelPath";
            this.txtCDKeyExcelPath.ReadOnly = true;
            this.txtCDKeyExcelPath.Size = new System.Drawing.Size(479, 21);
            this.txtCDKeyExcelPath.TabIndex = 57;
            // 
            // CDKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 420);
            this.Controls.Add(this.txtCDKeyExcelPath);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.btnCheckCDKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCDKeyResult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCDKey);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labDes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labEndTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labNum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labUseCount);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.listCDKey);
            this.Controls.Add(this.btnSelectCDKeyExcelPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CDKeyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DBExportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectCDKeyExcelPath;
        private System.Windows.Forms.ListBox listCDKey;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labUseCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labId;
        private System.Windows.Forms.Label labNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labEndTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labDes;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox txtCDKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCDKeyResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCheckCDKey;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.TextBox txtCDKeyExcelPath;
    }
}