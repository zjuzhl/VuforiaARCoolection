using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim : MonoBehaviour
{
    public float offset = 0.01f;
    private float speed = 0.001f;

    private Vector3 origin;
    private Vector3 targetUp;
    private Vector3 targetDown;

    private bool movingUp;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        targetUp = origin + Vector3.up * offset;
        targetDown = origin + Vector3.down * offset;
        // random
        var r = UnityEngine.Random.Range(0, 1.0f);
        transform.position = targetUp * r + targetDown * (1 - r);
        var r1 = UnityEngine.Random.Range(0, 1.0f);
        movingUp = r1 > 0.5f;
        var r2 = UnityEngine.Random.Range(1.0f, 1.5f);
        speed = 0.01f * r2 * offset;
        Debug.Log(transform.name + " - " + r + " - " + r1 + " - " + r2);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetUp, speed);
            if (Vector3.Distance(transform.position, targetUp) <= 0.001f) 
            {
                movingUp = false;
                // reset speed
                var r2 = UnityEngine.Random.Range(1.0f, 1.5f);
                speed = 0.01f * r2 * offset;
                return;
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, targetDown, speed);
            if (Vector3.Distance(transform.position, targetDown) <= 0.001f)
            {
                movingUp = true;
                // reset speed
                var r2 = UnityEngine.Random.Range(1.0f, 1.5f);
                speed = 0.01f * r2 * offset;
                return;
            }
        }
    }
}
