using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBtn : MonoBehaviour
{
    public Button switchBtn;

    private bool switched = true;

    // Start is called before the first frame update
    void Start()
    {
        switchBtn.onClick.AddListener(()=> 
        {
            if (switched)
            {
                GameController.instance.SwitchTansuo();
            }
            else {
                GameController.instance.SwitchJiaohu();
            }
            switched = !switched;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
