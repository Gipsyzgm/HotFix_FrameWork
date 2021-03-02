using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Test : MonoBehaviour
{

    public GameObject Object;
    public GameObject Par;
    public GameObject NewPar;
    private float space;
    private float y;
    public float aniTime = 0.1f;
    // Start is called before the first frame update
    List<GameObject> gameObjects = new List<GameObject>();
    void Start()
    {
         space = NewPar.transform.GetComponent<RectTransform>().rect.width/5f;
         y = NewPar.transform.GetComponent<RectTransform>().rect.height / 2f;

        for (int i = 0; i < 5; i++)
        {
            GameObject gameObject = Instantiate(Object);
            gameObject.transform.SetParent(Par.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f); ;
            gameObjects.Add(gameObject);
        }

        gameObject.SetActive(true);
        StartCoroutine(BoxAni());
      

    }

    IEnumerator BoxAni() 
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.SetParent(NewPar.transform);
            gameObjects[i].transform.DOLocalMove(new Vector3(i * space + space / 2f, y, 0), aniTime);
            gameObjects[i].transform.DORotate(Vector3.zero, aniTime);
            gameObjects[i].transform.DOScale(Vector3.one, aniTime);
            yield return new WaitForSeconds(aniTime);
        }

    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
