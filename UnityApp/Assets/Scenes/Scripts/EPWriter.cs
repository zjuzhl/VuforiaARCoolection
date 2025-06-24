using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPWriter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SaveTracked(string sceneTag) 
    {
        //PlayerPrefs.SetInt("ImageTarget" + sceneId, 1);
        PlayerPrefs.SetInt(sceneTag, 1);
        PlayerPrefs.Save();
    }
}
