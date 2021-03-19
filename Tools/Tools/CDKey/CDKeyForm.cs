using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Tools
{
    public partial class CDKeyForm : Form
    {
        CDKeyFactory cdKeyFact;
        CancellationTokenSource cts;
        public CDKeyForm()
        {
            InitializeComponent();
            cdKeyFact  = new CDKeyFactory();
            cts = new CancellationTokenSource();
            this.progressBar.Value = 0;
        }

        int selectIndex = -1;
        bool isCreating = false;
        private void DBExportForm_Load(object sender, EventArgs e)
        {
            this.txtCDKeyExcelPath.Text = Glob.codeOutSetting.CDKey.CDKeyFile.ToReality();
            this.txtSavePath.Text = Glob.codeOutSetting.CDKey.OutFileDir.ToReality();
            SetCDKeyList();
             
        }
        private void SetCDKeyList()
        {
            NoneInfo();
            selectIndex = -1;
            if (this.txtCDKeyExcelPath.Text == string.Empty) return;
            List<string> list = CDKeyHelper.SetCDKeyExcel(this.txtCDKeyExcelPath.Text);
            this.listCDKey.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.listCDKey.Items.Add(list[i]);
            }
            if (list.Count > 0)
                this.listCDKey.SelectedIndex = 0;
        }

        

        /// <summary>
        /// 一键生成所有相关文件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            int id = 0;
            Int32.TryParse(CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Id), out id);
            if (id == 0)
            {
                MessageBox.Show("请选择要导出的CDKey");
                return;
            }

            int num = 0;    //导出数量
            Int32.TryParse(CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Num), out num);
            if (num <= 0)
                return;
            if (num > 999999)
            {
                MessageBox.Show("生成的CDKey过多");
                return;
            }

            
            if (!isCreating)
            {
                isCreating = true;
                this.progressBar.Value = 0;
                this.listCDKey.Enabled = false;
                this.btnExport.Text = "CDKey生成中...\n点击停止生成";

                ConcurrentBag<string> list = new ConcurrentBag<string>();

                try
                {
                    Task tk = new Task(() =>
                    {
                        Parallel.For(0, num, (i, loopState) =>
                        {
                            list.Add(cdKeyFact.CreateCDKey(id, i + 1));
                            this.progressBar.Value = list.Count * 100 / num;
                            if (isCreating == false)
                            {
                                loopState.Stop();
                                return;
                            }
                        });
                        if (isCreating == false)
                        {
                            this.progressBar.Value = 0;
                            Logger.LogWarning("CDKey导出已终止...");
                            return;
                        }
                        this.progressBar.Value = 100;
                        this.btnExport.Text = "生成CDKey";
                        this.listCDKey.Enabled = true;

                        string name = CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Name);
                        CDKeyHelper.SaveCDKeyFile(this.txtSavePath.Text, name, list);
                        Logger.LogAction("CDKey导出完成!");
                        isCreating = false;
                    }, cts.Token);
                    tk.Start();
                }
                catch
                { }

            }
            else
            {                
                this.progressBar.Value = 100;
                this.btnExport.Text = "生成CDKey";
                this.listCDKey.Enabled = true;
                isCreating = false;
            }
            //Task.WaitAll(tk);
        }
        
        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.txtCDKeyExcelPath.Text);
        }

        //选择发生改变
        private void listCDKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectIndex = listCDKey.SelectedIndex;
            if (selectIndex >= 0)
            {
                this.labId.Text = CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Id);
                this.labName.Text = CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Name);
                int useCount = Convert.ToInt32(CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.UseCount));
                this.labUseCount.Text = useCount == 0 ? "无限制" : useCount.ToString();
                this.labNum.Text = CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Num);
                string endTime = CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.EndTime);
                this.labEndTime.Text = endTime == "" ? "无限制" : endTime;
                this.labDes.Text = CDKeyHelper.GetRowFieldValue(selectIndex, CDKeyFieldName.Des);
            }
            else
            {
                NoneInfo();
            }
        }
        private void NoneInfo()
        {
            this.labId.Text = string.Empty;
            this.labName.Text = string.Empty;
            this.labUseCount.Text = string.Empty;
            this.labNum.Text = string.Empty;
            this.labEndTime.Text = string.Empty;
            this.labDes.Text = string.Empty;
        }

        /// <summary>
        /// 检测CDKey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckCDKey_Click(object sender, EventArgs e)
        {
           long num = cdKeyFact.DecodeCDKey(this.txtCDKey.Text);
            if (num == 0)
            {
                this.txtCDKeyResult.Text = "此CDKey无效";
            }
            else
            {
                this.txtCDKeyResult.Text = num.ToString();
            }
        }
    }
}

