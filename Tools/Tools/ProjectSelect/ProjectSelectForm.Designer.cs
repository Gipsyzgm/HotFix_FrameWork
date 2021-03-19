namespace Tools
{
    partial class ProjectSelectForm
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
            this.ProjectDirTxt = new System.Windows.Forms.TextBox();
            this.ClientDirTxt = new System.Windows.Forms.TextBox();
            this.ProjectDirBtn = new System.Windows.Forms.Button();
            this.ClientDirBtn = new System.Windows.Forms.Button();
            this.ServerDirBtn = new System.Windows.Forms.Button();
            this.ServerDirTxt = new System.Windows.Forms.TextBox();
            this.ConfigDirBtn = new System.Windows.Forms.Button();
            this.ConfigDirTxt = new System.Windows.Forms.TextBox();
            this.ProtoDirBtn = new System.Windows.Forms.Button();
            this.ProtoDirTxt = new System.Windows.Forms.TextBox();
            this.ProjecPaneltList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientHotDirBtn = new System.Windows.Forms.Button();
            this.ClientHotDirTxt = new System.Windows.Forms.TextBox();
            this.EditProConfigBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CheckClient = new System.Windows.Forms.CheckBox();
            this.CheckServer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ProjectDirTxt
            // 
            this.ProjectDirTxt.Location = new System.Drawing.Point(148, 124);
            this.ProjectDirTxt.Name = "ProjectDirTxt";
            this.ProjectDirTxt.ReadOnly = true;
            this.ProjectDirTxt.Size = new System.Drawing.Size(592, 21);
            this.ProjectDirTxt.TabIndex = 4;
            // 
            // ClientDirTxt
            // 
            this.ClientDirTxt.Location = new System.Drawing.Point(148, 153);
            this.ClientDirTxt.Name = "ClientDirTxt";
            this.ClientDirTxt.ReadOnly = true;
            this.ClientDirTxt.Size = new System.Drawing.Size(592, 21);
            this.ClientDirTxt.TabIndex = 6;
            // 
            // ProjectDirBtn
            // 
            this.ProjectDirBtn.Location = new System.Drawing.Point(14, 122);
            this.ProjectDirBtn.Name = "ProjectDirBtn";
            this.ProjectDirBtn.Size = new System.Drawing.Size(128, 23);
            this.ProjectDirBtn.TabIndex = 9;
            this.ProjectDirBtn.Text = "项目路径";
            this.ProjectDirBtn.UseVisualStyleBackColor = true;
            this.ProjectDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ClientDirBtn
            // 
            this.ClientDirBtn.Location = new System.Drawing.Point(14, 151);
            this.ClientDirBtn.Name = "ClientDirBtn";
            this.ClientDirBtn.Size = new System.Drawing.Size(128, 23);
            this.ClientDirBtn.TabIndex = 11;
            this.ClientDirBtn.Text = "客户端目录";
            this.ClientDirBtn.UseVisualStyleBackColor = true;
            this.ClientDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ServerDirBtn
            // 
            this.ServerDirBtn.Location = new System.Drawing.Point(14, 209);
            this.ServerDirBtn.Name = "ServerDirBtn";
            this.ServerDirBtn.Size = new System.Drawing.Size(128, 23);
            this.ServerDirBtn.TabIndex = 13;
            this.ServerDirBtn.Text = "服务端目录";
            this.ServerDirBtn.UseVisualStyleBackColor = true;
            this.ServerDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ServerDirTxt
            // 
            this.ServerDirTxt.Location = new System.Drawing.Point(148, 211);
            this.ServerDirTxt.Name = "ServerDirTxt";
            this.ServerDirTxt.ReadOnly = true;
            this.ServerDirTxt.Size = new System.Drawing.Size(592, 21);
            this.ServerDirTxt.TabIndex = 12;
            // 
            // ConfigDirBtn
            // 
            this.ConfigDirBtn.Location = new System.Drawing.Point(14, 238);
            this.ConfigDirBtn.Name = "ConfigDirBtn";
            this.ConfigDirBtn.Size = new System.Drawing.Size(128, 23);
            this.ConfigDirBtn.TabIndex = 15;
            this.ConfigDirBtn.Text = "Config目录";
            this.ConfigDirBtn.UseVisualStyleBackColor = true;
            this.ConfigDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ConfigDirTxt
            // 
            this.ConfigDirTxt.Location = new System.Drawing.Point(148, 240);
            this.ConfigDirTxt.Name = "ConfigDirTxt";
            this.ConfigDirTxt.ReadOnly = true;
            this.ConfigDirTxt.Size = new System.Drawing.Size(592, 21);
            this.ConfigDirTxt.TabIndex = 14;
            // 
            // ProtoDirBtn
            // 
            this.ProtoDirBtn.Location = new System.Drawing.Point(14, 267);
            this.ProtoDirBtn.Name = "ProtoDirBtn";
            this.ProtoDirBtn.Size = new System.Drawing.Size(128, 23);
            this.ProtoDirBtn.TabIndex = 17;
            this.ProtoDirBtn.Text = "Proto目录";
            this.ProtoDirBtn.UseVisualStyleBackColor = true;
            this.ProtoDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ProtoDirTxt
            // 
            this.ProtoDirTxt.Location = new System.Drawing.Point(148, 269);
            this.ProtoDirTxt.Name = "ProtoDirTxt";
            this.ProtoDirTxt.ReadOnly = true;
            this.ProtoDirTxt.Size = new System.Drawing.Size(592, 21);
            this.ProtoDirTxt.TabIndex = 16;
            // 
            // ProjecPaneltList
            // 
            this.ProjecPaneltList.Location = new System.Drawing.Point(14, 24);
            this.ProjecPaneltList.Name = "ProjecPaneltList";
            this.ProjecPaneltList.Size = new System.Drawing.Size(726, 92);
            this.ProjecPaneltList.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "选择需要操作的项目";
            // 
            // ClientHotDirBtn
            // 
            this.ClientHotDirBtn.Location = new System.Drawing.Point(14, 180);
            this.ClientHotDirBtn.Name = "ClientHotDirBtn";
            this.ClientHotDirBtn.Size = new System.Drawing.Size(128, 23);
            this.ClientHotDirBtn.TabIndex = 21;
            this.ClientHotDirBtn.Text = "客户端热更目录";
            this.ClientHotDirBtn.UseVisualStyleBackColor = true;
            this.ClientHotDirBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // ClientHotDirTxt
            // 
            this.ClientHotDirTxt.Location = new System.Drawing.Point(148, 182);
            this.ClientHotDirTxt.Name = "ClientHotDirTxt";
            this.ClientHotDirTxt.ReadOnly = true;
            this.ClientHotDirTxt.Size = new System.Drawing.Size(592, 21);
            this.ClientHotDirTxt.TabIndex = 20;
            // 
            // EditProConfigBtn
            // 
            this.EditProConfigBtn.Location = new System.Drawing.Point(14, 296);
            this.EditProConfigBtn.Name = "EditProConfigBtn";
            this.EditProConfigBtn.Size = new System.Drawing.Size(128, 23);
            this.EditProConfigBtn.TabIndex = 22;
            this.EditProConfigBtn.Text = "编辑项目配置文件";
            this.EditProConfigBtn.UseVisualStyleBackColor = true;
            this.EditProConfigBtn.Click += new System.EventHandler(this.EditProjectConfigBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "改完重启工具生效";
            // 
            // CheckClient
            // 
            this.CheckClient.AutoSize = true;
            this.CheckClient.Location = new System.Drawing.Point(148, 300);
            this.CheckClient.Name = "CheckClient";
            this.CheckClient.Size = new System.Drawing.Size(204, 16);
            this.CheckClient.TabIndex = 24;
            this.CheckClient.Text = "客户端开发，只导客户端相关文件";
            this.CheckClient.UseVisualStyleBackColor = true;
            this.CheckClient.CheckedChanged += new System.EventHandler(this.CheckClient_CheckedChanged);
            // 
            // CheckServer
            // 
            this.CheckServer.AutoSize = true;
            this.CheckServer.Location = new System.Drawing.Point(379, 300);
            this.CheckServer.Name = "CheckServer";
            this.CheckServer.Size = new System.Drawing.Size(204, 16);
            this.CheckServer.TabIndex = 25;
            this.CheckServer.Text = "服务端开发，只导服务端相关文件";
            this.CheckServer.UseVisualStyleBackColor = true;
            this.CheckServer.CheckedChanged += new System.EventHandler(this.CheckServer_CheckedChanged);
            // 
            // ProjectSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 354);
            this.Controls.Add(this.CheckServer);
            this.Controls.Add(this.CheckClient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EditProConfigBtn);
            this.Controls.Add(this.ClientHotDirBtn);
            this.Controls.Add(this.ClientHotDirTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProjecPaneltList);
            this.Controls.Add(this.ProtoDirBtn);
            this.Controls.Add(this.ProtoDirTxt);
            this.Controls.Add(this.ConfigDirBtn);
            this.Controls.Add(this.ConfigDirTxt);
            this.Controls.Add(this.ServerDirBtn);
            this.Controls.Add(this.ServerDirTxt);
            this.Controls.Add(this.ClientDirBtn);
            this.Controls.Add(this.ProjectDirBtn);
            this.Controls.Add(this.ClientDirTxt);
            this.Controls.Add(this.ProjectDirTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProjectSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ProjectSelectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ProjectDirTxt;
        private System.Windows.Forms.TextBox ClientDirTxt;
        private System.Windows.Forms.Button ProjectDirBtn;
        private System.Windows.Forms.Button ClientDirBtn;
        private System.Windows.Forms.Button ServerDirBtn;
        private System.Windows.Forms.TextBox ServerDirTxt;
        private System.Windows.Forms.Button ConfigDirBtn;
        private System.Windows.Forms.TextBox ConfigDirTxt;
        private System.Windows.Forms.Button ProtoDirBtn;
        private System.Windows.Forms.TextBox ProtoDirTxt;
        private System.Windows.Forms.Panel ProjecPaneltList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClientHotDirBtn;
        private System.Windows.Forms.TextBox ClientHotDirTxt;
        private System.Windows.Forms.Button EditProConfigBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckClient;
        private System.Windows.Forms.CheckBox CheckServer;
    }
}