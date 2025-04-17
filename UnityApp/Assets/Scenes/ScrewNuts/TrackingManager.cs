using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{

    /// <summary>
    /// 提示识别图
    /// </summary>
    public GameObject TipFindAImage;
    /// <summary>
    /// 提示点击螺母
    /// </summary>
    public GameObject TipClickScrew;
    /// <summary>
    /// 提示点击轴承座
    /// </summary>
    public GameObject TipClickBearing;


    public ScrewNuts screwNuts;

    // Start is called before the first frame update
    void Start()
    {
        TipClickScrew.SetActive(false);
        TipClickBearing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 螺母和轴承座都没有识别时，提示识别图
        TipFindAImage.SetActive(!TipClickScrew.activeSelf && !TipClickBearing.activeSelf);
    }

    /// <summary>
    /// 监听螺母识别图跟踪状态
    /// </summary>
    public void onTargetRecog() 
    {
        if (!TipClickScrew.activeSelf) 
        {
            TipClickScrew.SetActive(true);
            screwNuts.targetScrew.gameObject.SetActive(true);
            screwNuts.targetBearing.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 监听轴承识别图跟踪状态
    /// </summary>
    public void onTargetRecog1()
    {
        if (!TipClickBearing.activeSelf) 
        {
            TipClickBearing.SetActive(true);
            screwNuts.targetBearing.gameObject.SetActive(true);
            screwNuts.targetScrew.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 监听螺母识别图丢失状态
    /// </summary>
    public void onTargetLost()
    {
        if (TipClickScrew.activeSelf) 
        {
            screwNuts.targetScrew.gameObject.SetActive(false);
            TipClickScrew.SetActive(false);
            screwNuts.resetTargetScrew();
        }
    }

    /// <summary>
    /// 监听轴承识别图丢失状态
    /// </summary>
    public void onTargetLost1()
    {
        if (TipClickBearing.activeSelf) 
        {
            screwNuts.targetBearing.gameObject.SetActive(false);
            TipClickBearing.SetActive(false);
            screwNuts.resetTargetBearing();
        }
    }

}
