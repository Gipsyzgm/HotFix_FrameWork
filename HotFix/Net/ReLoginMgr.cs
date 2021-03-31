using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using HotFix_MK.Net;
using HotFix_MK.Module.Login.DataMgr;
using CSF.Tasks;
using UnityEngine.UI;

namespace HotFix_MK
{
    public class ReLoginMgr
    {
        private static GameObject m_connecting;
        //private static CanvasGroup canvasGroup;
        public static bool IsReLogin = false;
        private static GameObject connecting
        {
            get
            {
                if (m_connecting == null)
                {
                    m_connecting = CSF.Mgr.UI.UIRoot.transform.Find("Connecting").gameObject;
                    //m_connecting.GetComponentInChildren<Text>().text = Mgr.Lang.Get("Com_Connecting");
                    //canvasGroup = m_connecting.GetComponentInChildren<CanvasGroup>();
                }
                return m_connecting;
            }
        }

        public static void Show()
        {
            if (connecting.IsVisible()) return;
            connecting.SetVisible(true);
            MsgWaiting.CleanAll();
            //canvasGroup.alpha = 0;
            //canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear).SetAutoKill(true);

            //半个小时没更新心跳，直接断开连接
            if (DateTime.Now.ToTimestamp() - Mgr.Time.LastUpdateServerTimestamp > 1800)
                ReLoginFail();
            else
                ReLogin().Run();
           
        }
        private static async CTask ReLogin() 
        {
            for(int i=0;i<5;i++)
            {
                await CTask.WaitForSeconds(0.5f);
                bool isSucce = await LoginMgr.I.ReLogin();
                if (!isSucce)
                {
                    await CTask.WaitForSeconds(1f);
                    CLog.Log("重连失败，尝试重新连接");
                }
                else
                {
                    return;
                }
            }
            ReLoginFail();
        }
        //收到消算,移除等待状态
        public static void Close()
        {
            //canvasGroup?.DOKill();
            connecting.SetVisible(false);
        }
        //登录成功
        public static void ReLoginSucc()
        {
            Close();
            Mgr.Net.ReLoginSendMessage();
        }

        //登录失败
        public static void ReLoginFail()
        {
            CLog.Log("重录失败了，返回界面重登");
            Mgr.Net.showDisconnectConfirm();
            ReLoginMgr.IsReLogin = false;
        }

    }
}
