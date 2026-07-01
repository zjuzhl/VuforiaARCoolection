using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnInteractive : MonoBehaviour
{
    public Animator targetAnimator;

    public GameObject tip;

    public GameObject btn;

    private int triggerState = 0;

    // Start is called before the first frame update
    void Start()
    {
        tip?.SetActive(true);
        btn?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableTip() 
    {
        tip?.SetActive(true);
        btn?.SetActive(false);
        triggerState = 0;
        targetAnimator?.ResetTrigger("Play1");
        targetAnimator?.ResetTrigger("Play2");
    }

    public void DisaenableTip() 
    {
        tip?.SetActive(false);
        btn?.SetActive(true);
    }



    public void SetTargetTrigger() 
    {
        if (triggerState == 0)
        {
            triggerState = 1;
            targetAnimator?.SetTrigger("Play1");
        }
        else {
            triggerState = 0;
            targetAnimator?.SetTrigger("Play2");
        }
    }

}
