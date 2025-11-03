using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchImages : MonoBehaviour
{

    public GameObject[] mats;
    public Renderer renderer;

    public Button[] btns;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mats.Length; i++) 
        {
            int index = i;
            btns[i].onClick.AddListener(()=>{
                SetImageEnable(index); // ÇÐ»»Ö¸¶¨Í¼Æ¬
            });
        }

        SetImageEnable(3); // Ä¬ÈÏCV
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImageEnable(int idx) {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetActive(i == idx);
        }
    }
}
