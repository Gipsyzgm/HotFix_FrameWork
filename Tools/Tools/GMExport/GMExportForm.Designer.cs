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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSelectDBPath = new System.Windows.Forms.Button();
            this.btnSelectGameServerPath = new System.Windows.Forms.Button();
            this.btnSelectAPIPath = new System.Windows.Forms.Button();
            this.btnExportAPI = new System.Windows.Forms.Button();
            this.btnExportConfig = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGameDBExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGameServerPath = new System.Windows.Forms.TextBox();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.txtAPIPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 95);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(162, 67);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "生成GM数据库结构类";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSelectDBPath
            // 
            this.btnSelectDBPath.Location = new System.Drawing.Point(12, 61);
            this.btnSelectDBPath.Name = "btnSelectDBPath";
            this.btnSelectDBPath.Size = new System.Drawing.Size(144, 23);
            this.btnSelectDBPath.TabIndex = 9;
            this.btnSelectDBPath.Text = "GM数据库设计Excel文件";
            this.btnSelectDBPath.UseVisualStyleBackColor = true;
            this.btnSelectDBPath.Click += new System.EventHandler(this.SelctFile_ClickEvent);
            // 
            // btnSelectGameServerPath
            // 
            this.btnSelectGameServerPath.Location = new System.Drawing.Point(12, 22);
            this.btnSelectGameServerPath.Name = "btnSelectGameServerPath";
            this.btnSelectGameServerPath.Size = new System.Drawing.Size(144, 23);
            this.btnSelectGameServerPath.TabIndex = 11;
            this.btnSelectGameServerPath.Text = "GM服务端目录";
            this.btnSelectGameServerPath.UseVisualStyleBackColor = true;
            this.btnSelectGameServerPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // btnSelectAPIPath
            // 
            this.btnSelectAPIPath.Location = new System.Drawing.Point(12, 200);
            this.btnSelectAPIPath.Name = "btnSelectAPIPath";
            this.btnSelectAPIPath.Size = new System.Drawing.Size(144, 23);
            this.btnSelectAPIPath.TabIndex = 15;
            this.btnSelectAPIPath.Text = "GM接口Excel文件";
            this.btnSelectAPIPath.UseVisualStyleBackColor = true;
            this.btnSelectAPIPath.Click += new System.EventHandler(this.SelctFile_ClickEvent);
            // 
            // btnExportAPI
            // 
            this.btnExportAPI.Location = new System.Drawing.Point(12, 238);
            this.btnExportAPI.Name = "btnExportAPI";
            this.btnExportAPI.Size = new System.Drawing.Size(162, 67);
            this.btnExportAPI.TabIndex = 13;
            this.btnExportAPI.Text = "生成GM接口结构类";
            this.btnExportAPI.UseVisualStyleBackColor = true;
            this.btnExportAPI.Click += new System.EventHandler(this.btnExportAPI_Click);
            // 
            // btnExportConfig
            // 
            this.btnExportConfig.Location = new System.Drawing.Point(12, 341);
            this.btnExportConfig.Name = "btnExportConfig";
            this.btnExportConfig.Size = new System.Drawing.Size(162, 67);
            this.btnExportConfig.TabIndex = 17;
            this.btnExportConfig.Text = "生成配置表配置数据";
            this.btnExportConfig.UseVisualStyleBackColor = true;
            this.btnExportConfig.Click += new System.EventHandler(this.btnExportConfig_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "游戏配置文件导出，GM后台使用";
            // 
            // btnGameDBExport
            // 
            this.btnGameDBExport.Location = new System.Drawing.Point(233, 95);
            this.btnGameDBExport.Name = "btnGameDBExport";
            this.btnGameDBExport.Size = new System.Drawing.Size(162, 67);
            this.btnGameDBExport.TabIndex = 41;
            this.btnGameDBExport.Text = "生成游戏数据库结构类";
            this.btnGameDBExport.UseVisualStyleBackColor = true;
            this.btnGameDBExport.Click += new System.EventHandler(this.btnGameDBExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(420, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "导出游戏数据库结构,GM后台使用";
            // 
            // txtGameServerPath
            // 
            this.txtGameServerPath.Location = new System.Drawing.Point(162, 22);
            this.txtGameServerPath.Name = "txtGameServerPath";
            this.txtGameServerPath.ReadOnly = true;
            this.txtGameServerPath.Size = new System.Drawing.Size(479, 21);
            this.txtGameServerPath.TabIndex = 56;
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(162, 61);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.ReadOnly = true;
            this.txtDBPath.Size = new System.Drawing.Size(479, 21);
            this.txtDBPath.TabIndex = 57;
            // 
            // txtAPIPath
            // 
            this.txtAPIPath.Location = new System.Drawing.Point(162, 200);
            this.txtAPIPath.Name = "txtAPIPath";
            this.txtAPIPath.ReadOnly = true;
            this.txtAPIPath.Size = new System.Drawing.Size(479, 21);
            this.txtAPIPath.TabIndex = 58;
            // 
            // GMExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 420);
            this.Controls.Add(this.txtAPIPath);
            this.Controls.Add(this.txtDBPath);
            this.Controls.Add(this.txtGameServerPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGameDBExport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExportConfig);
            this.Controls.Add(this.btnSelectAPIPath);
            this.Controls.Add(this.btnExportAPI);
            this.Controls.Add(this.btnSelectGameServerPath);
            this.Controls.Add(this.btnSelectDBPath);
            this.Controls.Add(this.btnExport);
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
        private System.Windows.Forms.Button btnSelectDBPath;
        private System.Windows.Forms.Button btnSelectGameServerPath;
        private System.Windows.Forms.Button btnSelectAPIPath;
        private System.Windows.Forms.Button btnExportAPI;
        private System.Windows.Forms.Button btnExportConfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnGameDBExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGameServerPath;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.TextBox txtAPIPath;
    }
}