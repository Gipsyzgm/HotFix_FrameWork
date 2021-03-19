using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace Tools
{
    public partial class ProjectSelectForm : Form
    {
        public ProjectSelectForm()
        {
            InitializeComponent();
        }
        //默认选择项目ID
        private int DefSelectId = 0;
        private void ProjectSelectForm_Load(object sender, EventArgs e)
        {
            DefSelectId = ToolsCookieHelper.Config.LastSelectProjectId;
            List<ProjectSetting> list = Glob.settingMgr.Select<ProjectSetting>();
            ProjectSetting setting;
            for (int i = 0; i < list.Count; i++)
            {
                setting = list[i];
                Button btn = new Button();
                btn.Text = setting.Name;
                btn.Click += new EventHandler(this.SelectProject_ClickEvent);
                btn.Name = "btn_" + setting.Id;
                btn.Size = new System.Drawing.Size(115, 38);
                btn.Location = new System.Drawing.Point(i % 6 * 121, i / 6 * 41);
                this.ProjecPaneltList.Controls.Add(btn);
            }
            if (DefSelectId == 0)
                DefSelectId = 1;
            SetSelectProject(DefSelectId);
            CheckClient.Checked = ToolsCookieHelper.Config.IsClientDev;
            CheckServer.Checked = ToolsCookieHelper.Config.IsServerDev;
        }

        private void SelectProject_ClickEvent(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            SetSelectProject(Convert.ToInt32(btn.Name.Replace("btn_", "")));
        }

        private void SetSelectProject(int id)
        {
            ProjectSetting setting = null;
            foreach (Control cont in this.ProjecPaneltList.Controls)
            {
                if (cont.Name == "btn_" + id)
                {
                    cont.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
                    Glob.SetProject(id);
                    setting = Glob.projectSetting;
                }
                else
                    cont.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }
            if (setting != null)
            {
                ToolsCookieHelper.Config.LastSelectProjectId = id;
                ToolsCookieHelper.Save();
                this.ProjectDirTxt.Text = setting.ProjectDir;
                this.ClientDirTxt.Text = setting.RealityClientDir;
                this.ClientHotDirTxt.Text = setting.RealityClientHotDir;
                this.ServerDirTxt.Text = setting.RealityServerDir;
                this.ConfigDirTxt.Text = setting.RealityConfigDir;
                this.ProtoDirTxt.Text = setting.RealityProtoDir;
            }
            else
            {
                this.ProjectDirTxt.Text = string.Empty;
                this.ClientDirTxt.Text = string.Empty;
                this.ClientHotDirTxt.Text = string.Empty;
                this.ServerDirTxt.Text = string.Empty;
                this.ConfigDirTxt.Text = string.Empty;
                this.ProtoDirTxt.Text = string.Empty;
                Main.SetToolsTitle();
            }
        }
        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }
        /// <summary>
        /// 编辑项目配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProjectConfigBtn_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Setting/ProjectSetting.json");  
            System.Diagnostics.Process.Start(path);
        }

        private void CheckClient_CheckedChanged(object sender, EventArgs e)
        {
            ToolsCookieHelper.Config.IsClientDev = CheckClient.Checked;
            ToolsCookieHelper.Save();
            Main.SetToolsTitle();
        }

        private void CheckServer_CheckedChanged(object sender, EventArgs e)
        {
            ToolsCookieHelper.Config.IsServerDev = CheckServer.Checked;
            ToolsCookieHelper.Save();
            Main.SetToolsTitle();
        }
    }
}

