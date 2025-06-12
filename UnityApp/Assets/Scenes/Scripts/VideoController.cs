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
    public Action<Transform> onPausedTrans;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //Debug.Log(videoPlayer.width + " , " + videoPlayer.height + " / " + Screen.width);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(-40, 720 * (Screen.width - 40) / 1280);

        btnPlay = transform.Find("Play").GetComponent<Button>();
        btnPause = transform.Find("Pause").GetComponent<Button>();
        btnMask = transform.Find("Mask").GetComponent<Button>();
        slider = transform.Find("Progress").GetComponent<Slider>();

        btnPlay.onClick.AddListener(()=> {
            SetVideoPlay();
        });

        btnMask.onClick.AddListener(() => {
            if (!videoPlayer.isPlaying)
            {
                SetVideoPlay();
            }
            else 
            {
                SetVideoPause();
            }
        });

        btnPause.onClick.AddListener(() => {
            SetVideoPlay();
        });

        videoPlayer.loopPointReached += (VideoPlayer source) =>
        {
            SetVideoReady();
        };

        videoPlayer.frameReady += (VideoPlayer source, long frameIdx) => { };
        
        videoPlayer.prepareCompleted += (VideoPlayer source) =>
        {
            StartCoroutine(nameof(delayFrame));
        };

        InitVideoState();
    }

    public void InitVideoState() 
    {
        if (btnMask) btnMask.gameObject.SetActive(true);
        if (btnPlay) btnPlay.gameObject.SetActive(true);
        if (btnPause) btnPause.gameObject.SetActive(false);
        if (slider) slider.value = 0;
        transform.Find("Frame").GetComponent<MPUIKIT.MPImage>().enabled = true;
    }

    public void SetVideoReady() 
    {
        btnPlay.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
    }

    public void SetVideoPlay() 
    {
        videoPlayer.Play();
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(false);

        onPlayedTrans?.Invoke(transform.parent);
    }

    public void SetVideoPause()
    {
        videoPlayer.Pause();
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);

        onPausedTrans?.Invoke(transform.parent);
    }

    public IEnumerator delayFrame() 
    {
        yield return new WaitForEndOfFrame();
        transform.Find("Frame").GetComponent<MPUIKIT.MPImage>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying) 
        {
           if(slider) slider.value = (float)(videoPlayer.time / videoPlayer.length);
        }
    }
}
