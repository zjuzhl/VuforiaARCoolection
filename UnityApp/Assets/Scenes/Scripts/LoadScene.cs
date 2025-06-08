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
        progressText.text = "������...";
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

        // ��ֹ����������ɺ��Զ�����
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // operation.progress ��ֵ��Χ�� 0.0 �� 0.9
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // ���½�����
            progressText.text = "������..." + Mathf.FloorToInt(progress * 99) + "%";

            // �����ؽ��ȴﵽ0.9ʱ����ʾ������ɣ��ȴ��û�����
            if (operation.progress >= 0.9f)
            {
                var diff = Time.time - loadingStartTime;
                Debug.Log("diff time: " + diff);
                if (diff >= 0.5f)
                {
                    operation.allowSceneActivation = true; // �����
                    yield return new WaitForSeconds(0.1f);
                    loadingPanel.gameObject.SetActive(false);
                }
                else {
                    yield return new WaitForSeconds(0.5f - diff);
                    operation.allowSceneActivation = true; // �����
                    yield return new WaitForSeconds(0.1f);
                    loadingPanel.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

}
