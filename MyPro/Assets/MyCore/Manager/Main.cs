using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    void Awake()
    {

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        Mgr.Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
