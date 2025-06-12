using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VideoPanelCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.Find("VideoClose").GetComponent<Button>().onClick.AddListener(() =>
        {
            var cc = transform.childCount;
            for (int j = 0; j < cc; j++)
            {
                var cld = transform.GetChild(j);
                cld.gameObject.SetActive(false);
            }
            this.transform.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
