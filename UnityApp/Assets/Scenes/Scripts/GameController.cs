using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TrackingManager trackingManager;

    // audio
    public Button audioPlayBtn;
    public AudioSource audioSource;
    private bool recordPlaying;
    private bool isAudioPlaying = false;

    // 文字逐个显示
    public TextGradullay textGradullay;

    // 视频播放
    public VideoController videoController;

    public HandRotate handRotate;

    // Start is called before the first frame update
    void Start()
    {
        trackingManager.onLost += OnLostTarget;

        audioPlayBtn.onClick.AddListener(()=> 
        {
            if (!isAudioPlaying)
            {
                audioSource.Play();
                textGradullay.StartShow();
                isAudioPlaying = true;
                audioPlayBtn.transform.Find("On").gameObject.SetActive(isAudioPlaying);
                audioPlayBtn.transform.Find("Off").gameObject.SetActive(!isAudioPlaying);

                videoController.SetVideoPause();
            }
            else 
            {
                audioSource.Stop();
            }
        });


        videoController.onPlayedTrans += (Transform trans) => 
        {
            if (audioSource.isPlaying) audioSource.Stop();
        };
    }
    void OnDestroy()
    {
        trackingManager.onTracked -= OnLostTarget;
    }

    void OnLostTarget(Transform trans) 
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (videoController.videoPlayer.isPlaying) 
        {
            videoController.SetVideoPause();
        }
    }

    private void Update()
    {
        // 监听音频播放结束或暂停
        if (audioSource.isPlaying)
        {
            recordPlaying = true;
        }
        else if (recordPlaying)
        {
            recordPlaying = false;
            textGradullay.StopShow();
            isAudioPlaying = false;
            audioPlayBtn.transform.Find("On").gameObject.SetActive(isAudioPlaying);
            audioPlayBtn.transform.Find("Off").gameObject.SetActive(!isAudioPlaying);
        }
    }
}
