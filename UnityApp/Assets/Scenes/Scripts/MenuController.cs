using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform content;
    public VideoController[] videoControllers;
    public LoadScene loadScene;
    private Transform curMenu = null;
    // Start is called before the first frame update
    void Start()
    {
        videoControllers = content.GetComponentsInChildren<VideoController>();
        foreach (var vc in videoControllers) 
        {
            vc.onPlayedTrans = (Transform menu) =>
            {
                Debug.Log(" onPlayedTrans " + menu.name);
                menu.Find("Actived").GetComponent<MPUIKIT.MPImage>().enabled = true;
                ChangVideo(menu);
            };
        }

        for (int i = 1; i <= 4; i++) 
        {
            var path = "Menu" + i + "/Button";
            var scenename = "MySample_ImageTarget" + i;
            content.Find(path).GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("clicked: " + path);

                if (curMenu != null)
                {
                    curMenu.GetComponentInChildren<VideoController>().SetVideoReady();
                }
                loadScene.doLoadScene(scenename);
            });
        }

    }

    void ChangVideo(Transform menu)
    {
        if (curMenu != null && curMenu.name != menu.name)
        {
            Debug.Log(curMenu.name + " - " + menu.name);
            curMenu.GetComponentInChildren<VideoController>().SetVideoReady();
            curMenu.Find("Actived").GetComponent<MPUIKIT.MPImage>().enabled = false;
        }
        curMenu = menu;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
