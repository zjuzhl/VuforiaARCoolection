using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;
using System;

public class VideoController : MonoBehaviour
{
    VideoPlayer videoPlayer;

    private Button btnPlay;
    private Button btnPause;
    private Button btnMask;

    private Slider slider;

    public Action<Transform> onPlayedTrans;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        btnPlay = transform.Find("Play").GetComponent<Button>();
        btnPause = transform.Find("Pause").GetComponent<Button>();
        btnMask = transform.Find("Mask").GetComponent<Button>();
        slider = transform.Find("Progress").GetComponent<Slider>();

        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
        btnMask.gameObject.SetActive(false);

        btnPlay.onClick.AddListener(()=> {
            videoPlayer.Play();
            btnMask.gameObject.SetActive(true);
            btnPlay.gameObject.SetActive(false);
            btnPause.gameObject.SetActive(false);

            onPlayedTrans?.Invoke(transform.parent);
        });

        btnMask.onClick.AddListener(() => {
            videoPlayer.Pause();
            btnMask.gameObject.SetActive(false);
            btnPlay.gameObject.SetActive(false);
            btnPause.gameObject.SetActive(true);
        });

        btnPause.onClick.AddListener(() => {
            videoPlayer.Play();
            btnMask.gameObject.SetActive(true);
            btnPlay.gameObject.SetActive(false);
            btnPause.gameObject.SetActive(false);
        });

        videoPlayer.loopPointReached += (VideoPlayer source) =>
        {
            SetVideoReady();
        };
    }

    public void SetVideoReady() 
    {
        videoPlayer.Pause();
        btnMask.gameObject.SetActive(false);
        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying) 
        {
            slider.value = (float)(videoPlayer.time / videoPlayer.length);
        }
    }
}
