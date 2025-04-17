using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewNuts : MonoBehaviour
{
    /// <summary>
    /// ��˿��ĸģ��
    /// </summary>
    public Transform targetScrew;
    /// <summary>
    /// �����ģ��
    /// </summary>
    public Transform targetBearing;
    /// <summary>
    /// ��Ƕ���״̬����¼�Ƿ���š����š��������true��š����������š��
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
            //��ȡ��Ļ���
            var touch = Input.GetTouch(0);
            var state = touch.phase;
            //��Ļ�����ʼʱ
            if (state == TouchPhase.Began)
            {
                //����Ƿ������ñģ��
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
                {
                    Debug.Log("hitinfo: " + hitInfo.transform.name);
                    //�ж��Ƿ������ĸ
                    if (hitInfo.transform.parent.name == targetScrew.name) 
                    {
                        var animator = targetScrew.GetComponent<Animator>();
                        //ͨ�������������ƶ�������
                        animator.SetTrigger(onPlayBack ? "PlayOut" : "PlayIn");
                        onPlayBack = !onPlayBack;
                    }
                    //�ж��Ƿ���������
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
    /// ���ó�ʼ��״̬ ��ĸ
    /// </summary>
    public void resetTargetScrew() 
    {
        onPlayBack = false;
        var animator = targetScrew.GetComponent<Animator>();
        animator.Play("empty", -1, 0);
    }

    /// <summary>
    /// ���ó�ʼ��״̬ �����
    /// </summary>
    public void resetTargetBearing()
    {
        var animator = targetBearing.GetComponent<Animator>();
        animator.Play("empty", -1, 0);
    }
}
