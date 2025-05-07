using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackingManager : MonoBehaviour
{

    public Transform targetTrans;
    public Action<Transform> onTracked;
    public Action<Transform> onLost;

    // Start is called before the first frame update
    void Start()
    {
        targetTrans.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
