using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
using static EventListener;

namespace HotFix
{
    public static class UIExtension
    {
        #region Image扩展
        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <param name="uiAtlas">UIAtlas</param>
        //public static async CTask SetSprite(this Image img, string spriteName, string uiAtlas = UIAtlas.ItemIcon, bool autoSetSize = false)
        //{
        //    if (img == null) return;
        //    SpriteAtlas atlas = await CSF.Mgr.Assetbundle.LoadSpriteAtlas(uiAtlas);
        //    if (img == null) return;
        //    img.sprite = atlas.GetSprite(spriteName);
        //    if (img.sprite == null && uiAtlas == UIAtlas.ItemIcon) //使用默认头像
        //        img.sprite = atlas.GetSprite("Default");
        //    if (autoSetSize)
        //    {
        //        img.SetNativeSize();
        //    }
        //}
        ///// <summary>
        ///// 加载贴图
        ///// </summary>
        ///// <param name="img"></param>
        ///// <param name="imgName"></param>
        ///// <param name="imgName">IsAutoShow</param>
        //public static async CTask SetSpriteRenderer(this SpriteRenderer img, string spriteName, string uiAtlas = UIAtlas.ItemIcon)
        //{
        //    SpriteAtlas atlas = await CSF.Mgr.Assetbundle.LoadSpriteAtlas(uiAtlas);
        //    if (img == null) return;
        //    img.sprite = atlas.GetSprite(spriteName);
        //}

        /// <summary>
        /// 加载贴图
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgName"></param>
        /// <param name="imgName">IsAutoShow</param>
        public static async CTask SetTextures(this RawImage img, string imgName)
        {
            if (img == null) return;
            Texture tex = await Addressables.LoadAssetAsync<Texture>(imgName).Task;
            if (img == null) return;
            img.texture = tex;
        }
        /// <summary>
        /// 设置图片透明度
        /// </summary>
        /// <param name="img"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha<T>(this T img, float alpha) where T : MaskableGraphic
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
        }
        #endregion

        #region 透明区域不可点击

        public static void AlphaUnClick(this Button btn)
        {
            btn.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.3f;
        }

        #endregion

        #region 变灰
        private static Material grayMaterial;
        /// <summary>
        /// 设置图片变灰
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isGray">是否变灰,false/还原</param>
        public static void SetGray(this Image img, bool isGray = true)
        {
            if (isGray)
            {
                if (grayMaterial == null)
                    grayMaterial = Resources.Load<Material>("Materials/UIGray");
                img.material = grayMaterial;
            }
            else
                img.material = null;
        }
        public static void SetGray(this Button btn, bool isGray = true)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.SetGray(isGray);
            Image[] imgChild = btn.GetComponentsInChildren<Image>();
            for (int i = 0; i < imgChild.Length; i++)
            {
                imgChild[i].SetGray(isGray);
            }
        }
        #endregion

        #region 闪烁
        private static Material flashMaterial;
        /// <summary>
        /// 设置图片闪烁
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isflash">是否闪烁,false/还原</param>
        public static void SetFlash(this Image img, bool isflash = true)
        {
            if (isflash)
            {
                if (flashMaterial == null)
                    flashMaterial = Resources.Load<Material>("Materials/UIFlash");
                img.material = flashMaterial;
            }
            else
                img.material = null;
        }
        /// <summary>
        /// 设置图片闪烁
        /// </summary>
        /// <param name="img"></param>
        /// <param name="isflash">是否闪烁,false/还原</param>
        public static void SetFlash(this SpriteRenderer img, bool isflash = true)
        {
            if (isflash)
            {
                if (flashMaterial == null)
                    flashMaterial = Resources.Load<Material>("Materials/UIFlash");
                img.material = flashMaterial;
            }
            else
                img.material = null;
        }
        #endregion

        #region UI相关释放

        /// <summary>
        /// 释放UI上的Item列表
        /// </summary>
        public static void Dispose<T>(this List<T> list) where T : BaseItem
        {
            for (int i = 0; i < list.Count; i++)
                list[i].Dispose();
            list.Clear();
        }
        #endregion

        #region 获取子对象
        /// <summary>
        /// 获取GameObject子对像，不包含自己
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static List<Transform> GetChildrenTransform(this GameObject tran)
        {
            List<Transform> list = new List<Transform>();
            foreach (Transform child in tran.transform)
                list.Add(child);
            return list;
        }
        #endregion
    }
}