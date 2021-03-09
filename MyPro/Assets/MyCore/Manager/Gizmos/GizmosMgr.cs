using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UObject = UnityEngine.Object;
using UnityEngine.U2D;
using System.Threading.Tasks;


/// <summary>
/// 资源包管理器
/// 全部资源包加载都使用异步加载
/// </summary>
public class GizmosMgr : BaseMgr<GizmosMgr>
{
    public List<GizmosData> spheres = new List<GizmosData>();
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = spheres.Count; --i >= 0;)
        {
            Gizmos.DrawSphere(spheres[i].center, spheres[i].raduis);
            spheres[i].time -= Time.deltaTime;
            if (spheres[i].time < 0)
                spheres.RemoveAt(i);
        }
    }
#endif
    public void DrawSphere(Vector3 center, float raduis, float time)
    {
        GizmosData data = new GizmosData();
        data.center = center;
        data.raduis = raduis;
        data.time = time;
        spheres.Add(data);
    }
}
