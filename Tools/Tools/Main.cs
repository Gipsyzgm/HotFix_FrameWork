using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace Tools
{
    public partial class Main : Form
    {
        private static Main _instance;
        public Main()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            ToolsCookieHelper.Load();
            Glob.Initialize();
        }

        private List<TabPage> tabPages = new List<TabPage>();
        public delegate void UpdateLogTxt(string msg, Color color);
        private UpdateLogTxt _updateLogTxt;
        private void Main_Load(object sender, EventArgs e)
        {
            Logger.MainForm = this;
            Glob.SetProject(ToolsCookieHelper.Config.LastSelectProjectId);

            tabControl.SelectedIndex = Math.Min(ToolsCookieHelper.Config.LastSelectTabIndex, tabControl.TabCount);

            if (Glob.projectSetting == null || Glob.codeOutSetting == null)
            {
                Logger.LogError("项目配置错误!!!!");
                tabControl.SelectedIndex = 0;
            }

            OpenProtoExport(tabControl.SelectedTab);
            _instance = this;
            _updateLogTxt = new UpdateLogTxt(UpdateLogTxtMethod);

            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                tabPages.Add(tabControl.TabPages[i]);
            }
            SetToolsTitle();
            SetLogTypeCheck();
        }

     

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Glob.projectSetting == null || Glob.codeOutSetting == null)
            {
                Logger.LogError("项目配置错误!!!!,请修改配置或更换项目!!");
                return;
            }
            OpenProtoExport(tabControl.SelectedTab);
            ToolsCookieHelper.Config.LastSelectTabIndex = tabControl.SelectedIndex;
            ToolsCookieHelper.Save();

        }
        public static void SetToolsTitle()
        {
            if (_instance != null)
            {
                string title = "::游戏工具::";
                if (Glob.projectSetting != null)
                    title += Glob.projectSetting.Name;
                title += $"         [{ToolsCookieHelper.GetDevName()}]开发导出";
                _instance.Text = title;
                bool isClient = ToolsCookieHelper.Config.IsClientDev;
                bool isServer = ToolsCookieHelper.Config.IsServerDev;

                _instance.tabPages[1].Parent = isClient || isServer ? _instance.tabControl : null;  //Excel
                _instance.tabPages[2].Parent = isClient || isServer ? _instance.tabControl : null; //Proto
                _instance.tabPages[3].Parent = isServer ? _instance.tabControl : null; //数据库
                _instance.tabPages[4].Parent = isClient || isServer ? _instance.tabControl : null; //CDKey
                _instance.tabPages[5].Parent = isServer ? _instance.tabControl : null;     //GM
                _instance.tabPages[6].Parent = isServer ? _instance.tabControl : null;  //服务器日志
            }
        }

        //打开page对应的窗口
        private void OpenProtoExport(TabPage pag)
        {
            if (pag.Controls.Count == 0)
            {
                ObjectHandle t = Activator.CreateInstance("Tools", "Tools." + pag.Name);
                Form form = (Form)t.Unwrap();
                form.TopLevel = false;
                form.Parent = pag;
                form.FormBorderStyle = FormBorderStyle.None;
                form.ControlBox = false;
                form.Dock = System.Windows.Forms.DockStyle.Fill;
                form.Show();
            }
            else
            {
                pag.Controls[0].Refresh();
            }
        }
        /// <summary>
        /// 检查设置log类型
        /// </summary>
        private void SetLogTypeCheck()
        {
            ELogType logType = (ELogType)ToolsCookieHelper.Config.LogType;
            Logger.SetLogType(logType);
            //&如果同时存在于两个操作数中，二进制 AND 运算符复制一位到结果中。
            //即0（0000）和4（0100），即为0(0000)
            //如果是4（0100）和4（0100），即为4（0100）
            //计算方式为二进制对应位置分别进行AND预算，求得结果。
            CheckLog.Checked = (logType & ELogType.Normal) == ELogType.Normal;
            CheckLogWarning.Checked = (logType & ELogType.Warning) == ELogType.Warning;
            CheckLogError.Checked = (logType & ELogType.Error) == ELogType.Error;
        }
        /// <summary>
        /// 点击设置log类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckLog_Click(object sender, EventArgs e)
        {
            //| 如果存在于任一操作数中，二进制 OR 运算符复制一位到结果中。
            //即0（0000）和4（0100），即为4(0100)
            //如果是4（0100）和8（1000），即为12（1100）
            //计算方式为二进制对应位置分别进行OR预算，求得结果。
            ELogType logType = ELogType.None;
            if (CheckLog.Checked)
                logType = logType | ELogType.Normal;
            if (CheckLogWarning.Checked)
                logType = logType | ELogType.Warning;
            if (CheckLogError.Checked)
                logType = logType | ELogType.Error;
            Logger.SetLogType(logType);
            ToolsCookieHelper.Config.LogType = (int)logType;
            ToolsCookieHelper.Save();
        }

        //每次输出Log调起事件更新事件UpdateLogTxtMethod
        public void Log(string str, Color color)
        {
            if (LogTxt == null) return;
            if (LogTxt.InvokeRequired)
            {
                BeginInvoke(_updateLogTxt, str, color);
            }
            else
            {
                UpdateLogTxtMethod(str, color);
            }
        }

        public void UpdateLogTxtMethod(string str, Color color)
        {
            if (str == null)    //清空日志
                LogTxt.Text = string.Empty;
            else
            {
                if (LogTxt.Text != string.Empty)
                    str = Environment.NewLine + str;
                LogTxt.SelectionStart = LogTxt.TextLength;
                LogTxt.SelectionLength = 0;
                LogTxt.SelectionColor = color;
                LogTxt.AppendText(str);
                LogTxt.SelectionColor = LogTxt.ForeColor;
                LogTxt.Focus();
            }
        }

        private void CleanAllBtn_Click(object sender, EventArgs e)
        {
            Logger.Clean();
        }
    }
}
