using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform target;
    public Transform targetRootInDesc;
    public Transform targetRootInCamera;
    public Transform targetRootInMarker1;
    public Transform targetRootInMarker2;
    public Transform targetRootInMarker3;
    public Transform targetRootInMarker4;

    public Transform descPanel;
    public Button backClearBtn;
    public GameObject recogTipImg;

    public HandRotate handRotate;
    public TrackingManager trackingMgr1;
    public TrackingManager trackingMgr2;
    public TrackingManager trackingMgr3;
    public TrackingManager trackingMgr4;
    public int trackingIndex = 0;
    public bool isMocTracking = false;
    private bool afterFirstTracked = false;

    private bool movingTarget;
    private TargetPose targetPose;
    public enum TargetPose 
    {
        None,
        InMarker,
        InMidCam,
        InUpDesc
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPose = TargetPose.None;
        UpdateTarget();
        trackingMgr1.onTracked += (Transform trans) =>
        {
            if (targetPose == TargetPose.InUpDesc) return;
            if (target != null && target.name != trans.name) 
            {
                ResetMarkerMode();
            }
            target = trans;
            trackingIndex = 1;
            UpdateTarget();
        };
        trackingMgr2.onTracked += (Transform trans) =>
        {
            if (targetPose == TargetPose.InUpDesc) return;
            if (target != null && target.name != trans.name)
            {
                ResetMarkerMode();
            }
            target = trans;
            trackingIndex = 2;
            UpdateTarget();
        };
        trackingMgr3.onTracked += (Transform trans) =>
        {
            if (targetPose == TargetPose.InUpDesc) return;
            if (target != null && target.name != trans.name)
            {
                ResetMarkerMode();
            }
            target = trans;
            trackingIndex = 3;
            UpdateTarget();
        };
        trackingMgr4.onTracked += (Transform trans) =>
        {
            if (targetPose == TargetPose.InUpDesc) return;
            // 重复识别
            if (target != null && target.name != trans.name)
            {
                ResetMarkerMode();
            }
            target = trans;
            trackingIndex = 4;
            UpdateTarget();
        };

        trackingMgr1.onLost += (Transform trans) => { };
        trackingMgr2.onLost += (Transform trans) => { };
        trackingMgr3.onLost += (Transform trans) => { };
        trackingMgr4.onLost += (Transform trans) => { };

        backClearBtn.onClick.AddListener(()=> 
        {
            ResetMarkerMode();
            recogTipImg.SetActive(true);
            backClearBtn.gameObject.SetActive(false);
            target = null;
            trackingIndex = 0;
        });

        recogTipImg.SetActive(true);
        backClearBtn.gameObject.SetActive(false);

        descPanel.Find("BtnClose").GetComponent<Button>().onClick.AddListener(()=> 
        {
            var tracker = trackingIndex == 1 ? trackingMgr1 :
               trackingIndex == 2 ? trackingMgr2 :
               trackingIndex == 3 ? trackingMgr3 :
               trackingIndex == 4 ? trackingMgr4 : trackingMgr1;
            if (tracker.trackingStatus == Vuforia.Status.TRACKED || (isMocTracking && Application.isEditor))
            {
                EnterMarkerMode();
            }
            else
            {
                EnterMidCamMode();
            }
        });
    }

    void UpdateTarget() 
    {
        backClearBtn.gameObject.SetActive(true);
        recogTipImg.SetActive(false);
        handRotate.colliderTarget = target;
        handRotate.rotateTarget = target;
        handRotate.scaleTarget = target;
        handRotate.resetOriginalPose();
        descPanel.Find("ScrollView1").gameObject.SetActive(trackingIndex == 1);
        descPanel.Find("ScrollView2").gameObject.SetActive(trackingIndex == 2);
        descPanel.Find("ScrollView3").gameObject.SetActive(trackingIndex == 3);
        descPanel.Find("ScrollView4").gameObject.SetActive(trackingIndex == 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || (Input.touchCount == 1 && Input.touches[0].tapCount == 2))
        {
            EnterDescMode();
        }

        if (targetPose != TargetPose.InUpDesc)
        {
            if (trackingIndex > 0) 
            {
                var tracker = trackingIndex == 1 ? trackingMgr1 :
                   trackingIndex == 2 ? trackingMgr2 :
                   trackingIndex == 3 ? trackingMgr3 :
                   trackingIndex == 4 ? trackingMgr4 : trackingMgr1;
                if (tracker.trackingStatus == Vuforia.Status.TRACKED || (isMocTracking && Application.isEditor))
                {
                    if (!afterFirstTracked) afterFirstTracked = true;
                    EnterMarkerMode();
                }
                else
                {
                    if (afterFirstTracked)
                        EnterMidCamMode();
                }
            }
        }

        if (movingTarget) 
        {
            target.localPosition = Vector3.MoveTowards(target.localPosition, Vector3.zero, 0.06f);
            target.localRotation = Quaternion.RotateTowards(target.localRotation, Quaternion.identity, 10f);
            if(targetPose == TargetPose.InMidCam || targetPose == TargetPose.InUpDesc)
                target.localScale = Vector3.MoveTowards(target.localScale, Vector3.one, 0.1f); // 设定缩放
            if (Vector3.Magnitude(target.localPosition) <= 0.01f && Vector3.Magnitude(target.localRotation.eulerAngles) <= 0.01f) 
            {
                movingTarget = false;
                target.localPosition = Vector3.zero;
            }
        }
    }

    void ResetMarkerMode()
    {
        if (trackingIndex == 0) return;

        var marker = trackingIndex == 1 ? targetRootInMarker1 :
                       trackingIndex == 2 ? targetRootInMarker2 :
                       trackingIndex == 3 ? targetRootInMarker3 :
                       trackingIndex == 4 ? targetRootInMarker4 : targetRootInMarker1;
        target.SetParent(marker);
        movingTarget = false;
        target.localPosition = Vector3.zero;
        target.localRotation = Quaternion.identity;
        target.localScale = Vector3.one;
        targetPose = TargetPose.None;

        descPanel.parent.GetComponent<Animator>().Play("DescHidden", -1, 0);

        SetComponentsEnabled(target, false);
    }

    void EnterMarkerMode()
    {
        if (targetPose != TargetPose.InMarker)
        {
            var marker = trackingIndex == 1 ? targetRootInMarker1 :
                trackingIndex == 2 ? targetRootInMarker2 :
                trackingIndex == 3 ? targetRootInMarker3 :
                trackingIndex == 4 ? targetRootInMarker4 : targetRootInMarker1;
            target.SetParent(marker);
            movingTarget = true;
            descPanel.parent.GetComponent<Animator>().Play("DescHidden", -1, 0);
            targetPose = TargetPose.InMarker;

            handRotate.scaleEnable = true;
            handRotate.xrotEnable = true;
            handRotate.yrotEnable = true;
            SetComponentsEnabled(target, true);
        }
    }

    void EnterMidCamMode()
    {
        if (targetPose != TargetPose.InMidCam)
        {
            target.SetParent(targetRootInCamera);
            movingTarget = true;
            if (targetPose == TargetPose.InUpDesc)
            {
                descPanel.parent.GetComponent<Animator>().Play("DescHiding", -1, 0);
            }
            if (targetPose == TargetPose.InMarker || targetPose == TargetPose.None)
            {
                descPanel.parent.GetComponent<Animator>().Play("DescHidden", -1, 0);
            }
            targetPose = TargetPose.InMidCam;

            handRotate.scaleEnable = true;
            handRotate.xrotEnable = true;
            handRotate.yrotEnable = true;
            SetComponentsEnabled(target, true);
        }
    }

    void EnterDescMode() 
    {
        if (targetPose != TargetPose.InUpDesc)
        {
            targetPose = TargetPose.InUpDesc;

            target.SetParent(targetRootInDesc);
            movingTarget = true;
            descPanel.parent.GetComponent<Animator>().Play("DescShowing", -1, 0);

            handRotate.scaleEnable = false;
            handRotate.xrotEnable = false;
            handRotate.yrotEnable = false;
            SetComponentsEnabled(target, true);
        }
    }


    void SetComponentsEnabled(Transform t,  bool enable)
    {
        var components = t.GetComponentsInChildren<Component>();
        foreach (var component in components)
        {
            switch (component)
            {
                case Renderer rendererComponent:
                    rendererComponent.enabled = enable;
                    break;
                case Collider colliderComponent:
                    colliderComponent.enabled = enable;
                    break;
                case Canvas canvasComponent:
                    canvasComponent.enabled = enable;
                    break;
            }
        }
    }
}
