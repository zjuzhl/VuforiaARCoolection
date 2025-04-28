using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandClicked : MonoBehaviour
{
    public Action<Transform> onClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            var state = touch.phase;
            if (state == TouchPhase.Began)
            {
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
                {
                    onClicked?.Invoke(hitInfo.transform);
                }
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                onClicked?.Invoke(hitInfo.transform);
            }
        }
#endif
    }
}
