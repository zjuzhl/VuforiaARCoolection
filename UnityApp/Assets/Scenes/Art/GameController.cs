using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public HandClicked handClicked;
    public AudioSource tingAudio;
    public TrackingManager tingTracking;
    public TrackingManager foxTracking;
    public TrackingManager screenTracking;

    public Transform tingTarget;
    public Transform foxTarget;
    public Transform screenTarget;

    // Start is called before the first frame update
    void Start()
    {
        handClicked.onClicked = (Transform trans) =>
        {
            if (trans.name == "AT") 
            {
                foxTarget.GetComponent<Animator>().SetTrigger("Play");
                screenTarget.GetComponent<Animator>().SetTrigger("Play");
            }
            if (trans.name == "Tingzi") {
                tingTarget.GetComponent<Animator>().SetTrigger("Play");
                tingAudio.Play();
            }
        };

        tingTracking.onTracked = (Transform trans) =>
        {

        };
        tingTracking.onLost = (Transform trans) =>
        {
            tingAudio.Stop();
            if (tingTarget) tingTarget.GetComponent<Animator>().Play("empty", -1, 0);
        };

        foxTracking.onTracked = (Transform trans) =>
        {

        };
        foxTracking.onLost = (Transform trans) =>
        {
        };

        screenTracking.onTracked = (Transform trans) =>
        {
  
        };
        screenTracking.onLost = (Transform trans) =>
        {
            if(screenTarget) screenTarget.GetComponent<Animator>().Play("empty", -1, 0);
        };
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
