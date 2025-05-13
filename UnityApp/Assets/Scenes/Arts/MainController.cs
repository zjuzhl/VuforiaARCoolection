using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ö÷Âß¼­
/// </summary>
public class MainController : MonoBehaviour
{
    // µ¥Àý
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
        // ¾²Òô°´Å¥
        muteBtn.onClick.AddListener(()=> {
            isMute = !isMute;
            if (menuManager) menuManager.ResetMute();
            muteBtn.transform.Find("TextOff").gameObject.SetActive(!isMute);
            muteBtn.transform.Find("TextOn").gameObject.SetActive(isMute);
        });
        // ·µ»ØÉÏ¼¶
        backBtn.onClick.AddListener(()=> {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });

        // ¼àÌý¸ú×Ù×´Ì¬
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
        // ¼àÌý¶ªÊ§×´Ì¬
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
    /// ÇÐ»»µ½¸ú×Ù×´Ì¬Ê±³õÊ¼»¯
    /// </summary>
    /// <param name="trans"></param>
    void GetMenu(Transform trans) 
    {
        menuManager = trans.GetComponent<MenuManager>();
        menuManager.InitInfos();
    }

    /// <summary>
    /// ÇÐ»»µ½¶ªÊ§×´Ì¬Ê±ÖØÖÃ
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
