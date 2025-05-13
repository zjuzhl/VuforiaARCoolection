using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���߼�
/// </summary>
public class MainController : MonoBehaviour
{
    // ����
    private static MainController instance;
    public static MainController Instance
    {
        get {
            return instance;
        }
    }

    public Button muteBtn;
    [HideInInspector]
    public bool isMute = false;

    public Button backBtn;

    public TrackingManager tracking1;
    public TrackingManager tracking2;
    public TrackingManager tracking3;
    public TrackingManager tracking4;
    public TrackingManager tracking5;
    public TrackingManager tracking6;
    //public List<MenuManager> menus = new List<MenuManager>();

    MenuManager menuManager;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ������ť
        muteBtn.onClick.AddListener(()=> {
            isMute = !isMute;
            if (menuManager) menuManager.ResetMute();
            muteBtn.transform.Find("TextOff").gameObject.SetActive(!isMute);
            muteBtn.transform.Find("TextOn").gameObject.SetActive(isMute);
        });
        // �����ϼ�
        backBtn.onClick.AddListener(()=> {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });

        // ��������״̬
        tracking1.ontracked = (Transform trans) =>
        {
            GetMenu(trans);
        };
        tracking2.ontracked = (Transform trans) =>
        {
            GetMenu(trans);
        };
        tracking3.ontracked = (Transform trans) =>
        {
            GetMenu(trans);
        };
        tracking4.ontracked = (Transform trans) =>
        {
            GetMenu(trans);
        };
        tracking5.ontracked = (Transform trans) =>
        {
            GetMenu(trans);
        };
        tracking6.ontracked = (Transform trans) =>
        {
            GetMenu(trans);
        };
        // ������ʧ״̬
        tracking1.onlost = (Transform trans) =>
        {
            ResetMenu(trans);
        };
        tracking2.onlost = (Transform trans) =>
        {
            ResetMenu(trans);
        };
        tracking3.onlost = (Transform trans) =>
        {
            ResetMenu(trans);
        };
        tracking4.onlost = (Transform trans) =>
        {
            ResetMenu(trans);
        };
        tracking5.onlost = (Transform trans) =>
        {
            ResetMenu(trans);
        };
        tracking6.onlost = (Transform trans) =>
        {
            ResetMenu(trans);
        };
    }

    /// <summary>
    /// �л�������״̬ʱ��ʼ��
    /// </summary>
    /// <param name="trans"></param>
    void GetMenu(Transform trans) 
    {
        menuManager = trans.GetComponent<MenuManager>();
        menuManager.InitInfos();
    }

    /// <summary>
    /// �л�����ʧ״̬ʱ����
    /// </summary>
    /// <param name="trans"></param>
    void ResetMenu(Transform trans)
    {
        var mm = trans.GetComponent<MenuManager>();
        mm.DeinitInfos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
