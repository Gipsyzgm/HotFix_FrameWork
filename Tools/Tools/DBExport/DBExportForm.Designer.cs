namespace Tools
{
    partial class DBExportForm
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
            this.txtServerPath = new System.Windows.Forms.TextBox();
            this.btnServerPath = new System.Windows.Forms.Button();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 131);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(162, 67);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "生成数据库结构类";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSelectDBPath
            // 
            this.btnSelectDBPath.Location = new System.Drawing.Point(12, 33);
            this.btnSelectDBPath.Name = "btnSelectDBPath";
            this.btnSelectDBPath.Size = new System.Drawing.Size(128, 23);
            this.btnSelectDBPath.TabIndex = 9;
            this.btnSelectDBPath.Text = "数据库设计Excel文件";
            this.btnSelectDBPath.UseVisualStyleBackColor = true;
            this.btnSelectDBPath.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtServerPath
            // 
            this.txtServerPath.Location = new System.Drawing.Point(146, 78);
            this.txtServerPath.Name = "txtServerPath";
            this.txtServerPath.ReadOnly = true;
            this.txtServerPath.Size = new System.Drawing.Size(479, 21);
            this.txtServerPath.TabIndex = 55;
            // 
            // btnServerPath
            // 
            this.btnServerPath.Location = new System.Drawing.Point(12, 78);
            this.btnServerPath.Name = "btnServerPath";
            this.btnServerPath.Size = new System.Drawing.Size(128, 23);
            this.btnServerPath.TabIndex = 54;
            this.btnServerPath.Text = "服务端目录";
            this.btnServerPath.UseVisualStyleBackColor = true;
            this.btnServerPath.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(146, 35);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.ReadOnly = true;
            this.txtDBPath.Size = new System.Drawing.Size(479, 21);
            this.txtDBPath.TabIndex = 55;
            // 
            // DBExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 420);
            this.Controls.Add(this.txtDBPath);
            this.Controls.Add(this.txtServerPath);
            this.Controls.Add(this.btnServerPath);
            this.Controls.Add(this.btnSelectDBPath);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DBExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DBExportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSelectDBPath;
        private System.Windows.Forms.TextBox txtServerPath;
        private System.Windows.Forms.Button btnServerPath;
        private System.Windows.Forms.TextBox txtDBPath;
    }
}