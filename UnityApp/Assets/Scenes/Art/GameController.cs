using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public HandClicked handClicked;
    public AudioSource tingAudio;
    public TrackingManager tingTracking;
    public TrackingManager foxTracking;
    public TrackingManager screenTracking;

    public Transform tingTarget;
    public Transform foxTarget;
    public Transform screenTarget;

    // Start is called before the first frame update
    void Start()
    {
        // ���ģ�͵Ļص�
        handClicked.onClicked = (Transform trans) =>
        {
            if (trans.name == "AT") 
            {
                foxTarget.GetComponent<Animator>().SetTrigger("Play");  // ���궯��
                screenTarget.GetComponent<Animator>().SetTrigger("Play"); // ��������
            }
            if (trans.name == "Tingzi") {
                tingTarget.GetComponent<Animator>().SetTrigger("Play"); // ͤ����ת����
                tingAudio.Play();   // ������Ƶ
            }
        };

        tingTracking.onTracked = (Transform trans) => { };
        tingTracking.onLost = (Transform trans) =>
        {
            tingAudio.Stop(); // ֹͣ��Ƶ����
            // ����ģ�ͳ�ʼ״̬
            if (tingTarget) tingTarget.GetComponent<Animator>().Play("empty", -1, 0);
        };

        foxTracking.onTracked = (Transform trans) => { };
        foxTracking.onLost = (Transform trans) => { };

        screenTracking.onTracked = (Transform trans) => { };
        screenTracking.onLost = (Transform trans) =>
        {
            // ����ģ�ͳ�ʼ״̬
            if (screenTarget) screenTarget.GetComponent<Animator>().Play("empty", -1, 0);
        };
        
    }

    // Update is called once per frame
    void Update() { }
}
