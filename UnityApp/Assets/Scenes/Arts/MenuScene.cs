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
        // ��ʼ��Ϸ��ť
        playBtn.onClick.AddListener(() => {
            SceneManager.LoadScene(1);
        });
        // �˳���Ϸ��ť
        quitBtn.onClick.AddListener(()=> {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
