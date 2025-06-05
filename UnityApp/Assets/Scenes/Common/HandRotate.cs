using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotate : MonoBehaviour
{
    public Transform colliderTarget;
    public Transform rotateTarget;
    public float rotationSpeed = 8f;
    public bool xrotEnable = false;
    public bool yrotEnable = false;
    private bool isDragging = false;
    private Quaternion prequa;

    // scale
    public Transform scaleTarget;
    private float curTouchesDis = 0;
    public float scaleSpeed = 0.02f;
    public float maxScaleSize = 2;
    public float minScaleSize = 0.5f;
    private Vector3 prescale;
    private float curScaleSize = 1.0f;

    public bool enable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (rotateTarget) prequa = rotateTarget.localRotation;
        if (scaleTarget) prescale = scaleTarget.localScale;
    }

    public void resetPose() 
    {
        if (rotateTarget) rotateTarget.localRotation = prequa;
    }

    public void resetScale()
    {
        if(scaleTarget) scaleTarget.localScale = prescale;
        curScaleSize = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        if (Input.touchCount == 1) 
        {
            if (!colliderTarget || !rotateTarget) return; 

            var touch = Input.GetTouch(0);
            var state = touch.phase;
            if (state == TouchPhase.Began) 
            {
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, 1 << LayerMask.NameToLayer("Rotate"))) 
                {
                    if (hitInfo.transform == colliderTarget) 
                    {
                        isDragging = true;
                    }
                }
            }
            else if (state == TouchPhase.Moved)
            {
                if (isDragging && rotateTarget != null) 
                {
                    var delta = touch.deltaPosition;
                    if (Screen.orientation == ScreenOrientation.Portrait) 
                    {
                        if (xrotEnable)
                            rotateTarget.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime, Space.Self);
                        if (yrotEnable)
                            rotateTarget.Rotate(Vector3.right, delta.y * rotationSpeed * Time.deltaTime, Space.World);
                    }

                    if (Screen.orientation == ScreenOrientation.LandscapeLeft) 
                    {
                        if (xrotEnable)
                            rotateTarget.Rotate(Vector3.up, -delta.x * rotationSpeed * Time.deltaTime, Space.Self);
                        if (yrotEnable)
                            rotateTarget.Rotate(Vector3.right, delta.y * rotationSpeed * Time.deltaTime, Space.World);
                    }
                }
            }
            else if (state == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }

        if (Input.touchCount == 2) 
        {
            if (!scaleTarget) return; // û�п����ŵĶ���
            if (!scaleTarget.gameObject.activeInHierarchy) return; // �����ŵĶ���δ��ʾ

            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            var dis = Vector2.Distance(touch1.position, touch0.position);
            
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                curTouchesDis = dis;// ��������ʼʱ����¼��ʼ����
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved) 
            {
                if (curTouchesDis <= 0) return;
                var l = dis - curTouchesDis;
                curScaleSize += (l * 0.1f * scaleSpeed);
                if (curScaleSize >= maxScaleSize) curScaleSize = maxScaleSize;
                if (curScaleSize <= minScaleSize) curScaleSize = minScaleSize;
                scaleTarget.localScale = prescale * curScaleSize;
                curTouchesDis = dis;
            }
            if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) 
            {
                curTouchesDis = 0;
            }
        }
    }
}
