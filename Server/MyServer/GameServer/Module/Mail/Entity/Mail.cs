using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class Mail : BaseEntity<TMail>
    {
        /// <summary>所属玩家Id</summary>
        public ObjectId PlayerId => Data.pId;

        /// <summary>所属玩家</summary>
        public Player player { get; protected set; }

        /// <summary>
        /// 邮件类型
        /// </summary>
        public MailType Type { get; }

        /// <summary>
        /// 邮件是否已经打开过(已读)
        /// </summary>
        public bool IsOpen()
        {
            if (Type == MailType.Person)
                return Data.isOpen;
            else
            {
                if (player.MassMailSubList.ContainsKey(Data.id))
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 邮件是否已经领取过
        /// </summary>
        public bool IsGet(ObjectId pid)
        {
            if (Type == MailType.Person)
                return Data.isGet;
            else
            {
                if (player.MassMailSubList.ContainsKey(Data.id))
                    return player.MassMailSubList[Data.id].isGet;
                return false;
            }
        }

        /// <summary>
        /// 邮件是否已删除(群邮件)
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool IsDel()
        {
            if (player.MassMailSubList.ContainsKey(Data.id))
                return player.MassMailSubList[Data.id].isDel;
            return false;
        }

        /// <summary>
        /// 邮件是否过期
        /// </summary>
        public bool IsOverdue
        {
            get
            {
                if ((DateTime.Now - (DateTime)Data.sTime).TotalDays > Glob.config.settingConfig.MailOverdueDay)
                    return true;
                return false;
            }
        }

        public Mail(Player _p, TMail data)
        {
            Data = data;
            player = _p;
            Type = (MailType)Data.type;
        }
        

    }
}
