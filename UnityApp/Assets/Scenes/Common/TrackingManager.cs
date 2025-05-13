using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackingManager : MonoBehaviour
{

    public Transform targetTrans;
    public Action<Transform> ontracked;
    public Action<Transform> onlost;

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
        ontracked?.Invoke(targetTrans);
    }

    public void OnTargetLost()
    {
        targetTrans.gameObject.SetActive(false);
        onlost?.Invoke(targetTrans);
    }
}
