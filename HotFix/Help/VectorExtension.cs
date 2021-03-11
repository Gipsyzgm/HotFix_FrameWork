using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HotFix
{
    public static class Vector
    {
        public static Vector3 Vector3Null = new Vector3(-1000, -1000, -1000);

        public static Vector2 Vector2Null = new Vector2(-1000, -1000);

        /// <summary>
        /// 看上目标点的旋转
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Quaternion LookTarget(this Vector3 pos, Vector3 target)
        {
            pos.y = 0; target.y = 0;
            return Quaternion.LookRotation(target - pos);
        }

        /// <summary>
        /// 看向目标点的旋转
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Quaternion LookTarget2D(this Vector2 pos, Vector2 target)
        {
            //二种方法都行
            //transform.right = target.position - transform.position;
            Vector2 dir = target - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
        public static Quaternion LookTarget2DUP(this Vector2 pos, Vector2 target)
        {
            //二种方法都行
            //transform.up = target.position - transform.position;
            Vector2 dir = target - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        public static Vector2 ToVector2(this float[] val)
        {
            if (val.Length < 2)
                return Vector2.zero;
            return new Vector2(val[0], val[1]);
        }

        public static Vector3 ToVector3(this float[] val)
        {
            if (val.Length < 3)
                return Vector3.zero;
            return new Vector3(val[0], val[1], val[2]);
        }
        //2D配置坐标转3D从标,Y设为0
        public static Vector3 ToPostion(this float[] val)
        {
            if (val.Length < 2) return Vector3.zero;
            return new Vector3(val[0], 0, val[1]);
        }
        //向目标点移动N距离的点
        public static Vector3 TargetPostionDis(this Vector3 origin, Vector3 target, float dis)
        {
            return origin + (target - origin).normalized * dis;
        }

        //向目标点移动N距离的点
        public static Vector2 TargetPostionDis2D(this Vector2 origin, Vector2 target, float dis)
        {
            return origin + (target - origin).normalized * dis;
        }

        public static float VectorAngle(this Vector2 from, Vector2 to)
        {
            float angle;
            Vector3 cross = Vector3.Cross(from, to);
            angle = Vector2.Angle(from, to);
            return cross.z > 0 ?-angle : angle;
        }
    }
}
