using UnityEngine;
using System;
using Vuforia;

public class TrackingManager : MonoBehaviour
{
    public Transform targetTrans;
    public Action<Transform> onTracked;
    public Action<Transform> onLost;

    public ObserverBehaviour mObserverBehaviour;
    public bool trackingStatus 
    {
        get { return mObserverBehaviour.TargetStatus.Status == Status.TRACKED; }
    }

    public string trackingTargetName
    {
        get { return mObserverBehaviour.TargetName; }
    }

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
