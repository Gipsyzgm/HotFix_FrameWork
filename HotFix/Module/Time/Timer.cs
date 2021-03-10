using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HotFix
{
    public class Timer
    {
        private static readonly object obj = new object();
        static int _identityUID = 0;
        /// <summary>
        ///时间间隔
        /// </summary>
        public float m_interval;
        public Action m_action;
        public Action<float> m_actionArg;
        /// <summary>
        /// 循环次数
        /// </summary>
        public int m_num;
        /// <summary>
        /// Timer的唯一ID
        /// </summary>
        public int UID = 0;
        /// <summary>
        /// 是否开始计时
        /// </summary>
        private bool isStart = false;
        /// <summary>
        /// 区分Timer的类型[0：不带参，根据interval执行。 1：不带参数,每帧执行 2：带参数，每帧执行]
        /// </summary>
        private int updateIntervalType = 0; //0不是Update 
        /// <summary>
        /// 用于判定是否达到间隔时长
        /// </summary>
        private float m_TimeTotal = 0;

        public Timer()
        {

        }

        public void Start(float interval, Action action, int num = -1)
        {
            lock (obj)
                UID = ++_identityUID;
            m_interval = interval;
            m_action = action;
            m_num = num;
            isStart = true;
            updateIntervalType = 0;
        }

        public void StartUpdate(Action<float> action)
        {
            lock (obj)
                UID = ++_identityUID;
            isStart = true;
            m_actionArg = action;
            updateIntervalType = 2;
        }
        public void StartUpdate(Action action)
        {
            lock (obj)
                UID = ++_identityUID;
            isStart = true;
            m_action = action;
            updateIntervalType = 1;
        }

        public void Stop()
        {
            isStart = false;
            m_TimeTotal = 0;
        }

        //管理器进行调用
        public void Update(float deltaTime)
        {
            if (!isStart) return;
            if (updateIntervalType == 1)
                m_action?.Invoke();
            if (updateIntervalType == 2)
                m_actionArg?.Invoke(deltaTime);
            else
            {
                m_TimeTotal += deltaTime;
                if (m_TimeTotal > m_interval)
                {
                    m_TimeTotal -= m_interval;
                    m_action?.Invoke();
                    if (m_num > 0)
                    {
                        m_num -= 1;
                        if (m_num == 0)
                        {
                            HotMgr.Timer.Stop(UID);
                            return;
                        }
                    }
                }
            }
        }
    }
}