using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TrackingManager trackingManager;
    private bool isFirstTracked = false;

    public HandRotate handRotate;

    // Start is called before the first frame update
    void Start()
    {
        trackingManager.onTracked += OnTrackedTarget;
    }
    void OnDestroy()
    {
        trackingManager.onTracked -= OnTrackedTarget;
    }

    void OnTrackedTarget(Transform trans) 
    {
        if (!isFirstTracked) 
        {
            isFirstTracked = true;
        }
    }
}
