using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageState : MonoBehaviour, StateBase
{

    public Transform canvas;
    public Transform target;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        target.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            onEnter();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            onExit();
        }
    }

    void OnDisable() {
        onExit();
    }

    IEnumerator playContext() 
    {
        int cc = canvas.childCount;
        int i = 0;
        while (i < cc && isPlaying) 
        {
            for (int j = 0; j < cc; j++) 
            {
                canvas.GetChild(j).gameObject.SetActive(i == j);
            }
            var audios = canvas.GetChild(i).GetComponent<AudioSource>();
            yield return new WaitForEndOfFrame();
            Debug.Log(audios.clip.length);
            yield return new WaitForSeconds(audios.clip.length + 1.0f);
            i++;
        }

        for (int j = 0; j < cc; j++)
        {
            canvas.GetChild(j).gameObject.SetActive(false);
        }
    }

    public void onEnter() {
        target.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
        isPlaying = true;
        StartCoroutine(nameof(playContext));
    }

    public void onExit()
    {
        target.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        isPlaying = false;
    }
}
