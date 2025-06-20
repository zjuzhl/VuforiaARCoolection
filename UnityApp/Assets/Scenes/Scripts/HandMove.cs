using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandMove : MonoBehaviour
{
    public Transform areaPlane;
    private Transform target;
    private Vector3 originPos;
    private bool isMovingBack = false;
    private bool hittedTargetPlane = false;
    private Material areaMat;

    public Action<string>  handMoveFinished;

    // Start is called before the first frame update
    void Start()
    {
        areaMat = areaPlane.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingBack) 
        {
            if (target)
            {
                target.localPosition = Vector3.MoveTowards(target.localPosition, originPos, 0.05f);
                if (Vector3.Distance(target.localPosition, originPos) < 0.001f) 
                {
                    target.localPosition = originPos;
                    isMovingBack = false;
                    target = null;
                }
            }
            else 
            {
                isMovingBack = false;
            }
        }

        if (Input.touchCount == 1) 
        {
            if (isMovingBack) return;

            var touch = Input.GetTouch(0);
            var phase = touch.phase;
            if (phase == TouchPhase.Began) 
            {
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, 1 << 20))
                {
                    target = hitInfo.transform;
                    //originPos = target.position;
                    if (target.name == "brush_red")
                    {
                        originPos = new Vector3(0.26f, 0.05f, -0.726f);
                    }
                    else if (target.name == "brush_white") 
                    {
                        originPos = new Vector3(0.13f, 0.05f, -0.726f);
                    }
                    else if (target.name == "brush_black")
                    {
                        originPos = new Vector3(0.0f, 0.05f, -0.726f);
                    }
                    else if (target.name == "brush_brown")
                    {
                        originPos = new Vector3(-0.13f, 0.05f, -0.726f);
                    }
                    else if (target.name == "brush_yellow")
                    {
                        originPos = new Vector3(-0.26f, 0.05f, -0.726f);
                    }
                    isMovingBack = false;
                    hittedTargetPlane = false;
                    return;
                }
            }

            if (phase == TouchPhase.Moved && Time.frameCount % 2 == 0) 
            {
                if (target) 
                {
                    var ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo1, 1000, 1 << 22))
                    {
                        hittedTargetPlane = true;
                        // 高亮
                        areaMat.SetFloat("_Opacity", 0.5f);
                    }
                    else
                    {
                        hittedTargetPlane = false;
                        // 取消高亮
                        areaMat.SetFloat("_Opacity", 0.0f);
                    }

                    if (Physics.Raycast(ray, out RaycastHit hitInfo2, 1000, 1 << 21))
                    {
                        target.position = hitInfo2.point;
                    }
                }
            }

            if (phase == TouchPhase.Ended)
            {
                isMovingBack = true;

                if (target && hittedTargetPlane) 
                {
                    switch (target.name) 
                    {
                        case "brush_red":
                            transform.Find("Red").gameObject.SetActive(true);
                            handMoveFinished?.Invoke("Red");
                            break;
                        case "brush_white":
                            transform.Find("White").gameObject.SetActive(true);
                            handMoveFinished?.Invoke("White");
                            break;
                        case "brush_black":
                            transform.Find("Black").gameObject.SetActive(true);
                            handMoveFinished?.Invoke("Black");
                            break;
                        case "brush_brown":
                            transform.Find("Brown").gameObject.SetActive(true);
                            handMoveFinished?.Invoke("Brown");
                            break;
                        case "brush_yellow":
                            transform.Find("Yellow").gameObject.SetActive(true);
                            handMoveFinished?.Invoke("Yellow");
                            break;
                    }
                }
                hittedTargetPlane = false;
                // 取消高亮
                areaMat.SetFloat("_Opacity", 0.0f);
            }
        }
    }
}
