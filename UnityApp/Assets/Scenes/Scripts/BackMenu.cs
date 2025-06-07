using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackMenu : MonoBehaviour
{

    public LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        var loading = GameObject.Find("LoadingCanvas");
        if (loading) 
        {
            Debug.Log("Find LoadingCanvas");
            loadScene = loading.GetComponent<LoadScene>();
        }

        this.GetComponent<Button>().onClick.AddListener(()=> 
        {
            if (loadScene) loadScene.doLoadScene("MenuScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
