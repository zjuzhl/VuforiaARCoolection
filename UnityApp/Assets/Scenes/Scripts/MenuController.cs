using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button btnJumpAR;
    public LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        btnJumpAR.onClick.AddListener(() =>
        {
            loadScene.doLoadScene("ImageTarget1");
        });
    }


    // Update is called once per frame
    void Update() { }
}
