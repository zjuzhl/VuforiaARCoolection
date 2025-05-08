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
        // 点击模型的回调
        handClicked.onClicked = (Transform trans) =>
        {
            if (trans.name == "AT") 
            {
                foxTarget.GetComponent<Animator>().SetTrigger("Play");  // 狐狸动画
                screenTarget.GetComponent<Animator>().SetTrigger("Play"); // 开屏动画
            }
            if (trans.name == "Tingzi") {
                tingTarget.GetComponent<Animator>().SetTrigger("Play"); // 亭子旋转动画
                tingAudio.Play();   // 播放音频
            }
        };

        tingTracking.onTracked = (Transform trans) => { };
        tingTracking.onLost = (Transform trans) =>
        {
            tingAudio.Stop(); // 停止音频播放
            // 重置模型初始状态
            if (tingTarget) tingTarget.GetComponent<Animator>().Play("empty", -1, 0);
        };

        foxTracking.onTracked = (Transform trans) => { };
        foxTracking.onLost = (Transform trans) => { };

        screenTracking.onTracked = (Transform trans) => { };
        screenTracking.onLost = (Transform trans) =>
        {
            // 重置模型初始状态
            if (screenTarget) screenTarget.GetComponent<Animator>().Play("empty", -1, 0);
        };
        
    }

    // Update is called once per frame
    void Update() { }
}
