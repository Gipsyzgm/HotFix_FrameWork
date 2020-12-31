using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform target;

    public bool IsLootAtMain = false;

    private void Awake()
    {
        if (IsLootAtMain&& Camera.main!=null)
            target = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(target);
    }
}
