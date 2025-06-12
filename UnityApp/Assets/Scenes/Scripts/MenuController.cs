using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform content;
    public Transform VideoPanel;


    public LoadScene loadScene;
    private Transform curMenu = null;


    // Start is called before the first frame update
    void Start()
    {
        VideoPanel.gameObject.SetActive(false);

        for (int i = 1; i <= 6; i++) 
        {
            var arpath = "Menu" + i + "/BtnAR";
            var videoname = "Video" + i;

            var scenename = "ImageTarget" + i;
            var arBtn = content.Find(arpath).GetComponent<Button>();
            arBtn.onClick.AddListener(() =>
            {
                loadScene.doLoadScene(scenename);
            });

            var videppath = "Menu" + i + "/BtnVideo";
            var videoBtn = content.Find(videppath).GetComponent<Button>();
            videoBtn.onClick.AddListener(()=> 
            {
                VideoPanel.gameObject.SetActive(true);
                var cc = VideoPanel.childCount;
                for (int j = 0; j < cc; j++)
                {
                    var cld = VideoPanel.GetChild(j);
                    cld.gameObject.SetActive(cld.name == videoname);
                }
                VideoPanel.Find("VideoClose").gameObject.SetActive(true);
                VideoPanel.Find(videoname).GetComponent<VideoController>().InitVideoState();
            });
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
