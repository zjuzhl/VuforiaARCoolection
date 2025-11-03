using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager1 : MonoBehaviour
{

    public Transform targetTrans;
    public SwitchImages switchImages;

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
        switchImages.SetImageEnable(3);
    }

    public void OnTargetLost()
    {
        targetTrans.gameObject.SetActive(false);
    }
}
