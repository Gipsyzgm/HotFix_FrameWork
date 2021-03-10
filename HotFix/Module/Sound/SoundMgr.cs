using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace HotFix
{

    public class SoundMgr
    {
        private GameObject gameObject;
        //private int bgMusicIndex = 0;
        //背景音乐名称集合，用于随机背景音乐
        private string[] bgMusicList = new string[] { };
        private AudioSource LoopSource;
        private AudioSource OneShotSource;
        private bool _playEffect = true;

        /// <summary>
        /// 播放/关闭音效
        /// </summary>
        public bool isPlayEffect
        {
            set
            {
                _playEffect = value;
                OneShotSource.volume = value ? 1 : 0;
                PlayerPrefs.SetInt("AudioEffect", value ? 0 : 1);
                PlayerPrefs.Save();
            }
            get => _playEffect;
        }
        private bool _playMusic = true;
        /// <summary>
        /// 播放/关闭背景音乐
        /// </summary>
        public bool isPlayMusic
        {
            set
            {
                _playMusic = value;
                LoopSource.volume = value ? 1 : 0;
                PlayerPrefs.SetInt("AudioMusic", value ? 0 : 1);
                PlayerPrefs.Save();
            }
            get => _playMusic;
        }
        /// <summary>
        /// 声音管理器
        /// </summary>
        public SoundMgr()
        {
            gameObject = new GameObject("__SoundMgr");
            GameObject.DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<AudioListener>();

            if (LoopSource == null)
            {
                LoopSource = gameObject.AddComponent<AudioSource>();
                LoopSource.loop = true;
                LoopSource.playOnAwake = false;
            }
            if (OneShotSource == null)
            {
                OneShotSource = gameObject.AddComponent<AudioSource>();
                OneShotSource.playOnAwake = false;
            }

            isPlayEffect = PlayerPrefs.GetInt("AudioEffect") == 0;
            isPlayMusic = PlayerPrefs.GetInt("AudioMusic") == 0;
        }
        //HashSet
        //这个类代表一组值。
        //这个类提供了高性能的操作。
        //这是一组不包含重复元素的集合，并且其中存储的元素没有特定的顺序。
        //在.NET Framework 4.6版本中，HashSet 实现IReadOnlyCollection 界面连同ISet 接口。
        //哈希集类对其中存储的元素数量没有任何最大容量。随着元素数量的增加，这种容量不断增加。

        protected HashSet<string> playingSounds = new HashSet<string>();
        /// <summary>
        /// 播放声音，同一个声音名0.1秒内只播放一次
        /// </summary>
        /// <param name="audioName"></param>
        public void PlaySound(string audioName)
        {
            if (audioName == "Click")
            {
                playingSounds.Remove(audioName);
            }
            if (playingSounds.Contains(audioName))
            {
                return;
            }
            playingSounds.Add(audioName);
            playSound(audioName).Run();
        }
        private async CTask playSound(string audioName)
        {
            AudioClip clip = await Addressables.LoadAssetAsync<AudioClip>(audioName).Task;
            if (OneShotSource == null)
                OneShotSource = gameObject.AddComponent<AudioSource>();
            OneShotSource.PlayOneShot(clip);
            await CTask.WaitForSeconds(0.1f);
            playingSounds.Remove(audioName);
        }
        //播放背景音乐 登陆完成播放背景-战斗结束播放
        public void PlayMusic()
        {
            if (bgMusicList.Length == 0)
            {
                if (LoopSource.isPlaying) LoopSource.Stop();
                return;
            }
            //随机一个背景音乐索引
            System.Random rd = new System.Random();
            int index = rd.Next(0, bgMusicList.Length);
            if (index > bgMusicList.Length)
            {
                index = 0;
                Debug.Log("背景音乐随机数大于背景音乐数据--" + index + "  当前数组大小  " + bgMusicList.Length);
            }
            string bkgName = bgMusicList[index];
            //播放背景音乐
            if (LoopSource.isPlaying) LoopSource.Stop();
            playLoopAudio(bkgName).Run();
        }
        //播放战斗背景音乐
        public void PlayMusic(string audioName)
        {
            playLoopAudio(audioName).Run();
        }
        private async CTask playLoopAudio(string bkgName)
        {
            if (LoopSource.isPlaying) LoopSource.Stop();
            LoopSource.clip = await Addressables.LoadAssetAsync<AudioClip>(bkgName).Task;
            LoopSource.Play();
        }
        //停止播放背景音乐，战斗开始停止播放-登陆界面停止播放
        public void StopBkgMusic()
        {
            LoopSource.Stop();
        }
        public void Dispose()
        {

        }
    }
}