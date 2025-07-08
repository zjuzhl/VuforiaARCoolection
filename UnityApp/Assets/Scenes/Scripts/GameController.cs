using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TrackingManager trackingManager;
    private bool isFirstTracked = false;

    public TMPro.TMP_Text NameDesc;
    public HorizontalScrollView horizontalScrollView;

    public HandRotate handRotate;

    // Start is called before the first frame update
    void Start()
    {
        NameDesc.transform.parent.gameObject.SetActive(false);
        horizontalScrollView.gameObject.SetActive(false);

        trackingManager.onTracked += OnTrackedTarget;
        horizontalScrollView.onSwicthItemSuccess += OnSwicthItem;
    }
    void OnDestroy()
    {
        trackingManager.onTracked -= OnTrackedTarget;
        horizontalScrollView.onSwicthItemSuccess -= OnSwicthItem;
    }

    void OnTrackedTarget(Transform trans) 
    {
        if (!isFirstTracked) 
        {
            isFirstTracked = true;
            horizontalScrollView.gameObject.SetActive(true);
            horizontalScrollView.InitItem();
        }
    }

    void OnSwicthItem(int id) 
    {
        handRotate.resetPose();
        handRotate.resetScale();
        NameDesc.transform.parent.gameObject.SetActive(true);
        NameDesc.text = id == 0 ? "方鼎，用于祭祀。" :
            id == 1 ? "食鼎，用于储存食物。" :
            id == 2 ? "甗，用于蒸煮食物。" :
            id == 3 ? "尊，用于容酒。" :
            id == 4 ? "剑，短刃兵器，剑刃锋利。" :
            id == 5 ? "矛，勾啄兵器。" :
            id == 6 ? "铃，用于乐器演奏。" :
            id == 7 ? "爵，用于盛酒。" : "";
    }
}
