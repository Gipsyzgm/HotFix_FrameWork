namespace Tools
{
    partial class LogExportForm
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
            this.ServerPathTxt = new System.Windows.Forms.TextBox();
            this.ServerPathBtn = new System.Windows.Forms.Button();
            this.DBPathTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExportBtn
            // 
            this.ExportBtn.Location = new System.Drawing.Point(12, 142);
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new System.Drawing.Size(162, 67);
            this.ExportBtn.TabIndex = 1;
            this.ExportBtn.Text = "生成日志结构类";
            this.ExportBtn.UseVisualStyleBackColor = true;
            this.ExportBtn.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // SelectDBPathBtn
            // 
            this.SelectDBPathBtn.Location = new System.Drawing.Point(12, 33);
            this.SelectDBPathBtn.Name = "SelectDBPathBtn";
            this.SelectDBPathBtn.Size = new System.Drawing.Size(128, 23);
            this.SelectDBPathBtn.TabIndex = 9;
            this.SelectDBPathBtn.Text = "服务器日志Excel文件";
            this.SelectDBPathBtn.UseVisualStyleBackColor = true;
            this.SelectDBPathBtn.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // ServerPathTxt
            // 
            this.ServerPathTxt.Location = new System.Drawing.Point(146, 78);
            this.ServerPathTxt.Name = "ServerPathTxt";
            this.ServerPathTxt.ReadOnly = true;
            this.ServerPathTxt.Size = new System.Drawing.Size(551, 21);
            this.ServerPathTxt.TabIndex = 55;
            // 
            // ServerPathBtn
            // 
            this.ServerPathBtn.Location = new System.Drawing.Point(12, 76);
            this.ServerPathBtn.Name = "ServerPathBtn";
            this.ServerPathBtn.Size = new System.Drawing.Size(128, 23);
            this.ServerPathBtn.TabIndex = 54;
            this.ServerPathBtn.Text = "服务端目录";
            this.ServerPathBtn.UseVisualStyleBackColor = true;
            this.ServerPathBtn.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // DBPathTxt
            // 
            this.DBPathTxt.Location = new System.Drawing.Point(146, 35);
            this.DBPathTxt.Name = "DBPathTxt";
            this.DBPathTxt.ReadOnly = true;
            this.DBPathTxt.Size = new System.Drawing.Size(551, 21);
            this.DBPathTxt.TabIndex = 55;
            // 
            // LogExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 354);
            this.Controls.Add(this.DBPathTxt);
            this.Controls.Add(this.ServerPathTxt);
            this.Controls.Add(this.ServerPathBtn);
            this.Controls.Add(this.SelectDBPathBtn);
            this.Controls.Add(this.ExportBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DBExportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ExportBtn;
        private System.Windows.Forms.Button SelectDBPathBtn;
        private System.Windows.Forms.TextBox ServerPathTxt;
        private System.Windows.Forms.Button ServerPathBtn;
        private System.Windows.Forms.TextBox DBPathTxt;
    }
}