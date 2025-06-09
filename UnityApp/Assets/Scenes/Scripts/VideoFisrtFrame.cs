using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoFisrtFrame : MonoBehaviour
{

    VideoPlayer videoPlayer;

    public UnityAction<Texture> onCompeleted;

    private int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //GetFirstFrame();
    }

    void GetFirstFrame() 
    {
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.frameReady += frameReady;
        videoPlayer.SetDirectAudioVolume(0, 0);
        videoPlayer.Play();
    }

    void frameReady(VideoPlayer source, long frameIdx) {
        frameCount++;
        if (frameCount == 1) 
        {
            videoPlayer.frameReady -= frameReady;
            videoPlayer.sendFrameReadyEvents = false;
            videoPlayer.Stop();
            videoPlayer.SetDirectAudioVolume(0, 1);
        }
    }
}
