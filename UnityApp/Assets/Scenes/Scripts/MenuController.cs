using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform Content;
    public LoadScene loadScene;
    public Color selectedColor;
    public Color disposeColor;

    private int prevIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        var cc = Content.childCount;
        for (int i = 1; i <= cc; i++) 
        {
            var idx = i;
            var menu = Content.Find("Menu" + idx);
            if (menu) 
            {
                var jpar = menu.Find("JumpAR");
                if (jpar) 
                {
                    jpar.GetComponent<Button>().onClick.AddListener(()=> 
                    {
                        if (prevIndex == idx) return;
                        if (prevIndex > 0)
                        {
                            var preMenu = Content.Find("Menu" + prevIndex);
                            preMenu.GetComponent<MPUIKIT.MPImage>().color = disposeColor;
                            preMenu.Find("AudioBtn/On").gameObject.SetActive(false);
                            preMenu.Find("AudioBtn").GetComponent<AudioSource>().Stop();
                        }
                        prevIndex = idx;
                        menu.GetComponent<MPUIKIT.MPImage>().color = selectedColor;

                        loadScene.doLoadScene("ImageTarget" + idx);
                    });
                }

                var abtn = menu.Find("AudioBtn");
                if (abtn)
                {
                    abtn.Find("On").gameObject.SetActive(false);
                    abtn.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (prevIndex == idx) return;
                        if (prevIndex > 0)
                        {
                            var preMenu = Content.Find("Menu" + prevIndex);
                            preMenu.GetComponent<MPUIKIT.MPImage>().color = disposeColor;
                            preMenu.Find("AudioBtn/On").gameObject.SetActive(false);
                            preMenu.Find("AudioBtn").GetComponent<AudioSource>().Stop();
                        }
                        prevIndex = idx;
                        abtn.GetComponent<AudioSource>().Play();
                        abtn.Find("On").gameObject.SetActive(true);
                        menu.GetComponent<MPUIKIT.MPImage>().color = selectedColor;
                    });
                }
            }
        }
    }


    // Update is called once per frame
    void Update() { }
}
