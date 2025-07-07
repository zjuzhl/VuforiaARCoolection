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
            this.transform.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
