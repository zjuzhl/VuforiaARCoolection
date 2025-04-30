using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickeV1() 
    {
        GameController.Instance.RollDog(0);
    }
    public void OnClickeV2()
    {
        GameController.Instance.RollDog(1);
    }
    public void OnClickeV3()
    {
        GameController.Instance.RollDog(2);
    }
}
