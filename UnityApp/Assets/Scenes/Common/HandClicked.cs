using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandClicked : MonoBehaviour
{
    public Action<Transform> onClicked;
    public bool isDoubleClick = false;

    public bool enable = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            var state = touch.phase;
            if (state == TouchPhase.Began)
            {
                if (isDoubleClick)
                {
                    if (touch.tapCount == 2) {
                        Vector2 pos = touch.position;
                        ClickRayAction(pos);
                    }
                }
                else {
                    Vector2 pos = touch.position;
                    ClickRayAction(pos);
                }
            }

            if (state == TouchPhase.Stationary) 
            {

            }

            if (state == TouchPhase.Ended)
            {
 
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            ClickRayAction(pos);
        }
#endif
    }

    private void ClickRayAction(Vector2 posOnScreen) 
    {
        if (!IsPointerOverUIObject(posOnScreen))
        {
            var ray = Camera.main.ScreenPointToRay(posOnScreen);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
            {
                onClicked?.Invoke(hitInfo.transform);
            }
        }
    }

    private bool IsPointerOverUIObject(Vector2 posOnScreen)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = posOnScreen;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}