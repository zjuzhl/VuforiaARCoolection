using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InfosController : MonoBehaviour
{
    public Button BanbaoBtn;
    public Button JianjieBtn;
    public Button VideoBtn;

    public Button VideoPlayBtn;
    public Button VideoPauseBtn;

    public Transform BanbaoInfo;
    public Transform JianjieInfo;
    public Transform VideoInfo;

    // Start is called before the first frame update
    void Start()
    {
        BanbaoInfo = transform.Find("BanbaoInfo");
        JianjieInfo = transform.Find("JianjieInfo");
        VideoInfo = transform.Find("VideoInfo");

        BanbaoBtn = transform.Find("Banbao").GetComponent<Button>();
        JianjieBtn = transform.Find("Jianjie").GetComponent<Button>();
        VideoBtn = transform.Find("Video").GetComponent<Button>();

        VideoPlayBtn = VideoInfo.Find("Play").GetComponent<Button>();
        VideoPauseBtn = VideoInfo.Find("Pause").GetComponent<Button>();

        BanbaoInfo.gameObject.SetActive(false);
        JianjieInfo.gameObject.SetActive(false);
        VideoInfo.gameObject.SetActive(false);

        BanbaoBtn.onClick.AddListener(()=> {
            BanbaoInfo.gameObject.SetActive(true);
            JianjieInfo.gameObject.SetActive(false);
            VideoInfo.gameObject.SetActive(false);
        });

        JianjieBtn.onClick.AddListener(() => {
            BanbaoInfo.gameObject.SetActive(false);
            JianjieInfo.gameObject.SetActive(true);
            VideoInfo.gameObject.SetActive(false);
        });

        VideoBtn.onClick.AddListener(() => {
            BanbaoInfo.gameObject.SetActive(false);
            JianjieInfo.gameObject.SetActive(false);
            VideoInfo.gameObject.SetActive(true);
        });

        VideoPlayBtn.onClick.AddListener(()=> {
            VideoInfo.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        });

        VideoPauseBtn.onClick.AddListener(() => {
            VideoInfo.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();
        });
    }

    // Update is called once per frame
    void Update() { }
}
