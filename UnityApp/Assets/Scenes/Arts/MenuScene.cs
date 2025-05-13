using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{

    public Button playBtn;
    public Button quitBtn;


    // Start is called before the first frame update
    void Start()
    {
        // 开始游戏按钮
        playBtn.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });
        // 退出游戏按钮
        quitBtn.onClick.AddListener(()=> {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
