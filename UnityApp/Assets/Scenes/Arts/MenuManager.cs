using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public Transform InfoRoot;
    public Transform MenuRoot;

    private AudioSource audioSource;
    private VideoPlayer videoPlayer;
    private AudioSource wordsSource;
    private bool videoPlaying = false;

    private Transform AudioPanel;
    private Transform VideoPanel;
    private Transform WordsPanel;

    // Start is called before the first frame update
    void Start()
    {
        AudioPanel = InfoRoot.Find("AudioPanel");
        VideoPanel = InfoRoot.Find("VideoPanel");
        WordsPanel = InfoRoot.Find("WordsPanel");

        videoPlayer = VideoPanel.GetComponentInChildren<VideoPlayer>();
        audioSource = AudioPanel.GetComponentInChildren<AudioSource>();
        wordsSource = WordsPanel.GetComponentInChildren<AudioSource>();

        // 介绍按钮
        var audios = MenuRoot.Find("Audios").GetComponent<UnityEngine.UI.Button>();
        audios.onClick.AddListener(()=> {
            AudioPanel.gameObject.SetActive(true);
            VideoPanel.gameObject.SetActive(false);
            WordsPanel.gameObject.SetActive(false);

            if (audioSource) {
                audioSource.Play();
                audioSource.mute = MainController.Instance.isMute; // 注意是否静音状态
            }
        });
        // 视频按钮
        var videos = MenuRoot.Find("Videos").GetComponent<UnityEngine.UI.Button>();
        videos.onClick.AddListener(() => {
            AudioPanel.gameObject.SetActive(false);
            VideoPanel.gameObject.SetActive(true);
            WordsPanel.gameObject.SetActive(false);

            if (videoPlayer) 
            {
                videoPlayer.Play();
                videoPlayer.SetDirectAudioMute(0, MainController.Instance.isMute);// 注意是否静音状态
                videoPlaying = true;
                VideoPanel.Find("PlayBtn/TextPlay").gameObject.SetActive(false);
                VideoPanel.Find("PlayBtn/TextPause").gameObject.SetActive(true);
            }
        });
        // 吃法按钮
        var words = MenuRoot.Find("Words").GetComponent<UnityEngine.UI.Button>();
        words.onClick.AddListener(() => {
            AudioPanel.gameObject.SetActive(false);
            VideoPanel.gameObject.SetActive(false);
            WordsPanel.gameObject.SetActive(true);

            if (wordsSource)
            {
                wordsSource.Play();
                wordsSource.mute = MainController.Instance.isMute;// 注意是否静音状态
            }
        });

        // 介绍关闭按钮
        var audioCloseBtn = InfoRoot.Find("AudioPanel/CloseBtn").GetComponent<UnityEngine.UI.Button>();
        audioCloseBtn.onClick.AddListener(() => {
            AudioPanel.gameObject.SetActive(false);
        });

        // 视频播放/暂停按钮
        var videoPlayBtn = InfoRoot.Find("VideoPanel/PlayBtn").GetComponent<UnityEngine.UI.Button>();
        videoPlayBtn.onClick.AddListener(() => {

            if (videoPlaying)
            {
                videoPlayer.Pause();
                videoPlaying = false;
            }
            else {
                videoPlayer.Play();
                videoPlaying = true;
            }
            VideoPanel.Find("PlayBtn/TextPlay").gameObject.SetActive(!videoPlaying);
            VideoPanel.Find("PlayBtn/TextPause").gameObject.SetActive(videoPlaying);
        });
        // 视频关闭按钮
        var videoCloseBtn = InfoRoot.Find("VideoPanel/CloseBtn").GetComponent<UnityEngine.UI.Button>();
        videoCloseBtn.onClick.AddListener(()=> {
            VideoPanel.gameObject.SetActive(false);
        });
        // 吃法关闭按钮
        var wordsCloseBtn = InfoRoot.Find("WordsPanel/CloseBtn").GetComponent<UnityEngine.UI.Button>();
        wordsCloseBtn.onClick.AddListener(() => {
            WordsPanel.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 跟踪时 初始化信息面板
    /// </summary>
    public void InitInfos() 
    {
        // fix：start 可能未执行
        AudioPanel = InfoRoot.Find("AudioPanel");
        VideoPanel = InfoRoot.Find("VideoPanel");
        WordsPanel = InfoRoot.Find("WordsPanel");
        AudioPanel.gameObject.SetActive(false);
        VideoPanel.gameObject.SetActive(false);
        WordsPanel.gameObject.SetActive(false);

        if (!videoPlayer)
        {
            videoPlayer = VideoPanel.GetComponentInChildren<VideoPlayer>();
        }
        videoPlayer.Prepare();
    }

    /// <summary>
    /// 丢失时 重置信息面板
    /// </summary>
    public void DeinitInfos()
    {
        // fix：start 可能未执行
        AudioPanel = InfoRoot.Find("AudioPanel");
        VideoPanel = InfoRoot.Find("VideoPanel");
        WordsPanel = InfoRoot.Find("WordsPanel");
        AudioPanel.gameObject.SetActive(false);
        VideoPanel.gameObject.SetActive(false);
        WordsPanel.gameObject.SetActive(false);
        var handrotate = this.transform.Find("Target").GetComponent<HandRotate>();
        if (handrotate)
        {
            // 重置位姿
            handrotate.resetPose();
            handrotate.resetScale();
        }
    }

    /// <summary>
    /// 更新静音状态
    /// </summary>
    public void ResetMute() 
    {
        audioSource.mute = MainController.Instance.isMute;
        videoPlayer.SetDirectAudioMute(0, MainController.Instance.isMute);
        wordsSource.mute = MainController.Instance.isMute;
    }

}
