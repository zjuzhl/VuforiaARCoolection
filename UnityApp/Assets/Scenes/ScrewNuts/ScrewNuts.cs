using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewNuts : MonoBehaviour
{
    public Transform target;
    /// <summary>
    /// ������ʱ��������Ĭ��ֵ���ڶ���ʱ��
    /// </summary>
    private float starttimecount = 10;
    /// <summary>
    /// ��Ƕ���״̬����¼�Ƿ���š����š��������true��š����������š��
    /// </summary>
    private bool onPlayBack = false;
    /// <summary>
    /// ����ʱ�����ڲ���š����š������ʱ����������������
    /// </summary>
    private float animtime = 2.1f; // �Դ��ڶ���ʱ��

    /// <summary>
    /// ��ʾʶ��
    /// </summary>
    public Transform mpImageRecog;
    /// <summary>
    /// ��ʾ���
    /// </summary>
    public Transform mpImageClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �����м�ʱ��δ�ﵽʱ�䲻������
        if (starttimecount < animtime)
        {
            starttimecount += Time.deltaTime;
            return;
        }

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
                    //�ж��Ƿ����
                    if (hitInfo.transform.name == target.name) 
                    {
                        if (starttimecount > animtime) 
                        {
                            var animator = hitInfo.transform.parent.GetComponent<Animator>();
                            //ͨ�������������ƶ�������
                            animator.SetTrigger(onPlayBack?"PlayOut": "PlayIn");
                            onPlayBack = !onPlayBack;

                            starttimecount = 0;
                        }
                    }
                }
            }
        }

        // �༭��ģʽ�µ��ԣ���Ӱ�췢�����������
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
    /// ��ʾʶ��
    /// </summary>
    public void onRecog() 
    {
        mpImageRecog.gameObject.SetActive(true);
        mpImageClick.gameObject.SetActive(false);
    }
    /// <summary>
    /// ��ʾ���
    /// </summary>
    public void onClick()
    {
        mpImageRecog.gameObject.SetActive(false);
        mpImageClick.gameObject.SetActive(true);
    }

}
