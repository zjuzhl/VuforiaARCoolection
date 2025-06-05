using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameController : MonoBehaviour
{

    public WaterWaveEffect waterWaveEffect;
    public ImageTargetBehaviour targetBehaviour;
    public GameObject Target;

    public GameObject CanvasInCamera;
    public GameObject Btn_Bg;

    public GameObject IntertivePage;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        IntertivePage.SetActive(false);
        targetBehaviour.OnTargetStatusChanged += OnStatusChanged;
    }

    private void OnDestroy()
    {
        targetBehaviour.OnTargetStatusChanged -= OnStatusChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStatusChanged(ObserverBehaviour observer, TargetStatus status)
    {
        if (status.Status == Status.TRACKED)
        {
            if (IntertivePage.activeSelf == false) {
                IntertivePage.SetActive(true);
            }
        }
    }

    public void StartClick() 
    {
        waterWaveEffect.enabled = false;
        CanvasInCamera.SetActive(false);
        Btn_Bg.SetActive(false);
    }

    public void CommandDogStand() 
    {
        Target.GetComponent<Animator>().SetTrigger("Play1");
    }

    public void CommandDogSit()
    {
        Target.GetComponent<Animator>().SetTrigger("Play2");
    }

    public void CommandDogShake()
    {
        Target.GetComponent<Animator>().SetTrigger("Play3");
    }

    public void CommandDogRollover()
    {
        Target.GetComponent<Animator>().SetTrigger("Play4");
    }
}   
