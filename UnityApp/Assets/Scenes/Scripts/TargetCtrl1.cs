using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCtrl1 : MonoBehaviour
{

    public HandClicked handClicked;

    // Start is called before the first frame update
    void Start()
    {
        handClicked.onClicked = (Transform trans) =>
        {
            if (trans.name == "bird1")
            {
                trans.GetComponent<Animator>().SetTrigger("Play");
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
