namespace Tools
{
    partial class GMExportForm
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
            this.ExportBtn = new System.Windows.Forms.Button();
            this.SelectDBPathBtn = new System.Windows.Forms.Button();
            this.SelectGameServerPathBtn = new System.Windows.Forms.Button();
            this.SelectAPIPathBtn = new System.Windows.Forms.Button();
            this.ExportAPIBtn = new System.Windows.Forms.Button();
            this.ExportConfigBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.GameDBExportBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GameServerPathTxt = new System.Windows.Forms.TextBox();
            this.DBPathTxt = new System.Windows.Forms.TextBox();
            this.APIPathTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExportBtn
            // 
            this.ExportBtn.Location = new System.Drawing.Point(12, 108);
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new System.Drawing.Size(165, 70);
            this.ExportBtn.TabIndex = 1;
            this.ExportBtn.Text = "生成GM数据库结构类";
            this.ExportBtn.UseVisualStyleBackColor = true;
            this.ExportBtn.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // SelectDBPathBtn
            // 
            this.SelectDBPathBtn.Location = new System.Drawing.Point(12, 61);
            this.SelectDBPathBtn.Name = "SelectDBPathBtn";
            this.SelectDBPathBtn.Size = new System.Drawing.Size(144, 23);
            this.SelectDBPathBtn.TabIndex = 9;
            this.SelectDBPathBtn.Text = "GM数据库设计Excel文件";
            this.SelectDBPathBtn.UseVisualStyleBackColor = true;
            this.SelectDBPathBtn.Click += new System.EventHandler(this.SelctFile_ClickEvent);
            // 
            // SelectGameServerPathBtn
            // 
            this.SelectGameServerPathBtn.Location = new System.Drawing.Point(12, 20);
            this.SelectGameServerPathBtn.Name = "SelectGameServerPathBtn";
            this.SelectGameServerPathBtn.Size = new System.Drawing.Size(144, 23);
            this.SelectGameServerPathBtn.TabIndex = 11;
            this.SelectGameServerPathBtn.Text = "GM服务端目录";
            this.SelectGameServerPathBtn.UseVisualStyleBackColor = true;
            this.SelectGameServerPathBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // SelectAPIPathBtn
            // 
            this.SelectAPIPathBtn.Location = new System.Drawing.Point(12, 198);
            this.SelectAPIPathBtn.Name = "SelectAPIPathBtn";
            this.SelectAPIPathBtn.Size = new System.Drawing.Size(144, 23);
            this.SelectAPIPathBtn.TabIndex = 15;
            this.SelectAPIPathBtn.Text = "GM接口Excel文件";
            this.SelectAPIPathBtn.UseVisualStyleBackColor = true;
            this.SelectAPIPathBtn.Click += new System.EventHandler(this.SelctFile_ClickEvent);
            // 
            // ExportAPIBtn
            // 
            this.ExportAPIBtn.Location = new System.Drawing.Point(12, 248);
            this.ExportAPIBtn.Name = "ExportAPIBtn";
            this.ExportAPIBtn.Size = new System.Drawing.Size(165, 70);
            this.ExportAPIBtn.TabIndex = 13;
            this.ExportAPIBtn.Text = "生成GM接口结构类";
            this.ExportAPIBtn.UseVisualStyleBackColor = true;
            this.ExportAPIBtn.Click += new System.EventHandler(this.btnExportAPI_Click);
            // 
            // ExportConfigBtn
            // 
            this.ExportConfigBtn.Location = new System.Drawing.Point(276, 248);
            this.ExportConfigBtn.Name = "ExportConfigBtn";
            this.ExportConfigBtn.Size = new System.Drawing.Size(165, 70);
            this.ExportConfigBtn.TabIndex = 17;
            this.ExportConfigBtn.Text = "生成配置表配置数据";
            this.ExportConfigBtn.UseVisualStyleBackColor = true;
            this.ExportConfigBtn.Click += new System.EventHandler(this.btnExportConfig_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "游戏配置文件导出，GM后台使用";
            // 
            // GameDBExportBtn
            // 
            this.GameDBExportBtn.Location = new System.Drawing.Point(276, 108);
            this.GameDBExportBtn.Name = "GameDBExportBtn";
            this.GameDBExportBtn.Size = new System.Drawing.Size(165, 70);
            this.GameDBExportBtn.TabIndex = 41;
            this.GameDBExportBtn.Text = "生成游戏数据库结构类";
            this.GameDBExportBtn.UseVisualStyleBackColor = true;
            this.GameDBExportBtn.Click += new System.EventHandler(this.btnGameDBExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(533, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "导出游戏数据库结构,GM后台使用";
            // 
            // GameServerPathTxt
            // 
            this.GameServerPathTxt.Location = new System.Drawing.Point(162, 22);
            this.GameServerPathTxt.Name = "GameServerPathTxt";
            this.GameServerPathTxt.ReadOnly = true;
            this.GameServerPathTxt.Size = new System.Drawing.Size(550, 21);
            this.GameServerPathTxt.TabIndex = 56;
            // 
            // DBPathTxt
            // 
            this.DBPathTxt.Location = new System.Drawing.Point(162, 63);
            this.DBPathTxt.Name = "DBPathTxt";
            this.DBPathTxt.ReadOnly = true;
            this.DBPathTxt.Size = new System.Drawing.Size(550, 21);
            this.DBPathTxt.TabIndex = 57;
            // 
            // APIPathTxt
            // 
            this.APIPathTxt.Location = new System.Drawing.Point(162, 200);
            this.APIPathTxt.Name = "APIPathTxt";
            this.APIPathTxt.ReadOnly = true;
            this.APIPathTxt.Size = new System.Drawing.Size(550, 21);
            this.APIPathTxt.TabIndex = 58;
            // 
            // GMExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 354);
            this.Controls.Add(this.APIPathTxt);
            this.Controls.Add(this.DBPathTxt);
            this.Controls.Add(this.GameServerPathTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GameDBExportBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ExportConfigBtn);
            this.Controls.Add(this.SelectAPIPathBtn);
            this.Controls.Add(this.ExportAPIBtn);
            this.Controls.Add(this.SelectGameServerPathBtn);
            this.Controls.Add(this.SelectDBPathBtn);
            this.Controls.Add(this.ExportBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GMExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GMExportForm_Load);
            this.Click += new System.EventHandler(this.btnOpenAPIFile_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SelectDBPathBtn;
        private System.Windows.Forms.Button SelectGameServerPathBtn;
        private System.Windows.Forms.Button SelectAPIPathBtn;
        private System.Windows.Forms.Button ExportAPIBtn;
        private System.Windows.Forms.Button ExportConfigBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ExportBtn;
        private System.Windows.Forms.Button GameDBExportBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GameServerPathTxt;
        private System.Windows.Forms.TextBox DBPathTxt;
        private System.Windows.Forms.TextBox APIPathTxt;
    }
}