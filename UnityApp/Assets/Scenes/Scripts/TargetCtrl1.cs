using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCtrl1 : MonoBehaviour
{

    public HandClicked handClicked;

    // Start is called before the first frame update
    void Start()
    {
        // 检测用户点击到碰撞盒回调
        handClicked.onClicked = (Transform trans) =>
        {
            if (trans.name == "bird1")
            {
                trans.GetComponent<Animator>().SetTrigger("Play");
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
