using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Main : MonoBehaviour
{
    bool IsStart = false;

    void Awake()
    {

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void  Start()
    {
        Mgr.Initialize();
        Mgr.ILR.Init();   
        IsStart = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsStart)
            Mgr.ILR.CallHotFixMainUpdate(Time.deltaTime);
    }
}
