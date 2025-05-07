using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public Button playButton;
    public AudioSource tingAudio;
    public TrackingManager tingTracking;
    public TrackingManager foxTracking;
    public TrackingManager screenTracking;

    private Transform curTarget;
    private Transform screenTarget;

    // Start is called before the first frame update
    void Start()
    {
        playButton.gameObject.SetActive(false);

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(()=> {
            if (curTarget && curTarget.gameObject.activeSelf) {
                playButton.gameObject.SetActive(false);
                curTarget.GetComponent<Animator>().SetTrigger("Play");
                if (curTarget.parent.name == "ImageTarget") 
                {
                    tingAudio.Play();
                }
                if (curTarget.parent.name == "ImageTarget1")
                {
                    screenTarget.GetComponent<Animator>().SetTrigger("Play");
                }
            }
        });

        tingTracking.onTracked = (Transform trans) =>
        {
            playButton.gameObject.SetActive(true);
            curTarget = trans;
        };
        tingTracking.onLost = (Transform trans) =>
        {
            tingAudio.Stop();
            playButton.gameObject.SetActive(false);
        };

        foxTracking.onTracked = (Transform trans) =>
        {
            playButton.gameObject.SetActive(true);
            curTarget = trans;
        };
        foxTracking.onLost = (Transform trans) =>
        {
            playButton.gameObject.SetActive(false);
        };

        screenTracking.onTracked = (Transform trans) =>
        {
            screenTarget = trans;
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
