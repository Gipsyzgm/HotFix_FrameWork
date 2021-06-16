using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CSF.Tasks;
namespace HotFix_Archer.Effect
{
    public class BaseEffect
    {
        private static readonly object obj = new object();
        static int _identityUID = 0;

        public int UID = 0;
        public EffectConfig Config;
        protected Transform m_parent = null;
        protected Vector3 m_pos;
        protected float m_sclae = 1;
        public GameObject gameObject;
        protected bool m_isActive = true;
        protected bool m_isDispose = false;
        public Action<BaseEffect> onComplete;  //播放完成事件


        protected CTaskHandle loadHandle;
        protected int startTimer = 0;
        public BaseEffect(Transform parent)
        {
            m_parent = parent;
        }

        public BaseEffect(EffectConfig config, Vector3 pos, Transform parent)
        {
            lock (obj)
                UID = ++_identityUID;
            Config = config;
            m_pos = pos;
            m_parent = parent;
            m_sclae = 1;
            Start();
        }

        public virtual void RePlay(Vector3 pos, Transform parent)
        {
            lock (obj)
                UID = ++_identityUID;
            m_pos = pos;
            m_parent = parent;
            m_isDispose = false;
            gameObject.transform.SetParent(m_parent, false);
            SetActive(true);
            gameObject.transform.localPosition = m_pos;
            SetComponent();
            m_sclae = 1;
            Start();
        }


        public void SetActive(bool value)
        {
            if (gameObject != null)
                gameObject.SetActive(value);
            m_isActive = value;
        }

        /// <summary>
        /// 开始播放特效
        /// </summary>
        public void Start()
        {
            if (gameObject == null)
                loadHandle = Load().Run();

            if (Config.duration>0) //有持续时间的自动停止
            {
                startTimer = Mgr.Timer.Once(Config.duration, () => {
                    Stop();
                    onComplete?.Invoke(this);
                    startTimer = 0;
                });
            }
        }

        /// <summary>
        /// 停止播放特效
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        /// <summary>
        /// 设置LocalPostion
        /// </summary>
        /// <param name="pos"></param>
        public void SetPosition(Vector3 pos)
        {
            m_pos = pos;
            if(gameObject != null)
                gameObject.transform.localPosition = m_pos;            
        }

        public void SetScale(float scale)
        {
            m_sclae = scale;
            if (gameObject != null)
                gameObject.transform.localScale = Vector3.one* m_sclae;
        }

        /// <summary>加载特效</summary>
        async CTask Load()
        {
            gameObject = await CSF.Mgr.Assetbundle.LoadPrefab("Effect/" + Config.res);
            gameObject.transform.SetParent(m_parent, false);
            gameObject.transform.localPosition = m_pos;
            SetComponent();
            gameObject.SetActive(m_isActive);
        }


        protected virtual void SetComponent()
        {
            SetScale(m_sclae);
            PlaySound();
        }
        //播放特效声音
        public bool PlaySound()
        {
            if (Config.audio != string.Empty)
            {
                Mgr.Sound.PlaySound(Config.audio);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 等待特结束
        /// </summary>
        /// <returns></returns>
        public virtual async CTask AwaitEnd()
        {
            await CTask.WaitUntil(() => { return m_isDispose; });
        }


        /// <summary>
        /// 等待特结束
        /// </summary>
        /// <returns></returns>
        public virtual async CTask Await()
        {
            await CTask.WaitUntil(() => { return gameObject!=null || m_isDispose; });
        }

        public virtual void Dispose()
        {
            m_isDispose = true;
            loadHandle.Stop();
            if (startTimer != 0)
                Mgr.Timer.Stop(startTimer);
            if (gameObject != null)
            {
                //GameObject.DestroyImmediate(gameObject);
                SetActive(false);
                Mgr.Effect.Release(this);
            }
        }

    }
}
