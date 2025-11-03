using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHappy : MonoBehaviour
{

    public GameObject target;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(()=> {
            SwitchVisible(true);
        });
        SwitchVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchVisible(bool v) {
        target.SetActive(v);
    }
}
