using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform content;
    public VideoController[] videoControllers;

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
            content.Find(path).GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("clicked: " + path);
                StartCoroutine(LoadAsync("MySample_ImageTarget1"));
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


    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // ��ֹ����������ɺ��Զ�����
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // operation.progress ��ֵ��Χ�� 0.0 �� 0.9
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // ���½�����
            //if (progressBar != null)
            //    progressBar.value = progress;

            // �����ؽ��ȴﵽ0.9ʱ����ʾ������ɣ��ȴ��û�����
            if (operation.progress >= 0.9f)
            {
                // ��ʾ"�������"֮�����ʾ
                Debug.Log("�����������...");
                operation.allowSceneActivation = true; // �����
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
