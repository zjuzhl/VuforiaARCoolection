using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{

    /// <summary>
    /// ��ʾʶ��ͼ
    /// </summary>
    public GameObject TipFindAImage;
    /// <summary>
    /// ��ʾ�����ĸ
    /// </summary>
    public GameObject TipClickScrew;
    /// <summary>
    /// ��ʾ��������
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
        // ��ĸ���������û��ʶ��ʱ����ʾʶ��ͼ
        TipFindAImage.SetActive(!TipClickScrew.activeSelf && !TipClickBearing.activeSelf);
    }

    /// <summary>
    /// ������ĸʶ��ͼ����״̬
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
    /// �������ʶ��ͼ����״̬
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
    /// ������ĸʶ��ͼ��ʧ״̬
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
    /// �������ʶ��ͼ��ʧ״̬
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
