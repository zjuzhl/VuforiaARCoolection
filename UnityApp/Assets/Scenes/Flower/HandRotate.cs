using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotate : MonoBehaviour
{
    private bool isDragging = false;
    private Transform selectedModel;
    public float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //selectedModel.parent.Rotate(Vector3.up, -0.01f * rotationSpeed * Time.deltaTime);

        if (Input.touchCount == 1) 
        {
            var touch = Input.GetTouch(0);
            var state = touch.phase;
            if (state == TouchPhase.Began) 
            {
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000)) 
                {
                    selectedModel = hitInfo.transform;
                    isDragging = true;
                }
            }
            if (state == TouchPhase.Moved)
            {
                if (isDragging && selectedModel != null) 
                {
                    float deltaX = touch.deltaPosition.x;
                    selectedModel.parent.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime);
                }
            }
            if (state == TouchPhase.Ended)
            {
                selectedModel = null;
                isDragging = false;
            }
        }
    }
}
