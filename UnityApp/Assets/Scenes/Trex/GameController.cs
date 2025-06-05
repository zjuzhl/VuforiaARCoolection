using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform target;
    public Transform targetRootInDesc;
    public Transform targetRootInCamera;
    public Transform targetRootInMarker;

    public Transform descPanel;

    public HandRotate handRotate;
    public TrackingManager trackingManager;
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
        descPanel.Find("BtnClose").GetComponent<Button>().onClick.AddListener(()=> 
        {
            if (trackingManager.trackingStatus == Vuforia.Status.TRACKED || (isMocTracking && Application.isEditor))
            {
                EnterMarkerMode();
            }
            else
            {
                EnterMidCamMode();
            }
        });
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
            if (trackingManager.trackingStatus == Vuforia.Status.TRACKED || (isMocTracking && Application.isEditor))
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
        if (targetPose == TargetPose.InMarker)
        {
            if (!handRotate.scaleEnable) handRotate.scaleEnable = true;
        }

        if (movingTarget) 
        {
            target.localPosition = Vector3.MoveTowards(target.localPosition, Vector3.zero, 0.01f);
            target.localRotation = Quaternion.RotateTowards(target.localRotation, Quaternion.identity, 10f);
            if(targetPose == TargetPose.InMidCam || targetPose == TargetPose.InUpDesc)
                target.localScale = Vector3.MoveTowards(target.localScale, 0.5f * Vector3.one, 0.1f); // …Ë∂®Àı∑≈
            if (Vector3.Magnitude(target.localPosition) <= 0.01f && Vector3.Magnitude(target.localRotation.eulerAngles) <= 0.01f) 
            {
                movingTarget = false;
                target.localPosition = Vector3.zero;
            }
        }
    }

    void EnterMarkerMode()
    {
        if (targetPose != TargetPose.InMarker)
        {
            target.SetParent(targetRootInMarker);
            movingTarget = true;
            descPanel.parent.GetComponent<Animator>().Play("DescHidden", -1, 0);
            targetPose = TargetPose.InMarker;
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

            handRotate.scaleEnable = false;
        }
    }

    void EnterDescMode() 
    {
        if (targetPose != TargetPose.InUpDesc)
        {
            targetPose = TargetPose.InUpDesc;

            handRotate.scaleEnable = false;

            target.SetParent(targetRootInDesc);
            movingTarget = true;
            descPanel.parent.GetComponent<Animator>().Play("DescShowing", -1, 0);
        }
    }


}
