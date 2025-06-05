using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotate : MonoBehaviour
{
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
        if (scaleTarget) prescale = scaleTarget.localScale;
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

        if (Input.touchCount == 2) 
        {
            if (!scaleTarget) return; // 没有可缩放的对象
            if (!scaleTarget.gameObject.activeInHierarchy) return; // 可缩放对象未显示

            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            var dis = Vector2.Distance(touch1.position, touch0.position);
            
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                curTouchesDis = dis;// 当触摸开始时，记录初始距离
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
