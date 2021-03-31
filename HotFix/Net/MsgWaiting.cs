using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using HotFix_MK.Net;
using UnityEngine.UI;

namespace HotFix_MK
{
    public class MsgWaiting
    {
        static HashSet<int> waitProtocalList = new HashSet<int>();
        private static GameObject m_waiting;
        private static Transform m_waitingAnim;
        //private static CanvasGroup canvasGroup;
        private static int checkTimerId = 0;
        private static int checkTimeTotal = 0;
        private static GameObject waiting
        {
            get
            {
                if (m_waiting == null)
                {
                    m_waiting = CSF.Mgr.UI.UIRoot.transform.Find("MsgWaiting").gameObject;
                    //m_waiting.GetComponentInChildren<Text>().text = Mgr.Lang.Get("Com_Waiting");
                    //canvasGroup = m_waiting.GetComponentInChildren<CanvasGroup>();
                    m_waitingAnim = m_waiting.transform.Find("Waiting");
                }
                return m_waiting;
            }
        }

        //请求消息,判断Wait是否需要显示
        public static bool Show(ushort csProtocol)
        {
            if (ClientToGameClientProtocol.Instance.CSList.TryGetValue(csProtocol, out var scProtocol))
            {
                if (waitProtocalList.Contains(scProtocol))
                    return false;
                waitProtocalList.Add(scProtocol);               
                waiting.SetVisible(true);
                //canvasGroup.alpha = 0;
                //canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear).SetAutoKill(true);

                m_waitingAnim.localScale = Vector3.zero;
                m_waitingAnim.DOScale(1, 0.1f).SetDelay(2f);

                checkTimeTotal = 0;
                if (checkTimerId == 0)
                {
                    checkTimerId = Mgr.Timer.Loop(1, checkUpdate);
                }
            }
            return true;
        }
        //收到消算,移除等待状态
        public static void Close(ushort scProtocol)
        {
            if (!ClientToGameClientProtocol.Instance.SCList.ContainsKey(scProtocol)) return;
            waitProtocalList.Remove(scProtocol);
            if (waitProtocalList.Count == 0)
            {
                //canvasGroup?.DOKill();
                m_waitingAnim?.DOKill();
                waiting.SetVisible(false);
            }
        }

        public static void checkUpdate()
        {
            if (checkTimeTotal++ > 15)
            {
                //超过15秒有未回的消息，走重登
                if (waitProtocalList.Count > 0)
                {
                    string str = string.Empty;
                    foreach (ushort protocal in waitProtocalList)
                        str += protocal + ";";
                    CLog.Error("Waiting Protocal:" + str);
                    Mgr.Net.Close(true);
                }
                checkTimeTotal = 0;
            }
        }

        //收到错误消息,移除等待状态
        public static void ErrorClose(ushort csProtocol)
        {
            if (ClientToGameClientProtocol.Instance.CSList.TryGetValue(csProtocol, out var scProtocol))
            {
                Close(scProtocol);
            }
        }

        public static void CleanAll()
        {
            if (m_waiting != null)
            {
                waiting.SetVisible(false);
            }
            waitProtocalList.Clear();
            checkTimerId = 0;
        }
    }
}
