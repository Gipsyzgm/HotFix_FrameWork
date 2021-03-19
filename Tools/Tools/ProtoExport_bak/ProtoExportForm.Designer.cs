namespace Tools
{
    partial class ProtoExportForm
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
            this.listFiles = new System.Windows.Forms.ListBox();
            this.btnProtoPath = new System.Windows.Forms.Button();
            this.btnServerPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClientPath = new System.Windows.Forms.Button();
            this.btnEditConfigFile = new System.Windows.Forms.Button();
            this.txtProtoPath = new System.Windows.Forms.TextBox();
            this.txtClientPath = new System.Windows.Forms.TextBox();
            this.txtServerPath = new System.Windows.Forms.TextBox();
            this.txtClientHoxPath = new System.Windows.Forms.TextBox();
            this.btnClientHoxPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(733, 351);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(175, 57);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "一键生成所有相关文件";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // listFiles
            // 
            this.listFiles.Cursor = System.Windows.Forms.Cursors.Default;
            this.listFiles.FormattingEnabled = true;
            this.listFiles.ItemHeight = 12;
            this.listFiles.Location = new System.Drawing.Point(12, 44);
            this.listFiles.Name = "listFiles";
            this.listFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listFiles.Size = new System.Drawing.Size(342, 364);
            this.listFiles.TabIndex = 8;
            // 
            // btnProtoPath
            // 
            this.btnProtoPath.Location = new System.Drawing.Point(13, 10);
            this.btnProtoPath.Name = "btnProtoPath";
            this.btnProtoPath.Size = new System.Drawing.Size(75, 23);
            this.btnProtoPath.TabIndex = 9;
            this.btnProtoPath.Text = "Proto目录";
            this.btnProtoPath.UseVisualStyleBackColor = true;
            this.btnProtoPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // btnServerPath
            // 
            this.btnServerPath.Location = new System.Drawing.Point(383, 156);
            this.btnServerPath.Name = "btnServerPath";
            this.btnServerPath.Size = new System.Drawing.Size(113, 23);
            this.btnServerPath.TabIndex = 11;
            this.btnServerPath.Text = "服务端目录";
            this.btnServerPath.UseVisualStyleBackColor = true;
            this.btnServerPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(382, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 22);
            this.label2.TabIndex = 38;
            this.label2.Text = "客户端导出:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.DarkGray;
            this.label3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(382, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 22);
            this.label3.TabIndex = 39;
            this.label3.Text = "服务端导出:";
            // 
            // btnClientPath
            // 
            this.btnClientPath.Location = new System.Drawing.Point(383, 42);
            this.btnClientPath.Name = "btnClientPath";
            this.btnClientPath.Size = new System.Drawing.Size(113, 23);
            this.btnClientPath.TabIndex = 41;
            this.btnClientPath.Text = "客户端目录";
            this.btnClientPath.UseVisualStyleBackColor = true;
            this.btnClientPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // btnEditConfigFile
            // 
            this.btnEditConfigFile.Location = new System.Drawing.Point(383, 362);
            this.btnEditConfigFile.Name = "btnEditConfigFile";
            this.btnEditConfigFile.Size = new System.Drawing.Size(179, 34);
            this.btnEditConfigFile.TabIndex = 50;
            this.btnEditConfigFile.Text = "编辑config.txt导出配置文件";
            this.btnEditConfigFile.UseVisualStyleBackColor = true;
            this.btnEditConfigFile.Click += new System.EventHandler(this.btnEditConfigFile_Click);
            // 
            // txtProtoPath
            // 
            this.txtProtoPath.Location = new System.Drawing.Point(94, 12);
            this.txtProtoPath.Name = "txtProtoPath";
            this.txtProtoPath.ReadOnly = true;
            this.txtProtoPath.Size = new System.Drawing.Size(260, 21);
            this.txtProtoPath.TabIndex = 51;
            this.txtProtoPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // txtClientPath
            // 
            this.txtClientPath.Location = new System.Drawing.Point(502, 44);
            this.txtClientPath.Name = "txtClientPath";
            this.txtClientPath.ReadOnly = true;
            this.txtClientPath.Size = new System.Drawing.Size(406, 21);
            this.txtClientPath.TabIndex = 52;
            // 
            // txtServerPath
            // 
            this.txtServerPath.Location = new System.Drawing.Point(502, 156);
            this.txtServerPath.Name = "txtServerPath";
            this.txtServerPath.ReadOnly = true;
            this.txtServerPath.Size = new System.Drawing.Size(406, 21);
            this.txtServerPath.TabIndex = 53;
            // 
            // txtClientHoxPath
            // 
            this.txtClientHoxPath.Location = new System.Drawing.Point(502, 76);
            this.txtClientHoxPath.Name = "txtClientHoxPath";
            this.txtClientHoxPath.ReadOnly = true;
            this.txtClientHoxPath.Size = new System.Drawing.Size(406, 21);
            this.txtClientHoxPath.TabIndex = 55;
            // 
            // btnClientHoxPath
            // 
            this.btnClientHoxPath.Location = new System.Drawing.Point(383, 74);
            this.btnClientHoxPath.Name = "btnClientHoxPath";
            this.btnClientHoxPath.Size = new System.Drawing.Size(113, 23);
            this.btnClientHoxPath.TabIndex = 54;
            this.btnClientHoxPath.Text = "客户端热更目录";
            this.btnClientHoxPath.UseVisualStyleBackColor = true;
            this.btnClientHoxPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ProtoExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 420);
            this.Controls.Add(this.txtClientHoxPath);
            this.Controls.Add(this.btnClientHoxPath);
            this.Controls.Add(this.txtServerPath);
            this.Controls.Add(this.txtClientPath);
            this.Controls.Add(this.txtProtoPath);
            this.Controls.Add(this.btnEditConfigFile);
            this.Controls.Add(this.btnClientPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnServerPath);
            this.Controls.Add(this.btnProtoPath);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProtoExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ProtoExportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Button btnProtoPath;
        private System.Windows.Forms.Button btnServerPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClientPath;
        private System.Windows.Forms.Button btnEditConfigFile;
        private System.Windows.Forms.TextBox txtProtoPath;
        private System.Windows.Forms.TextBox txtClientPath;
        private System.Windows.Forms.TextBox txtServerPath;
        private System.Windows.Forms.TextBox txtClientHoxPath;
        private System.Windows.Forms.Button btnClientHoxPath;
    }
}