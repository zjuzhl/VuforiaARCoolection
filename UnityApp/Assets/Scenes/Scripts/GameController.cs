using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameController : MonoBehaviour
{
    public ImageTargetBehaviour targetBehaviour; // Í¼Æ¬×é¼þ
    public GameObject Target;   // ¸ú×ÙÄ£ÐÍ

    public GameObject Btn_Bg;
    public GameObject RecogTip;
    public GameObject OverSpeedTip;
    public GameObject IntertivePage;
    private bool isInertive;

    private float switchTime = 0;

    private Vuforia.Status preStatus;

    // Start is called before the first frame update
    void Start()
    {
        Target.SetActive(false);
        RecogTip.SetActive(false);
        OverSpeedTip.SetActive(false);
        IntertivePage.SetActive(false);
        isInertive = false;

        preStatus = Status.NO_POSE;
        targetBehaviour.OnTargetStatusChanged += OnStatusChanged;
    }

    private void OnDestroy()
    {
        targetBehaviour.OnTargetStatusChanged -= OnStatusChanged;
    }

    // Update is called once per frame
    void Update() { }

    /// <summary>
    /// ¼àÌý¸ú×Ù×´Ì¬
    /// </summary>
    /// <param name="observer"></param>
    /// <param name="status"></param>
    public void OnStatusChanged(ObserverBehaviour observer, TargetStatus status)
    {
        preStatus = status.Status;
        if (!isInertive) return;

        if (status.Status == Status.TRACKED || preStatus == Status.TRACKED)
        {
            Target.SetActive(true);
            IntertivePage.SetActive(true);
            RecogTip.SetActive(false);
            preStatus = Status.NO_POSE; // reset
        }
        else if (status.Status == Status.LIMITED || status.Status == Status.EXTENDED_TRACKED) 
        {
            Target.SetActive(false);
            IntertivePage.SetActive(false);
            RecogTip.SetActive(true);
        }
    }

    /// <summary>
    /// µã»÷ÕÙ»½
    /// </summary>
    public void StartClick() 
    {
        Btn_Bg.SetActive(false);
        RecogTip.SetActive(true);
        isInertive = true;
    }

    #region ²¥·Å¶¯»­
    public void CommandDogPose1() 
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play1");
        Target.GetComponentInChildren<Animator>().ResetTrigger("Play2");
        Target.GetComponentInChildren<Animator>().ResetTrigger("Play3");
        Target.GetComponentInChildren<Animator>().ResetTrigger("Play4");
        Target.GetComponentInChildren<Animator>().ResetTrigger("Play5");
        Target.GetComponentInChildren<Animator>().ResetTrigger("Play6");
        CheckSwitchTime();
    }

    public void CommandDogPose2()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play2");
        CheckSwitchTime();
    }

    public void CommandDogPose3()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play3");
        CheckSwitchTime();
    }

    public void CommandDogPose4()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play4");
        CheckSwitchTime();
    }

    public void CommandDogPose5()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play5");
        CheckSwitchTime();
    }

    public void CommandDogPose6()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play6");
        CheckSwitchTime();
    }

    public void CheckSwitchTime() 
    {
        if (switchTime > 0 && (Time.time - switchTime) <= 1.0f) 
        {
            if (!OverSpeedTip.activeSelf) 
            {
                OverSpeedTip.SetActive(true);
                Invoke(nameof(CloseSpeedTip), 3.0f);
            }
        }
        switchTime = Time.time;
    }

    public void CloseSpeedTip() 
    {
        OverSpeedTip.SetActive(false);
    }
    #endregion
}
