//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.UI;

//namespace HotFix_Archer.Effect
//{
//    /// <summary>
//    /// 技能特效
//    /// </summary>
//    public class SkillEffect: BaseEffect
//    {
//        SkillConfig skillConfig;
//        protected Transform cameraTran;
//        public SkillEffect(SkillConfig sconfig, Transform parent):base(parent)
//        {
//            skillConfig = sconfig;
//            EffectConfig config;
//            int effectId = 2000;  //默认为冲刺效果
//            if (skillConfig != null)
//                effectId = 2000 + sconfig.type;          
//            Mgr.Config.dicEffect.TryGetValue(effectId, out config);

//            Config = config;
//            m_pos = Vector3.zero;
//            cameraTran = Camera.main.transform;
//        }
       

//        protected override void SetComponent()
//        {
//            Transform canvas = gameObject.transform.Find("Canvas");
//            if (canvas == null) return;
              
//            Text txtTitle = canvas.GetComponentInChildren<Text>();

//            ESkillType type = ESkillType.Burst; //爆发
//            if (skillConfig != null)
//                type = (ESkillType)skillConfig.type;
//            string text = Utils.GetEnumName(type);
//            switch (type)
//            {
//                case ESkillType.EAttackLessenSpeed: //攻击减速
//                    text = string.Format(text, skillConfig.arg[0]);
//                    break;
//                case ESkillType.EAttackLessenPower://攻击减体
//                    text = string.Format(text, skillConfig.arg[1]);
//                    break;
//                case ESkillType.EAssistAddSpeed://辅助加速
//                    text = string.Format(text, skillConfig.arg[0]);
//                    break;
//                case ESkillType.EAssistAddPower://辅助加体
//                    text = string.Format(text, skillConfig.arg[1]);
//                    break;
//            }
//            txtTitle.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
//            txtTitle.text = text;
//        }

     

//        public override void Dispose()
//        {
//            cameraTran = null;
//            base.Dispose();
           
//        }

//    }
//}
