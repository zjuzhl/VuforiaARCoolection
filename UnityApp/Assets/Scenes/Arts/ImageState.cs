using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageState : MonoBehaviour, StateBase
{

    public Transform target;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        target.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            onEnter();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            onExit();
        }
    }


    public void onEnter() {
        target.gameObject.SetActive(true);
        isPlaying = true;
    }

    public void onExit()
    {
        target.gameObject.SetActive(false);
        isPlaying = false;
    }
}
