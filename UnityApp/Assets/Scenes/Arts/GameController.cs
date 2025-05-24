using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public HandClicked handClicked;
    public TrackingManager trackingManager1;
    public TrackingManager trackingManager2;
    public TrackingManager trackingManager3;
    public TrackingManager trackingManager4;

    public ImageState ImageState1;
    public ImageState ImageState2;
    public ImageState ImageState3;
    public ImageState ImageState4;

    // Start is called before the first frame update
    void Start()
    {
        handClicked.onClicked = (trans) =>
        {
            if(trans.name == "bee_animation_rigged" ||
            trans.name == "bird_orange" ||
            trans.name == "bird_orange1" ||
            trans.name == "low-poly_tractor" ||
            trans.name == "snow_man")
            {
                var animator = trans.GetComponent<Animator>();
                if (animator) {
                    animator.SetTrigger("Play");
                }
            }
        };

        trackingManager1.onTracked = (trans) =>
        {
            ImageState1.onEnter();
        };
        trackingManager2.onTracked = (trans) =>
        {
            ImageState2.onEnter();
        };
        trackingManager3.onTracked = (trans) =>
        {
            ImageState3.onEnter();
        };
        trackingManager4.onTracked = (trans) =>
        {
            ImageState4.onEnter();
        };

        trackingManager1.onLost = (trans) =>
        {
            ImageState1.onExit();
        };
        trackingManager2.onLost = (trans) =>
        {
            ImageState2.onExit();
        };
        trackingManager3.onLost = (trans) =>
        {
            ImageState3.onExit();
        };
        trackingManager4.onLost = (trans) =>
        {
            ImageState4.onExit();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
