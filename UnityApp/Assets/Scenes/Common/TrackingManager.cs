using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackingManager : MonoBehaviour
{
    public Transform targetTrans; // 控制对象
    public Action<Transform> onTracked; // 进入跟踪状态的回调
    public Action<Transform> onLost; // 进入丢失状态的回调

    // Start is called before the first frame update
    void Start()
    {
        targetTrans.gameObject.SetActive(false);
    }

    public void OnTargetTracked() 
    {
        targetTrans.gameObject.SetActive(true);
        onTracked?.Invoke(targetTrans);
    }

    public void OnTargetLost()
    {
        targetTrans.gameObject.SetActive(false);
        onLost?.Invoke(targetTrans);
    }
}
