using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    private Transform loadingPanel;

    private TMPro.TMP_Text progressText;

    private float loadingStartTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        loadingPanel = this.transform.Find("LoadingPanel");
        loadingPanel.gameObject.SetActive(false);

        progressText = this.transform.Find("LoadingPanel/MPImage/Progress").GetComponent<TMPro.TMP_Text>();
        progressText.text = "加载中...";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doLoadScene(string sceneName) 
    {
        StartCoroutine(LoadAsync(sceneName));
    }
    private IEnumerator LoadAsync(string sceneName)
    {

        loadingPanel.gameObject.SetActive(true);
        loadingStartTime = Time.time;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // 阻止场景加载完成后自动激活
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // operation.progress 的值范围是 0.0 到 0.9
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // 更新进度条
            progressText.text = "加载中..." + Mathf.FloorToInt(progress * 99) + "%";

            // 当加载进度达到0.9时，表示加载完成，等待用户操作
            if (operation.progress >= 0.9f)
            {
                var diff = Time.time - loadingStartTime;
                Debug.Log("diff time: " + diff);
                if (diff >= 0.5f)
                {
                    operation.allowSceneActivation = true; // 激活场景
                    yield return new WaitForSeconds(0.1f);
                    loadingPanel.gameObject.SetActive(false);
                }
                else {
                    yield return new WaitForSeconds(0.5f - diff);
                    operation.allowSceneActivation = true; // 激活场景
                    yield return new WaitForSeconds(0.1f);
                    loadingPanel.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

}
