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
            this.ProtoPathBtn = new System.Windows.Forms.Button();
            this.ServerPathBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ClientPathBtn = new System.Windows.Forms.Button();
            this.ProtoPathTxt = new System.Windows.Forms.TextBox();
            this.ClientPathTxt = new System.Windows.Forms.TextBox();
            this.ServerPathTxt = new System.Windows.Forms.TextBox();
            this.ClientHoxPathTxt = new System.Windows.Forms.TextBox();
            this.ClientHoxPathBtn = new System.Windows.Forms.Button();
            this.treeFiles = new System.Windows.Forms.TreeView();
            this.panelBtnList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(282, 311);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(458, 31);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "一键生成所有相关文件";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ProtoPathBtn
            // 
            this.ProtoPathBtn.Location = new System.Drawing.Point(13, 10);
            this.ProtoPathBtn.Name = "ProtoPathBtn";
            this.ProtoPathBtn.Size = new System.Drawing.Size(75, 23);
            this.ProtoPathBtn.TabIndex = 9;
            this.ProtoPathBtn.Text = "Proto目录";
            this.ProtoPathBtn.UseVisualStyleBackColor = true;
            this.ProtoPathBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ServerPathBtn
            // 
            this.ServerPathBtn.Location = new System.Drawing.Point(282, 128);
            this.ServerPathBtn.Name = "ServerPathBtn";
            this.ServerPathBtn.Size = new System.Drawing.Size(113, 23);
            this.ServerPathBtn.TabIndex = 11;
            this.ServerPathBtn.Text = "服务端目录";
            this.ServerPathBtn.UseVisualStyleBackColor = true;
            this.ServerPathBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(278, 11);
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
            this.label3.Location = new System.Drawing.Point(278, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 22);
            this.label3.TabIndex = 39;
            this.label3.Text = "服务端导出:";
            // 
            // ClientPathBtn
            // 
            this.ClientPathBtn.Location = new System.Drawing.Point(282, 38);
            this.ClientPathBtn.Name = "ClientPathBtn";
            this.ClientPathBtn.Size = new System.Drawing.Size(113, 23);
            this.ClientPathBtn.TabIndex = 41;
            this.ClientPathBtn.Text = "客户端目录";
            this.ClientPathBtn.UseVisualStyleBackColor = true;
            this.ClientPathBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ProtoPathTxt
            // 
            this.ProtoPathTxt.Location = new System.Drawing.Point(12, 34);
            this.ProtoPathTxt.Name = "ProtoPathTxt";
            this.ProtoPathTxt.ReadOnly = true;
            this.ProtoPathTxt.Size = new System.Drawing.Size(260, 21);
            this.ProtoPathTxt.TabIndex = 51;
            this.ProtoPathTxt.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ClientPathTxt
            // 
            this.ClientPathTxt.Location = new System.Drawing.Point(401, 40);
            this.ClientPathTxt.Name = "ClientPathTxt";
            this.ClientPathTxt.ReadOnly = true;
            this.ClientPathTxt.Size = new System.Drawing.Size(339, 21);
            this.ClientPathTxt.TabIndex = 52;
            // 
            // ServerPathTxt
            // 
            this.ServerPathTxt.Location = new System.Drawing.Point(401, 130);
            this.ServerPathTxt.Name = "ServerPathTxt";
            this.ServerPathTxt.ReadOnly = true;
            this.ServerPathTxt.Size = new System.Drawing.Size(339, 21);
            this.ServerPathTxt.TabIndex = 53;
            // 
            // ClientHoxPathTxt
            // 
            this.ClientHoxPathTxt.Location = new System.Drawing.Point(401, 69);
            this.ClientHoxPathTxt.Name = "ClientHoxPathTxt";
            this.ClientHoxPathTxt.ReadOnly = true;
            this.ClientHoxPathTxt.Size = new System.Drawing.Size(339, 21);
            this.ClientHoxPathTxt.TabIndex = 55;
            // 
            // ClientHoxPathBtn
            // 
            this.ClientHoxPathBtn.Location = new System.Drawing.Point(282, 67);
            this.ClientHoxPathBtn.Name = "ClientHoxPathBtn";
            this.ClientHoxPathBtn.Size = new System.Drawing.Size(113, 23);
            this.ClientHoxPathBtn.TabIndex = 54;
            this.ClientHoxPathBtn.Text = "客户端热更目录";
            this.ClientHoxPathBtn.UseVisualStyleBackColor = true;
            this.ClientHoxPathBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // treeFiles
            // 
            this.treeFiles.Location = new System.Drawing.Point(13, 61);
            this.treeFiles.Name = "treeFiles";
            this.treeFiles.Size = new System.Drawing.Size(259, 281);
            this.treeFiles.TabIndex = 56;
            this.treeFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeFiles_MouseDoubleClick);
            // 
            // panelBtnList
            // 
            this.panelBtnList.Location = new System.Drawing.Point(282, 191);
            this.panelBtnList.Name = "panelBtnList";
            this.panelBtnList.Size = new System.Drawing.Size(458, 114);
            this.panelBtnList.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(278, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 22);
            this.label1.TabIndex = 58;
            this.label1.Text = "编辑config.txt导出配置文件:";
            // 
            // ProtoExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 354);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelBtnList);
            this.Controls.Add(this.treeFiles);
            this.Controls.Add(this.ClientHoxPathTxt);
            this.Controls.Add(this.ClientHoxPathBtn);
            this.Controls.Add(this.ServerPathTxt);
            this.Controls.Add(this.ClientPathTxt);
            this.Controls.Add(this.ProtoPathTxt);
            this.Controls.Add(this.ClientPathBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerPathBtn);
            this.Controls.Add(this.ProtoPathBtn);
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
        private System.Windows.Forms.Button ProtoPathBtn;
        private System.Windows.Forms.Button ServerPathBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ClientPathBtn;
        private System.Windows.Forms.TextBox ProtoPathTxt;
        private System.Windows.Forms.TextBox ClientPathTxt;
        private System.Windows.Forms.TextBox ServerPathTxt;
        private System.Windows.Forms.TextBox ClientHoxPathTxt;
        private System.Windows.Forms.Button ClientHoxPathBtn;
        private System.Windows.Forms.TreeView treeFiles;
        private System.Windows.Forms.Panel panelBtnList;
        private System.Windows.Forms.Label label1;
    }
}