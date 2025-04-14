using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewNuts : MonoBehaviour
{
    public Transform target;
    /// <summary>
    /// 动画的时长计数，默认值大于动画时长
    /// </summary>
    private float starttimecount = 10;
    /// <summary>
    /// 标记动画状态，记录是否是拧进或拧出动画，true是拧出，否则是拧进
    /// </summary>
    private bool onPlayBack = false;
    /// <summary>
    /// 动画时长，在播放拧进或拧出动画时，不允许点击交互。
    /// </summary>
    private float animtime = 2.1f; // 略大于动画时长

    /// <summary>
    /// 提示识别
    /// </summary>
    public Transform mpImageRecog;
    /// <summary>
    /// 提示点击
    /// </summary>
    public Transform mpImageClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 动画中计时，未达到时间不允许交互
        if (starttimecount < animtime)
        {
            starttimecount += Time.deltaTime;
            return;
        }

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
                    //判断是否点钟
                    if (hitInfo.transform.name == target.name) 
                    {
                        if (starttimecount > animtime) 
                        {
                            var animator = hitInfo.transform.parent.GetComponent<Animator>();
                            //通过触发参数控制动画播放
                            animator.SetTrigger(onPlayBack?"PlayOut": "PlayIn");
                            onPlayBack = !onPlayBack;

                            starttimecount = 0;
                        }
                    }
                }
            }
        }

        // 编辑器模式下调试，不影响发布后真机体验
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            if (starttimecount > animtime) 
            {
                var animator = target.transform.parent.GetComponent<Animator>();
                animator.SetTrigger("PlayIn");
                onPlayBack = !onPlayBack;
                starttimecount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (starttimecount > animtime) 
            {
                var animator = target.transform.parent.GetComponent<Animator>();
                animator.SetTrigger("PlayOut");
                onPlayBack = !onPlayBack;
                starttimecount = 0;
            }
        }
#endif
    }


    /// <summary>
    /// 提示识别
    /// </summary>
    public void onRecog() 
    {
        mpImageRecog.gameObject.SetActive(true);
        mpImageClick.gameObject.SetActive(false);
    }
    /// <summary>
    /// 提示点击
    /// </summary>
    public void onClick()
    {
        mpImageRecog.gameObject.SetActive(false);
        mpImageClick.gameObject.SetActive(true);
    }

}
