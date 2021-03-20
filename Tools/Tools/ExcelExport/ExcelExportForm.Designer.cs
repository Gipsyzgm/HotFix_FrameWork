namespace Tools
{
    partial class ExcelExportForm
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
            this.ClientDirTxt = new System.Windows.Forms.TextBox();
            this.ConfigDirTxt = new System.Windows.Forms.TextBox();
            this.ServerDirTxt = new System.Windows.Forms.TextBox();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.ConfigDirBtn = new System.Windows.Forms.Button();
            this.ClientDirBtn = new System.Windows.Forms.Button();
            this.ServerDirBtn = new System.Windows.Forms.Button();
            this.ServerOutDirBtn = new System.Windows.Forms.Button();
            this.ServerOutDirTxt = new System.Windows.Forms.TextBox();
            this.ClientOutDirBtn = new System.Windows.Forms.Button();
            this.ClientOutDirTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVerLangFile = new System.Windows.Forms.TextBox();
            this.txtVerLangOutFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExportVerLang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(561, 292);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(179, 50);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "一键导出配置文件";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ClientDirTxt
            // 
            this.ClientDirTxt.Location = new System.Drawing.Point(411, 39);
            this.ClientDirTxt.Name = "ClientDirTxt";
            this.ClientDirTxt.ReadOnly = true;
            this.ClientDirTxt.Size = new System.Drawing.Size(329, 21);
            this.ClientDirTxt.TabIndex = 2;
            // 
            // ConfigDirTxt
            // 
            this.ConfigDirTxt.Location = new System.Drawing.Point(12, 39);
            this.ConfigDirTxt.Name = "ConfigDirTxt";
            this.ConfigDirTxt.ReadOnly = true;
            this.ConfigDirTxt.Size = new System.Drawing.Size(259, 21);
            this.ConfigDirTxt.TabIndex = 4;
            // 
            // ServerDirTxt
            // 
            this.ServerDirTxt.Location = new System.Drawing.Point(411, 138);
            this.ServerDirTxt.Name = "ServerDirTxt";
            this.ServerDirTxt.ReadOnly = true;
            this.ServerDirTxt.Size = new System.Drawing.Size(329, 21);
            this.ServerDirTxt.TabIndex = 6;
            // 
            // listFiles
            // 
            this.listFiles.FormattingEnabled = true;
            this.listFiles.ItemHeight = 12;
            this.listFiles.Location = new System.Drawing.Point(12, 62);
            this.listFiles.Name = "listFiles";
            this.listFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listFiles.Size = new System.Drawing.Size(259, 280);
            this.listFiles.TabIndex = 8;
            this.listFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFiles_MouseDoubleClick);
            // 
            // ConfigDirBtn
            // 
            this.ConfigDirBtn.Location = new System.Drawing.Point(12, 10);
            this.ConfigDirBtn.Name = "ConfigDirBtn";
            this.ConfigDirBtn.Size = new System.Drawing.Size(88, 23);
            this.ConfigDirBtn.TabIndex = 9;
            this.ConfigDirBtn.Text = "配置文件目录";
            this.ConfigDirBtn.UseVisualStyleBackColor = true;
            this.ConfigDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ClientDirBtn
            // 
            this.ClientDirBtn.Location = new System.Drawing.Point(292, 39);
            this.ClientDirBtn.Name = "ClientDirBtn";
            this.ClientDirBtn.Size = new System.Drawing.Size(113, 23);
            this.ClientDirBtn.TabIndex = 10;
            this.ClientDirBtn.Text = "客户端目录";
            this.ClientDirBtn.UseVisualStyleBackColor = true;
            this.ClientDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ServerDirBtn
            // 
            this.ServerDirBtn.Location = new System.Drawing.Point(292, 136);
            this.ServerDirBtn.Name = "ServerDirBtn";
            this.ServerDirBtn.Size = new System.Drawing.Size(113, 23);
            this.ServerDirBtn.TabIndex = 11;
            this.ServerDirBtn.Text = "服务端目录";
            this.ServerDirBtn.UseVisualStyleBackColor = true;
            this.ServerDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ServerOutDirBtn
            // 
            this.ServerOutDirBtn.Location = new System.Drawing.Point(292, 165);
            this.ServerOutDirBtn.Name = "ServerOutDirBtn";
            this.ServerOutDirBtn.Size = new System.Drawing.Size(113, 23);
            this.ServerOutDirBtn.TabIndex = 32;
            this.ServerOutDirBtn.Text = "服务端配置导出目录";
            this.ServerOutDirBtn.UseVisualStyleBackColor = true;
            this.ServerOutDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ServerOutDirTxt
            // 
            this.ServerOutDirTxt.Location = new System.Drawing.Point(411, 167);
            this.ServerOutDirTxt.Name = "ServerOutDirTxt";
            this.ServerOutDirTxt.ReadOnly = true;
            this.ServerOutDirTxt.Size = new System.Drawing.Size(329, 21);
            this.ServerOutDirTxt.TabIndex = 31;
            // 
            // ClientOutDirBtn
            // 
            this.ClientOutDirBtn.Location = new System.Drawing.Point(292, 68);
            this.ClientOutDirBtn.Name = "ClientOutDirBtn";
            this.ClientOutDirBtn.Size = new System.Drawing.Size(113, 23);
            this.ClientOutDirBtn.TabIndex = 34;
            this.ClientOutDirBtn.Text = "客户端配置导出目录";
            this.ClientOutDirBtn.UseVisualStyleBackColor = true;
            this.ClientOutDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ClientOutDirTxt
            // 
            this.ClientOutDirTxt.Location = new System.Drawing.Point(411, 70);
            this.ClientOutDirTxt.Name = "ClientOutDirTxt";
            this.ClientOutDirTxt.ReadOnly = true;
            this.ClientOutDirTxt.Size = new System.Drawing.Size(329, 21);
            this.ClientOutDirTxt.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(288, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 22);
            this.label2.TabIndex = 37;
            this.label2.Text = "客户端导出:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkGray;
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(288, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 22);
            this.label3.TabIndex = 38;
            this.label3.Text = "服务端导出:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(288, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 22);
            this.label1.TabIndex = 41;
            this.label1.Text = "版本检测文字导出:";
            // 
            // txtVerLangFile
            // 
            this.txtVerLangFile.Location = new System.Drawing.Point(349, 238);
            this.txtVerLangFile.Name = "txtVerLangFile";
            this.txtVerLangFile.ReadOnly = true;
            this.txtVerLangFile.Size = new System.Drawing.Size(310, 21);
            this.txtVerLangFile.TabIndex = 39;
            // 
            // txtVerLangOutFile
            // 
            this.txtVerLangOutFile.Location = new System.Drawing.Point(349, 265);
            this.txtVerLangOutFile.Name = "txtVerLangOutFile";
            this.txtVerLangOutFile.ReadOnly = true;
            this.txtVerLangOutFile.Size = new System.Drawing.Size(310, 21);
            this.txtVerLangOutFile.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "文件路径";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(290, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "导出路径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(505, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "此文件不能热更";
            // 
            // btnExportVerLang
            // 
            this.btnExportVerLang.Location = new System.Drawing.Point(665, 226);
            this.btnExportVerLang.Name = "btnExportVerLang";
            this.btnExportVerLang.Size = new System.Drawing.Size(75, 60);
            this.btnExportVerLang.TabIndex = 47;
            this.btnExportVerLang.Text = "导出";
            this.btnExportVerLang.UseVisualStyleBackColor = true;
            this.btnExportVerLang.Click += new System.EventHandler(this.btnExportVerLang_Click);
            // 
            // ExcelExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 354);
            this.Controls.Add(this.btnExportVerLang);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVerLangOutFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVerLangFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClientOutDirBtn);
            this.Controls.Add(this.ClientOutDirTxt);
            this.Controls.Add(this.ServerOutDirBtn);
            this.Controls.Add(this.ServerOutDirTxt);
            this.Controls.Add(this.ServerDirBtn);
            this.Controls.Add(this.ClientDirBtn);
            this.Controls.Add(this.ConfigDirBtn);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.ServerDirTxt);
            this.Controls.Add(this.ConfigDirTxt);
            this.Controls.Add(this.ClientDirTxt);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExcelExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ExcelExportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox ClientDirTxt;
        private System.Windows.Forms.TextBox ConfigDirTxt;
        private System.Windows.Forms.TextBox ServerDirTxt;
        private System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Button ConfigDirBtn;
        private System.Windows.Forms.Button ClientDirBtn;
        private System.Windows.Forms.Button ServerDirBtn;
        private System.Windows.Forms.Button ServerOutDirBtn;
        private System.Windows.Forms.TextBox ServerOutDirTxt;
        private System.Windows.Forms.Button ClientOutDirBtn;
        private System.Windows.Forms.TextBox ClientOutDirTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVerLangFile;
        private System.Windows.Forms.TextBox txtVerLangOutFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExportVerLang;
    }
}