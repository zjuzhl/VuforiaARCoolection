using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameController : MonoBehaviour
{
    public ImageTargetBehaviour targetBehaviour; // Í¼Æ¬×é¼þ
    public GameObject Target;   // ¸ú×ÙÄ£ÐÍ

    public GameObject CanvasBg;
    public GameObject Btn_Bg;
    public GameObject RecogTip;
    public GameObject IntertivePage;
    private bool isInertive;

    // Start is called before the first frame update
    void Start()
    {
        Target.SetActive(false);
        CanvasBg.SetActive(true);
        RecogTip.SetActive(false);
        IntertivePage.SetActive(false);
        isInertive = false;

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
        if (!isInertive) return;

        if (status.Status == Status.TRACKED)
        {
            Target.SetActive(true);
            IntertivePage.SetActive(true);
            RecogTip.SetActive(false);
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
        CanvasBg.SetActive(false);
        Btn_Bg.SetActive(false);
        RecogTip.SetActive(true);
        isInertive = true;
    }

    #region ²¥·Å¶¯»­
    public void CommandDogPose1() 
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play1");
    }

    public void CommandDogPose2()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play2");
    }

    public void CommandDogPose3()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play3");
    }

    public void CommandDogPose4()
    {
        Target.GetComponentInChildren<Animator>().SetTrigger("Play4");
    }
    #endregion
}
