using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using HotFix.Net;

namespace HotFix
{
    public class MsgWaiting
    {
        //等待的消息列表
        static HashSet<int> WaitProtocalList = new HashSet<int>();
        private static GameObject m_waiting;
        private static Transform m_waitingAnim;
        private static int checkTimerId = 0;
        private static int checkTimeTotal = 0;
        private static GameObject Waiting
        {
            get
            {
                if (m_waiting == null)
                {
                    m_waiting = MainMgr.UI.UIRoot.transform.Find("MsgWaiting").gameObject;
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
                if (WaitProtocalList.Contains(scProtocol))
                    return false;
                WaitProtocalList.Add(scProtocol);
                Waiting.SetActive(true);
                m_waitingAnim.localScale = Vector3.zero;
                m_waitingAnim.DOScale(1, 0.1f).SetDelay(2f);
                checkTimeTotal = 0;
                if (checkTimerId == 0)
                {
                    checkTimerId = HotMgr.Timer.Loop(1, checkUpdate);
                }
            }
            else 
            {
                Debug.Log($"消息列表不包含消息{csProtocol}");
            }
            return true;
        }
        //收到消算,移除等待状态
        public static void Close(ushort scProtocol)
        {
            if (!ClientToGameClientProtocol.Instance.SCList.ContainsKey(scProtocol)) return;
            WaitProtocalList.Remove(scProtocol);
            if (WaitProtocalList.Count == 0)
            {
                m_waitingAnim?.DOKill();
                Waiting.SetActive(false);
            }
        }

        public static void checkUpdate()
        {
            if (checkTimeTotal++ > 15)
            {
                //超过15秒有未回的消息，走重登
                if (WaitProtocalList.Count > 0)
                {
                    string str = string.Empty;
                    foreach (ushort protocal in WaitProtocalList)
                        str += protocal + ";";
                    Debug.Log("Waiting Protocal:" + str);
                    HotMgr.Net.Close(true);
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
                Waiting.SetActive(false);
            }
            WaitProtocalList.Clear();
            checkTimerId = 0;
        }
    }
}
