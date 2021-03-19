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
            this.txtClientDir = new System.Windows.Forms.TextBox();
            this.txtConfigDir = new System.Windows.Forms.TextBox();
            this.txtServerDir = new System.Windows.Forms.TextBox();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.btnConfigDir = new System.Windows.Forms.Button();
            this.btnClientDir = new System.Windows.Forms.Button();
            this.btnServerDir = new System.Windows.Forms.Button();
            this.btnServerOutDir = new System.Windows.Forms.Button();
            this.txtServerOutDir = new System.Windows.Forms.TextBox();
            this.btnClientOutDir = new System.Windows.Forms.Button();
            this.txtClientOutDir = new System.Windows.Forms.TextBox();
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
            this.btnExport.Location = new System.Drawing.Point(729, 348);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(179, 60);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "一键导出配置文件";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtClientDir
            // 
            this.txtClientDir.Location = new System.Drawing.Point(498, 46);
            this.txtClientDir.Name = "txtClientDir";
            this.txtClientDir.ReadOnly = true;
            this.txtClientDir.Size = new System.Drawing.Size(393, 21);
            this.txtClientDir.TabIndex = 2;
            // 
            // txtConfigDir
            // 
            this.txtConfigDir.Location = new System.Drawing.Point(107, 10);
            this.txtConfigDir.Name = "txtConfigDir";
            this.txtConfigDir.ReadOnly = true;
            this.txtConfigDir.Size = new System.Drawing.Size(247, 21);
            this.txtConfigDir.TabIndex = 4;
            // 
            // txtServerDir
            // 
            this.txtServerDir.Location = new System.Drawing.Point(498, 159);
            this.txtServerDir.Name = "txtServerDir";
            this.txtServerDir.ReadOnly = true;
            this.txtServerDir.Size = new System.Drawing.Size(393, 21);
            this.txtServerDir.TabIndex = 6;
            // 
            // listFiles
            // 
            this.listFiles.FormattingEnabled = true;
            this.listFiles.ItemHeight = 12;
            this.listFiles.Location = new System.Drawing.Point(12, 37);
            this.listFiles.Name = "listFiles";
            this.listFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listFiles.Size = new System.Drawing.Size(342, 364);
            this.listFiles.TabIndex = 8;
            // 
            // btnConfigDir
            // 
            this.btnConfigDir.Location = new System.Drawing.Point(13, 10);
            this.btnConfigDir.Name = "btnConfigDir";
            this.btnConfigDir.Size = new System.Drawing.Size(88, 23);
            this.btnConfigDir.TabIndex = 9;
            this.btnConfigDir.Text = "配置文件目录";
            this.btnConfigDir.UseVisualStyleBackColor = true;
            this.btnConfigDir.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // btnClientDir
            // 
            this.btnClientDir.Location = new System.Drawing.Point(379, 44);
            this.btnClientDir.Name = "btnClientDir";
            this.btnClientDir.Size = new System.Drawing.Size(113, 23);
            this.btnClientDir.TabIndex = 10;
            this.btnClientDir.Text = "客户端目录";
            this.btnClientDir.UseVisualStyleBackColor = true;
            this.btnClientDir.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // btnServerDir
            // 
            this.btnServerDir.Location = new System.Drawing.Point(379, 157);
            this.btnServerDir.Name = "btnServerDir";
            this.btnServerDir.Size = new System.Drawing.Size(113, 23);
            this.btnServerDir.TabIndex = 11;
            this.btnServerDir.Text = "服务端目录";
            this.btnServerDir.UseVisualStyleBackColor = true;
            this.btnServerDir.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // btnServerOutDir
            // 
            this.btnServerOutDir.Location = new System.Drawing.Point(379, 190);
            this.btnServerOutDir.Name = "btnServerOutDir";
            this.btnServerOutDir.Size = new System.Drawing.Size(113, 23);
            this.btnServerOutDir.TabIndex = 32;
            this.btnServerOutDir.Text = "服务端配置导出目录";
            this.btnServerOutDir.UseVisualStyleBackColor = true;
            this.btnServerOutDir.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // txtServerOutDir
            // 
            this.txtServerOutDir.Location = new System.Drawing.Point(498, 192);
            this.txtServerOutDir.Name = "txtServerOutDir";
            this.txtServerOutDir.ReadOnly = true;
            this.txtServerOutDir.Size = new System.Drawing.Size(393, 21);
            this.txtServerOutDir.TabIndex = 31;
            // 
            // btnClientOutDir
            // 
            this.btnClientOutDir.Location = new System.Drawing.Point(379, 75);
            this.btnClientOutDir.Name = "btnClientOutDir";
            this.btnClientOutDir.Size = new System.Drawing.Size(113, 23);
            this.btnClientOutDir.TabIndex = 34;
            this.btnClientOutDir.Text = "客户端配置导出目录";
            this.btnClientOutDir.UseVisualStyleBackColor = true;
            this.btnClientOutDir.Click += new System.EventHandler(this.SelctFolder_ClickEvent);
            // 
            // txtClientOutDir
            // 
            this.txtClientOutDir.Location = new System.Drawing.Point(498, 77);
            this.txtClientOutDir.Name = "txtClientOutDir";
            this.txtClientOutDir.ReadOnly = true;
            this.txtClientOutDir.Size = new System.Drawing.Size(393, 21);
            this.txtClientOutDir.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(382, 12);
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
            this.label3.Location = new System.Drawing.Point(382, 128);
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
            this.label1.Location = new System.Drawing.Point(382, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 22);
            this.label1.TabIndex = 41;
            this.label1.Text = "版本检测文字导出:";
            // 
            // txtVerLangFile
            // 
            this.txtVerLangFile.Location = new System.Drawing.Point(441, 262);
            this.txtVerLangFile.Name = "txtVerLangFile";
            this.txtVerLangFile.ReadOnly = true;
            this.txtVerLangFile.Size = new System.Drawing.Size(390, 21);
            this.txtVerLangFile.TabIndex = 39;
            // 
            // txtVerLangOutFile
            // 
            this.txtVerLangOutFile.Location = new System.Drawing.Point(438, 291);
            this.txtVerLangOutFile.Name = "txtVerLangOutFile";
            this.txtVerLangOutFile.ReadOnly = true;
            this.txtVerLangOutFile.Size = new System.Drawing.Size(393, 21);
            this.txtVerLangOutFile.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(384, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "文件路径";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(384, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "导出路径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(589, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "此文件不能热更";
            // 
            // btnExportVerLang
            // 
            this.btnExportVerLang.Location = new System.Drawing.Point(836, 260);
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
            this.ClientSize = new System.Drawing.Size(920, 420);
            this.Controls.Add(this.btnExportVerLang);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVerLangOutFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVerLangFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClientOutDir);
            this.Controls.Add(this.txtClientOutDir);
            this.Controls.Add(this.btnServerOutDir);
            this.Controls.Add(this.txtServerOutDir);
            this.Controls.Add(this.btnServerDir);
            this.Controls.Add(this.btnClientDir);
            this.Controls.Add(this.btnConfigDir);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.txtServerDir);
            this.Controls.Add(this.txtConfigDir);
            this.Controls.Add(this.txtClientDir);
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
        private System.Windows.Forms.TextBox txtClientDir;
        private System.Windows.Forms.TextBox txtConfigDir;
        private System.Windows.Forms.TextBox txtServerDir;
        private System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Button btnConfigDir;
        private System.Windows.Forms.Button btnClientDir;
        private System.Windows.Forms.Button btnServerDir;
        private System.Windows.Forms.Button btnServerOutDir;
        private System.Windows.Forms.TextBox txtServerOutDir;
        private System.Windows.Forms.Button btnClientOutDir;
        private System.Windows.Forms.TextBox txtClientOutDir;
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