using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{

    public Transform targetTrans;

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
    }

    public void OnTargetLost()
    {
        targetTrans.gameObject.SetActive(false);
    }
}
