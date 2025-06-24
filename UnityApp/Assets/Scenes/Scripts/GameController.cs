using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public EPWriter epWriter;
    public TrackingManager trackingManager;

    public int sceneId = 1;

    // Start is called before the first frame update
    void Start()
    {
        trackingManager.onTracked += (Transform trans) =>
        {
            epWriter.SaveTracked("ImageTarget" + sceneId);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
