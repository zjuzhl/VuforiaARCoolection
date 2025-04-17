using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewNuts : MonoBehaviour
{
    /// <summary>
    /// 螺丝螺母模型
    /// </summary>
    public Transform targetScrew;
    /// <summary>
    /// 轴承座模型
    /// </summary>
    public Transform targetBearing;
    /// <summary>
    /// 标记动画状态，记录是否是拧进或拧出动画，true是拧出，否则是拧进
    /// </summary>
    private bool onPlayBack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            //获取屏幕点击
            var touch = Input.GetTouch(0);
            var state = touch.phase;
            //屏幕点击开始时
            if (state == TouchPhase.Began)
            {
                //检测是否点中螺帽模型
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
                {
                    Debug.Log("hitinfo: " + hitInfo.transform.name);
                    //判断是否点中螺母
                    if (hitInfo.transform.parent.name == targetScrew.name) 
                    {
                        var animator = targetScrew.GetComponent<Animator>();
                        //通过触发参数控制动画播放
                        animator.SetTrigger(onPlayBack ? "PlayOut" : "PlayIn");
                        onPlayBack = !onPlayBack;
                    }
                    //判断是否点中轴承座
                    else if (hitInfo.transform.name == targetBearing.name)
                    {
                        var animator = targetBearing.GetComponent<Animator>();
                        animator.SetTrigger("Play");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 重置初始化状态 螺母
    /// </summary>
    public void resetTargetScrew() 
    {
        onPlayBack = false;
        var animator = targetScrew.GetComponent<Animator>();
        animator.Play("empty", -1, 0);
    }

    /// <summary>
    /// 重置初始化状态 轴承座
    /// </summary>
    public void resetTargetBearing()
    {
        var animator = targetBearing.GetComponent<Animator>();
        animator.Play("empty", -1, 0);
    }
}
