using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPWriter : MonoBehaviour
{

    public int sceneId = 1;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("ImageTarget" + sceneId, 1);
        PlayerPrefs.Save();
    }

}
