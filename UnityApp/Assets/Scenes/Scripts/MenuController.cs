using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Transform Content;
    public LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        //btnJumpAR.onClick.AddListener(() =>
        //{
        //    loadScene.doLoadScene("ImageTarget1");
        //});

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
                        loadScene.doLoadScene("ImageTarget1");
                    });
                }
            }

        }
    }


    // Update is called once per frame
    void Update() { }
}
