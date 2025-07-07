using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HorizontalScrollView : MonoBehaviour
{
    public GameObject[] gameItems;
    private GameObject clickedCircle;
    private Button[] allBtns;
    private int preBtnIndex = -1;

    public Action<int> onSwicthItemSuccess;

    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        clickedCircle = this.transform.Find("Viewport/Content/Circle").gameObject;
        clickedCircle.SetActive(false);
        allBtns = this.transform.Find("Viewport/Content").GetComponentsInChildren<Button>();
        for (int i = 0; i < allBtns.Length; i++) 
        {
            var index = i;
            allBtns[index].onClick.AddListener(()=> 
            {
                SwitchItem(preBtnIndex, index);
            });
        }

        for (int i = 0; i < gameItems.Length; i++)
        {
            gameItems[i].SetActive(false);
        }

        started = true;
    }

    private void SwitchItem(int pre, int cur) 
    {
        if (pre >= 0)
        {
            if (gameItems.Length >= pre) 
            {
                gameItems[pre].SetActive(false);
            }
        }
        if (gameItems.Length >= cur)
        {
            gameItems[cur].SetActive(true);
            clickedCircle.SetActive(true);
            clickedCircle.GetComponent<RectTransform>().anchoredPosition =
                allBtns[cur].GetComponent<RectTransform>().anchoredPosition;
            onSwicthItemSuccess?.Invoke(cur);
        }
        else {
            clickedCircle.SetActive(false);
        }
        preBtnIndex = cur;
    }


    public void InitItem() 
    {
        StartCoroutine(nameof(doInitItem));
    }

    IEnumerator doInitItem() 
    {
        yield return new WaitUntil(() => started = true);
        var idx = UnityEngine.Random.Range(0, 3);
        SwitchItem(preBtnIndex, idx);
    }
}
