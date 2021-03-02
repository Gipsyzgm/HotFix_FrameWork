using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//[RequireComponent(typeof(Text))]
public class UILangText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private Text textTarget;
    void Start()
    {
        textTarget = gameObject.GetComponent<Text>();
        if (MainMgr.ILR != null)
        {
            textTarget.text = MainMgr.ILR.CallHotFixGetLang(key); //查找Key
        }
    }
    public string Key
    {
        get { return key; }
        set
        {
            if (key != value)
            {
                key = value;
                if (MainMgr.ILR != null)
                    Value = MainMgr.ILR.CallHotFixGetLang(key);
                if (textTarget != null) //重新查找值
                    textTarget.text = Value;
            }
        }
    }

    public void Refresh()
    {
        Value = MainMgr.ILR.CallHotFixGetLang(key);
    }

    public string Value
    {
        get
        {
            if (textTarget == null)
                return string.Empty;
            return textTarget.text;
        }
        set
        {
            if (textTarget == null)
            {
                textTarget = gameObject.GetComponent<Text>();
            }
            textTarget.text = value;
        }
    }
}
