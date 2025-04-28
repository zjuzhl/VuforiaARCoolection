using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject target;

    private bool firstTracked = false;

    private void Awake()
    {
        target.SetActive(false);
    }

    public void OnTracked() 
    {
        if (firstTracked) return;
        firstTracked = true;
        target.SetActive(true);

    }

    public void OnLost()
    {

    }
}
