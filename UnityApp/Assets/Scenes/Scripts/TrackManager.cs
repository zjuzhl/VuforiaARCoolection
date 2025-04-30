using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{

    public void OnTracked() 
    {
        Debug.Log("arvuforia-" + "OnTracked");
    }

    public void OnLost()
    {
        Debug.Log("arvuforia-" + "OnLost");
    }

}
