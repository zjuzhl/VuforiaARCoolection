using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackingManager : MonoBehaviour
{
    public Transform targetTrans; // ���ƶ���
    public Action<Transform> onTracked; // �������״̬�Ļص�
    public Action<Transform> onLost; // ���붪ʧ״̬�Ļص�

    // Start is called before the first frame update
    void Start()
    {
        targetTrans.gameObject.SetActive(false);
    }

    public void OnTargetTracked() 
    {
        targetTrans.gameObject.SetActive(true);
        onTracked?.Invoke(targetTrans);
    }

    public void OnTargetLost()
    {
        targetTrans.gameObject.SetActive(false);
        onLost?.Invoke(targetTrans);
    }
}
