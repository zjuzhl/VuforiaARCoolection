using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button btnVideoOpen;
    public GameObject VideoPanel;
    public Button btnJumpAR;
    public LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        btnJumpAR.onClick.AddListener(() =>
        {
            loadScene.doLoadScene("ImageTarget1");
        });

        VideoPanel.SetActive(false);
        btnVideoOpen.onClick.AddListener(()=> 
        {
            VideoPanel.SetActive(true);
        });
    }


    // Update is called once per frame
    void Update() { }
}
