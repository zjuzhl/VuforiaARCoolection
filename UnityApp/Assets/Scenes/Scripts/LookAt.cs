using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var campos = Camera.main.transform.position;
        this.transform.LookAt(new Vector3(campos.x, transform.position.y, campos.z), Vector3.up);
    }
}
